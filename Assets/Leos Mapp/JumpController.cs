using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class JumpController : MonoBehaviour
{
    [SerializeField] float JumpPower;
    Rigidbody2D rb;
    bool isGrounded;

    [SerializeField] Animator characterAnimator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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

