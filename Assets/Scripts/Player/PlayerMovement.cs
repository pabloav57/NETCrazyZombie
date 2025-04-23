using UnityEngine;
using Unity.Netcode;

public class PlayerMovement : NetworkBehaviour
{
    [Header("References")]
    [SerializeField] float speed;
    [SerializeField] float jumpForce;

    Rigidbody rb;
    CapsuleCollider col;

    public override void OnNetworkSpawn()
    {
        Cursor.lockState = CursorLockMode.Locked;

        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
    }

    void Update()
    {  
        if (!IsOwner) return;

        // Movimiento lateral y frontal
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        moveInput *= speed * Time.deltaTime;  // Ajustar a velocidad y tiempo

        // Sincronización de movimiento
        MovePlayerRpc(moveInput);

        // Salto
        if (Input.GetButtonDown("Jump"))
        {
            JumpRpc();
        }

        // Liberar el cursor
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    [Rpc(SendTo.Server)]
    void MovePlayerRpc(Vector2 moveInput)
    {
        transform.Translate(moveInput.x, 0, moveInput.y);
    }

    [Rpc(SendTo.Server)]
    void JumpRpc()
    {
        if (IsGrounded())
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, col.bounds.extents.y + 0.1f);
    }
}
