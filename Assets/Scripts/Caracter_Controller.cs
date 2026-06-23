using UnityEngine;
using Unity.Cinemachine;


[RequireComponent(typeof(Rigidbody))] // The Attribute Script provides extra information and, even if it's not already there, it automatically adds a Rigidbody.
public class Caracter_Controller : MonoBehaviour
{
    [Header("Camera")]
    public CinemachineCamera cinCamera;

    [Header("Speeds")]

    [Tooltip("Chracter Move Speed")] // It appears when you hover over it with the mouse.
    public float speed = 10f;

    [Tooltip("Chracter Jump Force")]
    public float JumpForce = 100f;

    [Tooltip("It is Sprint Speed")]
    public float SprintSpeed = 18f;

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

        rb.freezeRotation = true;
    }

    private void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal"); // Daha akıcı tepki vermesi için eklendi test et
        Vertical = Input.GetAxisRaw("Vertical");

        CheckGround();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space Pressed");
            Debug.Log("isGrounded = " + isGrounded);

            if (isGrounded) Jump();
        }

        isSprinting = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
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
        Vector3 camRight = cinCamera.transform.right;
        Vector3 camForward = cinCamera.transform.forward;

        camRight.y = 0f;
        camForward.y = 0f;
        camRight.Normalize();
        camForward.Normalize();

        Vector3 moveDirection = (Horizontal * camRight) + (Vertical * camForward);

        Vector3 normalSpeedVelocity = moveDirection * speed;
        Vector3 sprintSpeedVelocity = moveDirection * SprintSpeed;

        Vector3 linearvelocity = rb.linearVelocity;

        if (!isSprinting)
        {
            linearvelocity = new Vector3(normalSpeedVelocity.x, rb.linearVelocity.y, normalSpeedVelocity.z);
        }
        else if (isSprinting)
        {
            linearvelocity = new Vector3(sprintSpeedVelocity.x, rb.linearVelocity.y, sprintSpeedVelocity.z);
        }

        rb.linearVelocity = linearvelocity;
    }

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
}
