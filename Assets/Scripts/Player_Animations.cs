using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Player_Animations : MonoBehaviour
{
    private Animator animator;
    private Player_Static_Controller player_Controller;
    private Player_Jump_Controller jump_Controller;

    [Header("Smothness")]
    private float currentHorizontal;
    private float currentVertical;
    [SerializeField]private float smoothSpeed = 5f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        player_Controller = GetComponent<Player_Static_Controller>();
        jump_Controller = GetComponent<Player_Jump_Controller>();
    }

    private void Update()
    {
        if (player_Controller == null && jump_Controller == null) return;

        float targetHorizontal = player_Controller.horizontal;
        float targetVertical = player_Controller.vertical;

        if (player_Controller.isSprinting)
        {
            // for positive
            if (targetHorizontal > 0) targetHorizontal = 2f;
            if (targetVertical > 0)   targetVertical = 2f;

            // for negative
            if (targetHorizontal < 0) targetHorizontal = -2f;
            if (targetVertical < 0)   targetVertical = -2f;
        }

        currentHorizontal = Mathf.Lerp(currentHorizontal, targetHorizontal, smoothSpeed * Time.deltaTime);
        currentVertical = Mathf.Lerp(currentVertical, targetVertical, smoothSpeed * Time.deltaTime);

        animator.SetFloat("Horizontal", currentHorizontal);
        animator.SetFloat("Vertical", currentVertical);

        animator.SetBool("isRunning", player_Controller.isSprinting);
        animator.SetBool("isJumping", jump_Controller.currentstate == playerGroundState.Jumping);
        animator.SetBool("isGrounded", jump_Controller.currentstate == playerGroundState.Grounded);
    }
}
