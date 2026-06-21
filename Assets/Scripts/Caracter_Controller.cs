using UnityEngine;

public class Caracter_Controller : MonoBehaviour
{
    public float speed = 50f;
    public float JumpForce = 100f;

    private bool isGrounded;
    private Rigidbody rb;

    private float x;
    private float z;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    void Move()
    {
        Vector3 linearvelocity = rb.linearVelocity;

        linearvelocity.x = x * speed;
        linearvelocity.z = z * speed;

        rb.linearVelocity = linearvelocity;
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Jump()
    {
        Vector3 linearvelocity = rb.linearVelocity;

        linearvelocity.y = JumpForce;

        rb.linearVelocity = linearvelocity;

        isGrounded = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
