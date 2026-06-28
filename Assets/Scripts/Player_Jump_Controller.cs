using UnityEngine;

public enum playerGroundState
{
    Grounded,
    Jumping,
    Falling
}

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Player_Static_Controller))]
public class Player_Jump_Controller : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float raycastDistance = 0.3f;
    [SerializeField] private LayerMask groundLayer;

    private playerGroundState currentState = playerGroundState.Grounded;
    public playerGroundState currentstate => currentState;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void CheckGround()
    {
        Vector3 rayOrigin = transform.position + Vector3.up * 0.1f;

        bool hitGround = Physics.Raycast(rayOrigin, Vector3.down, raycastDistance, groundLayer);

        if (hitGround)
        {
            if (currentState == playerGroundState.Falling)
            {
                currentState = playerGroundState.Grounded;
            }
        }
        else
        {
            if (currentState == playerGroundState.Grounded)
            {
                currentState = playerGroundState.Falling;
            }
        }
    }

    void Jump()
    {
        currentState = playerGroundState.Jumping;

        Vector3 velocity = rb.linearVelocity;
        velocity.y = jumpForce;
        rb.linearVelocity = velocity;
    }

    private void Update()
    {
        CheckGround();

        if (currentState == playerGroundState.Jumping && rb.linearVelocity.y < 0)
        {
            currentState = playerGroundState.Falling;
        }

        if (Input.GetKeyDown(KeyCode.Space) && currentState == playerGroundState.Grounded)
        {
            Jump();
            
        }
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 rayOrigin = transform.position + Vector3.up * 0.1f;
        Gizmos.color = Color.black;
        Gizmos.DrawLine(rayOrigin, rayOrigin + Vector3.down * raycastDistance);
    }
}
