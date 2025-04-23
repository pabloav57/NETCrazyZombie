using UnityEngine;
using Unity.Netcode;

public class BulletHit : NetworkBehaviour
{
    [SerializeField] GameObject particle;

    private void OnCollisionEnter(Collision collision)
    {
        if (IsClient)
        {
            Instantiate(particle, transform.position, Quaternion.identity);
        }
    }
}
