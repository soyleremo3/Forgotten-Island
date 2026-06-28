using UnityEngine;

[RequireComponent(typeof(Rigidbody))] // The Attribute Script provides extra information and, even if it's not already there, it automatically adds a Rigidbody.
[RequireComponent(typeof(Player_Jump_Controller))]
public class Player_Static_Controller : MonoBehaviour
{
    [Header("Speeds")]

    [Tooltip("Chracter Move Speed")] // It appears when you hover over it with the mouse.
    public float speed = 10f;

    [Tooltip("It is Sprint Speed")]
    public float SprintSpeed = 18f;

    [HideInInspector] public bool isSprinting;

    private Rigidbody rb;

    [HideInInspector] public float horizontal;
    [HideInInspector] public float vertical;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.freezeRotation = true;
    }

    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        isSprinting = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
    }

    void Move()
    {
        Vector3 moveDirection = new Vector3(horizontal, 0f, vertical);

        float currentSpeed = isSprinting ? SprintSpeed : speed;

        if (moveDirection.magnitude > 1f) moveDirection.Normalize();

        Vector3 targetVelocity = moveDirection * currentSpeed;

        rb.linearVelocity = new Vector3(targetVelocity.x, rb.linearVelocity.y, targetVelocity.z);
    }

    private void FixedUpdate()
    {
        Move();
    }
}
