using System;
using UnityEngine;
using Unity.Netcode;

public class PlayerFireBullet : NetworkBehaviour
{
    [SerializeField] GameObject proyectile;
        
    void Update()
    {
        if(!IsOwner) return;
        if (Input.GetButtonDown("Fire1"))
        {
            FireRpc();
        }
    }

    [Rpc(SendTo.Server)]
    void FireRpc()
{
    Vector3 pos = transform.position + transform.forward * 1.5f;
    Quaternion rot = transform.rotation;

    GameObject bala = Instantiate(proyectile, pos, rot);
    bala.GetComponent<NetworkObject>().Spawn(true);
    }
}
