using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Control : MonoBehaviour
{
    Animator anim;

    [Header("Player Settings")]
    [Range(0, 10f)] public float speed = 5f;
    [Range(0, 20f)] public float jumpForce = 5f;

    private float startPos_y = 0.1f;
    private float endPos_y = 0;
    private float startPos_x = 1.6f;
    private float endPos_x = 0;
    private float speedChangeShadow = 0.001f;

    Rigidbody2D rb;

    [Header("Ground Settings")]
    public bool isGrounded;
    public Transform GroundCheck;
    public float checkRadius = 0.5f;
    public LayerMask Ground;

    public ParticleSystem dust;

    public GameObject shadow;

    bool FacingRight = true;

    int directionInput;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(speed * directionInput, rb.velocity.y);
        if (anim)
        {
            anim.SetBool("run", Mathf.Abs(directionInput) >= 0.1f);
        }

        if (!isGrounded)
        {
            anim.SetBool("jumping", true);
        }
        else anim.SetBool("jumping", false);

        if(directionInput != 0)
        {
            CreateDust();
        }


        if (directionInput < 0 && FacingRight)
            Flip();
        else if ((directionInput > 0 && !FacingRight))
            Flip();

        CheckingGround();
        changeShadow();
    }

    public void Move(int InputAxis)
    {
        directionInput = InputAxis;
    }

    public void Jump()
    {
        if (isGrounded)
        {
            rb.velocity = Vector2.up * jumpForce;

        }
        if (Mathf.Abs(rb.velocity.y) >= 0.1f)
        {
            anim.SetTrigger("jump");
        }
    }

    void Flip()
    {
        FacingRight = !FacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void CheckingGround()
    {
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, checkRadius, Ground);
    }

    void CreateDust()
    {
        dust.Play();
    }

    void changeShadow() 
    {
        if (isGrounded)
            shadow.transform.localScale = new Vector3(Mathf.Lerp(startPos_x, endPos_x, Time.deltaTime * speedChangeShadow),
                Mathf.Lerp(startPos_y, endPos_y, Time.deltaTime * speedChangeShadow),
                shadow.transform.localScale.z);
        else
            shadow.transform.localScale = new Vector3(Mathf.Lerp(endPos_x, startPos_x, Time.deltaTime * speedChangeShadow),
                Mathf.Lerp(endPos_y, startPos_y, Time.deltaTime * speedChangeShadow),
                shadow.transform.localScale.z);
    }
}

