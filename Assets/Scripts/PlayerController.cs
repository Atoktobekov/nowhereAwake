using System;
using System.Collections;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    
    private float horizontalMove;
    private bool facingRight = true;
    private bool canMove = true;
    public bool recentlyTeleported = false;
    
    
    public int extraJumps = 1;
    private int jumpCount = 0;
    private bool isDead;

    [Header("Player Movement Settings")] 
    [Range(5, 25f)] public float jumpForce;
    [Range(0, 10f)] public float speed;

    [Space] [Header("Ground Checker Settings")]
    public bool isGrounded = false;
    [Range(-5f,5f)] public float checkGroundOffSetY = -1.8f;
    [Range(0, 5f)] public float checkGroundRadius = 0.3f;
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    

    void Update()
    {
        if (isDead) return;
        if (!canMove) return;
        Jump();
        horizontalMove = Input.GetAxis("Horizontal") * speed;
        if (horizontalMove < 0 && facingRight)
        {
            Flip();
        }
        else if (horizontalMove > 0 && !facingRight)
        {
            Flip();
        }
        if (rb.linearVelocity.y < 0)
        {
            //animator.SetBool("isJumping", false);
        }
       
        
    }
    private void FixedUpdate()
    {
        if (isDead) return;
        if (!canMove) return;

        Move();
        checkGround();
        UpdateAnimator();
    }
    
    private void Move()
    {   
        Vector2 targetVelocity = new Vector2(horizontalMove * 5f, rb.linearVelocity.y);
        rb.linearVelocity = targetVelocity;
    }
   private void UpdateAnimator()
    {
        animator.SetBool("Jump", !isGrounded); 
        //animator.SetBool("isGrounded", isGrounded); 
        animator.SetBool("Run", isGrounded && Mathf.Abs(rb.linearVelocity.x) > 0.07f);
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void Jump()
    {
        if (isGrounded)
        {
            jumpCount = 0;
        }
        if ((isGrounded || jumpCount<extraJumps)
            && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            AudioManager.instance.PlaySFX("Jump");
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); // Устанавливаем вертикальную скорость
            jumpCount++;
            animator.SetBool("Jump", true);
        }

    }
    
    
    public void getDamage(Vector2 vector2, float force)
    {
        if (rb != null)
        {   AudioManager.instance.PlaySFX("Hit");
            rb.linearVelocity = new Vector2(vector2.x * force, vector2.y * force);
            StartCoroutine(KnockbackCoroutine(0.2f));
        }
    }
    
  private void checkGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll
            (new Vector2(transform.position.x, transform.position.y + checkGroundOffSetY), checkGroundRadius);

        if (colliders.Length > 1)
        {
            isGrounded = true;
            jumpCount = 0;
        }
        else
        {
            isGrounded = false;
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; 

        Vector2 groundCheckPos = new Vector2(transform.position.x, transform.position.y + checkGroundOffSetY);

        Gizmos.DrawWireSphere(groundCheckPos, checkGroundRadius);
    }
    
    public IEnumerator KnockbackCoroutine(float duration)
    {
        canMove = false; 
        yield return new WaitForSeconds(duration);
        canMove = true; 
    }

    public void SetCanMove(bool value)
    {
        isDead = !value;
        if (isDead)
        {
            rb.bodyType = RigidbodyType2D.Static;
            enabled = false;
        }
        else
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            enabled = true;
        }
    }
    
    public void SetTeleportCooldown(float duration)
    {
        if (!recentlyTeleported)
        {
            StartCoroutine(TeleportCooldownRoutine(duration));
        }
    }
    
    private System.Collections.IEnumerator TeleportCooldownRoutine(float duration)
    {
        recentlyTeleported = true;
        yield return new WaitForSeconds(duration);
        recentlyTeleported = false;
    }
}