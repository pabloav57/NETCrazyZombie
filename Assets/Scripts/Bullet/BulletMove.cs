using UnityEngine;
using Unity.Netcode;

public class BulletMove : NetworkBehaviour
{
    public float speed = 10.0f;
    public float lifeTime = 5f;
    public int PLAYER_DAMAGE = 10;

    private bool isDespawning = false;
    private NetworkObject networkObject;

    private void Start()
    {
        networkObject = GetComponent<NetworkObject>();

        if (IsServer)
        {
            // Destruir la bala después de cierto tiempo
            Invoke(nameof(DestroyBullet), lifeTime);
        }
    }

    private void Update()
    {
        if (IsServer)
        {
            // Mover la bala solo en el servidor
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        else if (IsOwner) // Asegurarse de que el cliente también tenga una bala que se mueve
        {
            // En el cliente, aseguramos que la bala se mueva (por si hay jitter)
            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }

    private void DestroyBullet()
    {
        if (IsServer && !isDespawning)
        {
            isDespawning = true;
            networkObject.Despawn();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!IsServer || isDespawning) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.SendMessage("ApplyDamage", PLAYER_DAMAGE);
        }

        // Despawn solo una vez
        isDespawning = true;
        networkObject.Despawn();
    }
}
