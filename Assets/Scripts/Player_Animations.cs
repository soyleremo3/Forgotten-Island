using UnityEngine;

public class Player_Animations : MonoBehaviour
{
    private Animator animator;
    private Player_Controller player_Controller;

    [Header("Smothness")]
    private float currentHorizontal;
    private float currentVertical;
    public float smothSpeed = 5f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        player_Controller = GetComponent<Player_Controller>();
    }

    private void Update()
    {
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

        currentHorizontal = Mathf.Lerp(currentHorizontal, targetHorizontal, smothSpeed * Time.deltaTime);
        currentVertical = Mathf.Lerp(currentVertical, targetVertical, smothSpeed * Time.deltaTime);

        animator.SetFloat("Horizontal", currentHorizontal);
        animator.SetFloat("Vertical", currentVertical);

        animator.SetBool("isRunning", player_Controller.isSprinting);
        animator.SetBool("isJumping", player_Controller.isJumping);
        animator.SetBool("isGrounded", player_Controller.isGrounded);
    }
}
