/*
 * TODO: https://www.youtube.com/watch?v=Vt8aZDPzRjI
 * 
 * Andres Hernandez
 * 6/10/2022
 * 
 * 
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerMovement : MonoBehaviour
{
    //player movement info
    [Header("Info")]
    [SerializeField] private int player;
    [SerializeField] private float rayLength = 1f;
    
    private bool canJump = true;
    private bool canMove = true;
    public bool isJumping = false;
    public bool facingLeft = false;
    private bool isFalling = false;
    public bool isAttacking = false;
    private float characterScale = 1;
    private bool fastFalling = false;
    private bool grabbingLedge = false; 

    [SerializeField] private float normalGravity = 5f;
    [SerializeField] private float fastGravity = 14;

    //Hitstun things
    [Header("Hitstun Info")]
    public  bool inHitstun = false;
    private float stunTime = 0f;
    [SerializeField] GameObject stunParticles;
    //Move things
    [Header("Movement Info")]
    public KeyCode jumpButton;
    //Character Info
    [Header("Fighter")]
    [SerializeField] float jumpStrength;
    [SerializeField] float runSpeed;
    [SerializeField] int extraJumpsValue;
    [SerializeField] int extraJumps;

    Rigidbody2D rb;
    BoxCollider2D boxCollider2d;
    private PhysicsMaterial2D p;
    public LayerMask groundLayerMask;
    public LayerMask platformLayerMask;

    [SerializeField] GameObject jumpParticle;

    PlayerInput inp;
    PlayerAttacking attackScript;
    public PlayerAnimationScript anim;
    public FighterScript fighter;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider2d = GetComponent<BoxCollider2D>();
        anim = GetComponent<PlayerAnimationScript>();
        inp = GetComponent<PlayerInput>();
        attackScript = GetComponent<PlayerAttacking>();

        extraJumps = extraJumpsValue;
        StartCoroutine(SpawnParticles());
        p = new PhysicsMaterial2D(name + " Mat");
        boxCollider2d.sharedMaterial = p;
        p.friction = 0f;

    }
    private void Update()
    {
        if (!inHitstun)
        {
            DoubleJumping();
            GetInput();
            TurningAround();
            HandleFalling();
        }
        if (stunTime > 0)
        {
            stunTime -= Time.deltaTime;
        }
        if (inHitstun && stunTime <= 0)
        {
            inHitstun = false;
            p.bounciness = 0f;
        }
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void GetInput()
    {
        if (IsGrounded() && !isAttacking && !isJumping && !isFalling)
        {
            if (!StandingStill())
            {
                anim.ChangeAnimationState(anim.PLAYER_RUN);
            }
            else
            {
                anim.ChangeAnimationState(anim.PLAYER_IDLE);
            }
        }
        //Fallen with attack cancel
        if (IsGrounded() && attackScript.airAttacking && isAttacking)
        {
            AttackComplete();
            attackScript.airAttacking = false;
        }
        // grounded attacking falling

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        FastFalling();

    }

    private void FastFalling()
    {
        //fast falling
        if (inp.Holding(PlayerInput.InputState.Down) && !IsGrounded() && !fastFalling)
        {
            fastFalling = true;
            rb.gravityScale = fastGravity;
        }
        if (IsGrounded() && fastFalling)
        {
            fastFalling = false;
            rb.gravityScale = normalGravity;
        }
    }

    private void Move()
    {
        if (canMove && !inHitstun) //Move
        {
            if (inp.Holding(PlayerInput.InputState.Right))
            {
                rb.velocity = 
                    new Vector2(Mathf.Lerp(rb.velocity.x, runSpeed, .15f), rb.velocity.y);
            }
            else if (inp.Holding(PlayerInput.InputState.Left))
            {
                rb.velocity = 
                    new Vector2(Mathf.Lerp(rb.velocity.x, -runSpeed, .15f), rb.velocity.y);
            }
            else
            {
                rb.velocity = 
                    new Vector2(Mathf.Lerp(rb.velocity.x, 0f, .09f), rb.velocity.y);
            }
        }
    }
    private void DoubleJumping()
    {
        //Double Jumping
        if (IsGrounded())
        {
            extraJumps = extraJumpsValue;
        }
        if (!IsGrounded() && Input.GetKeyDown(jumpButton) && extraJumps > 0 && canJump) //First Jump
        {
            Jump();
            extraJumps--;
            var particle = Instantiate(jumpParticle, new Vector2(transform.position.x, transform.position.y - boxCollider2d.size.y), Quaternion.identity);
            Destroy(particle, 1.1f);
        }
        if (IsGrounded() && Input.GetKeyDown(jumpButton) && canJump) //I don't know
        {
            Jump();
        }
        //Jumping - This may not be legal
        void Jump()
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpStrength, ForceMode2D.Impulse);
            rb.gravityScale = normalGravity;
            anim.ChangeAnimationState(anim.PLAYER_JUMP);
        }
        if (!isFalling && !isAttacking && rb.velocity.y > 0.1f) {
            anim.ChangeAnimationState(anim.PLAYER_JUMP);
        } 
    }
    private void TurningAround()
    {
        //Turning Around
        if (inp.Holding(PlayerInput.InputState.Left) && !isAttacking && IsGrounded() && !facingLeft)
        {
            characterScale = -1;
            facingLeft = true;
        }
        if (inp.Holding(PlayerInput.InputState.Right) && !isAttacking && IsGrounded() && facingLeft)
        {
            characterScale = 1;
            facingLeft = false;
        }
        transform.localScale = new Vector3(characterScale, transform.localScale.y, transform.localScale.z);
    }
    private void HandleFalling()
    {
        if (rb.velocity.y < -0.0001f && !isFalling && !IsGrounded())
        {
            isFalling = true;
            if (!isAttacking)
            {
                anim.ChangeAnimationState(anim.PLAYER_FALL);
            }
        }
        else isFalling = false;
    }
    public bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2d.bounds.center, 
            boxCollider2d.bounds.size - new Vector3(0.1f, 0f, 0f), 0f, Vector2.down, rayLength, groundLayerMask);
        Color rayColor;
        if (raycastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(boxCollider2d.bounds.center, Vector2.down * (boxCollider2d.bounds.extents.y + rayLength), rayColor);

        return raycastHit.collider != null;
        
/*        // Changed condition from !isJumping
        if (!isFalling && !isAttacking && rb.velocity.y > 0.1f)
        {
        }
        else return false;*/
    }
    public bool OnPlatform()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2d.bounds.center, 
            boxCollider2d.bounds.size - new Vector3(0.1f, 0f, 0f), 0f, Vector2.down, rayLength, platformLayerMask);
        return raycastHit.collider != null;
    }
    public void SetCanJumpFalse()
    {
        canJump = false;
    }
    public void SetCanJumpTrue()
    {
        canJump = true;
    }
    public void SetCanMove(int b)
    {
        if (b == 0)
            canMove = false;
        else 
            canMove = true;
    }
    public void AttackStarted()
    {
        isAttacking = true;
    }
    public void AttackComplete()
    {
        isAttacking = false;
    }
    public void SetInHitStun(float newStunTime)
    {
        inHitstun = true;
        p.bounciness = 2f;
        stunTime = newStunTime;
        anim.ChangeAnimationState(anim.PLAYER_HITSTUN);
    }
    private IEnumerator SpawnParticles()
    {
        while (true)
        {
            if (inHitstun)
            {
                var particle = Instantiate(stunParticles, transform.position, Quaternion.identity);
                Destroy(particle, 1f);
            }
            yield
            return new WaitForSeconds(0.3f);
        }
    }
    public bool StandingStill()
    {
        return !(inp.Holding(PlayerInput.InputState.Right) 
            || inp.Holding(PlayerInput.InputState.Left));
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            int pushFactor = (transform.position.x <= collision.transform.position.x) ? -1 : 1;
            rb.AddForce(Vector2.right * pushFactor);
        }
    }

    public void SetGrabbingLedge(bool state)
    {
        if (state == grabbingLedge) return;
        if(state && !grabbingLedge)
        {
            canMove = false;
            SetCanJumpFalse();
            grabbingLedge = true;
            anim.ChangeAnimationState(anim.PLAYER_LEDGEGRAB);
            rb.gravityScale = 0;
            transform.GetChild(1).gameObject.SetActive(false);
        }
    }
}
