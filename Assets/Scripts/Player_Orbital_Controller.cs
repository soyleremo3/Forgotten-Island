using UnityEngine;
using Unity.Cinemachine;

[RequireComponent(typeof(Rigidbody))] // The Attribute Script provides extra information and, even if it's not already there, it automatically adds a Rigidbody.
public class Player_Orbital_Controller : MonoBehaviour
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

    [HideInInspector] public bool isSprinting;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float radius = 1f;
    [SerializeField] private LayerMask GroundLayer;

    [HideInInspector] public bool isGrounded;
    private Rigidbody rb;

    [HideInInspector] public float horizontal;
    [HideInInspector] public float vertical;

    private bool jumpRequested = false;
    [HideInInspector] public bool isJumping = false;

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
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (!isJumping)
        {
            CheckGround();
        }
        else if (rb.linearVelocity.y < 0f)
        {
            CheckGround();
            if (isGrounded)
            {
                isJumping = false;
            }
        }

        isSprinting = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) jumpRequested = true;
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
        if (groundCheck == null) return;

        Gizmos.color = Color.blue;

        Gizmos.DrawWireSphere(
            groundCheck.position,
            radius);
    }

    void Move()
    {
        Vector3 camRight = Camera.main.transform.right;
        Vector3 camForward = Camera.main.transform.forward;

        camRight.y = 0f;
        camForward.y = 0f;
        camRight.Normalize();
        camForward.Normalize();

        Vector3 moveDirection = (vertical * camForward) + (horizontal * camRight);

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

    void Jump()
    {
        isJumping = true;
        Debug.Log("Jump Called Via FixedUpdate");

        Vector3 linearvelocity = rb.linearVelocity;
        linearvelocity.y = jumpForce;
        rb.linearVelocity = linearvelocity;

        isGrounded = false;
        jumpRequested = false;
    }

    private void FixedUpdate()
    {
        Move();

        if (jumpRequested)
        {
            Jump();
        }
    }
}
