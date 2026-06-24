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
    public float jumpForce = 20f;

    [Tooltip("It is Sprint Speed")]
    public float SprintSpeed = 18f;

    public float rotationSpeed = 10f;

    private bool isSprinting;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float radius = 1f;
    [SerializeField] private LayerMask GroundLayer;

    private bool isGrounded;
    private Rigidbody rb;

    private float horizontal;
    private float vertical;

    /*[Tooltip("Timer for jump")]
    private float JumpTimer; // Timer for jump*/

    private bool jumpRequested = false;
    private bool isJumping = false;

    [Header("Mouse Sensitivity")]
    public float mouseSensitivity = 300f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.freezeRotation = true;

        var inputcontroller = cinCamera.GetComponent<CinemachineInputAxisController>();
        if (inputcontroller != null)
        {
            foreach (var axis in inputcontroller.Controllers)
            {
                axis.Input.Gain = mouseSensitivity / 100f;
            }
        }
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal"); // I used Raw for a smoother experience.
        vertical = Input.GetAxisRaw("Vertical");

        /*if (JumpTimer > 0)
        {
            JumpTimer -= Time.deltaTime;
            isGrounded = false;
        }
        else
        {
            CheckGround();
        }*/
        if (!isJumping)
        {
            CheckGround();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space Pressed");
            Debug.Log("isGrounded = " + isGrounded);

            if (isGrounded) Jump();
        }

        isSprinting = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        if (Input.GetKeyDown(KeyCode.Space)  && isGrounded) jumpRequested = true;
    }

    void CheckGround()
    {
        isGrounded = Physics.CheckSphere(
            groundCheck.position, // Is there a collider inside a sphere of a specific radius?
            radius, 
            GroundLayer);
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck == null ) return;

        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(
            groundCheck.position,
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

        Vector3 moveDirection = (horizontal * camRight) + (vertical * camForward);

        float currentSpeed = isSprinting ? SprintSpeed : speed;

        if (moveDirection.magnitude > 1f) moveDirection.Normalize();

        Vector3 targetVelocity = moveDirection * currentSpeed;

        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }

        rb.linearVelocity = new Vector3(targetVelocity.x, rb.linearVelocity.y, targetVelocity.z);
    }

    private void FixedUpdate()
    {
        Move();

        if (jumpRequested)
        {
            Jump();
            jumpRequested = false;
        }
    }

    void Jump()
    {
        isJumping = true;
        Debug.Log("Jump Called");

        Vector3 linearvelocity = rb.linearVelocity;

        linearvelocity.y = jumpForce;

        rb.linearVelocity = linearvelocity;

        isGrounded = false;

        //JumpTimer = 0.15f;
    }
}
