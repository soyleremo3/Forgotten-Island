using UnityEngine;

[RequireComponent(typeof(Rigidbody))] // The Attribute Script provides extra information and, even if it's not already there, it automatically adds a Rigidbody.
public class Caracter_Controller : MonoBehaviour
{
    [Header("Speeds")]

    [Tooltip("Chracter Move Speed")] // It appears when you hover over it with the mouse.
    public float speed = 50f;

    [Tooltip("Chracter Jump Force")]
    public float JumpForce = 100f;

    [Tooltip("It is Sprint Speed")]
    public float SprintSpeed = 20f;

    private bool isSprinting;

    [Header("Ground Check")]
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private float radius = 1f;
    [SerializeField] private LayerMask GroundLayer;

    private bool isGrounded;
    private Rigidbody rb;

    private float Horizontal;
    private float Vertical;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");

        CheckGround();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space Pressed");
            Debug.Log("isGrounded = " + isGrounded);

            if (isGrounded) Jump();
        }

        isSprinting = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        /*if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            isSprinting = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            isSprinting = false;
        }*/

        /*if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            sprint();
        }

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            
        }*/
    }

    void CheckGround()
    {
        isGrounded = Physics.CheckSphere(
            GroundCheck.position, // Is there a collider inside a sphere of a specific radius?
            radius, 
            GroundLayer);
    }

    void OnDrawGizmosSelected()
    {
        if (GroundCheck == null ) return;

        Gizmos.DrawWireSphere(
            GroundCheck.position,
            radius);
    }

    void Move()
    {
        Vector3 linearvelocity = rb.linearVelocity;

        if (!isSprinting)
        {
            

            linearvelocity.x = Horizontal * speed;
            linearvelocity.z = Vertical * speed;

            
        }
        else if (isSprinting)
        {
            linearvelocity.x = Horizontal * SprintSpeed;
            linearvelocity.z = Vertical * SprintSpeed;
        }

        rb.linearVelocity = linearvelocity;
    }

    /*void sprint()
    {
        Vector3 linearvelocity = rb.linearVelocity;

        linearvelocity.x = Horizontal * SprintSpeed;
        linearvelocity.z = Vertical * SprintSpeed;

        rb.linearVelocity = linearvelocity;
    }*/

    private void FixedUpdate()
    {
        Move();
    }

    void Jump()
    {
        Debug.Log("Jump Called");

        Vector3 linearvelocity = rb.linearVelocity;

        linearvelocity.y = JumpForce;

        rb.linearVelocity = linearvelocity;

        isGrounded = false;
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }*/
}
