using UnityEngine;

public class JumpController : MonoBehaviour
{
    [SerializeField] float JumpPower;
    Rigidbody2D rb;
    bool isGrounded = true;

    Animator characterAnimator;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        characterAnimator = GetComponentInChildren<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpPower);

            characterAnimator.SetBool("isJumping", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;

            characterAnimator.SetBool("isJumping", false);
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

}

