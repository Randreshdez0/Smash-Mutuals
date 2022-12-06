using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public float runSpeed;
    public float rayLength = 1f;
    public bool facingLeft = false;
    public int extraJumps, extraJumpsValue;
    public float jumpStrength = 35f;
    public float normalGravity = 5f;
    public float fastGravity = 14;

    public float stunTime = 0f;

    public LayerMask groundLayerMask;
    public PlayerInput inp;
    public PlayerAnimationScript anim;
    public Rigidbody2D rb;
    public BoxCollider2D boxCollider2d;
    public GameObject ledgeGrabBox;

    public PlayerBaseState currentState;
    public PlayerGroundAttackState groundAttackState = new PlayerGroundAttackState();
    public PlayerAirAttackState airAttackState = new PlayerAirAttackState();
    public PlayerFallState FallState = new PlayerFallState();
    public PlayerJumpState JumpState = new PlayerJumpState();
    public PlayerLedgeState LedgeState = new PlayerLedgeState();
    public PlayerRunState RunState = new PlayerRunState(); // grounded state?
    public PlayerHitStunState HitStunState = new PlayerHitStunState();
    //Hitstun state

    private void Start()
    {
        SetVariables();
        currentState = RunState;

        currentState.EnterState(this);
    }
    private void Update()
    {
        currentState.UpdateState(this);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        currentState.OnTriggerEnter2D(this, collision);
    }

    public void SwitchState(PlayerBaseState state)
    {
        currentState = state;
        state.EnterState(this);
        //Debug.Log(state);
    }

    public void FlipPlayer(bool left)
    {
        float characterScale;
        if (!left)
        {
            characterScale = 1;
            facingLeft = false;
        }
        else
        {
            characterScale = -1;
            facingLeft = true;
        }
        transform.localScale = new Vector3(characterScale, transform.localScale.y, transform.localScale.z);
    }
    void SetVariables()
    {
        inp = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider2d = GetComponent<BoxCollider2D>();
    }

    public void OnAnimationEnd()
    {
        currentState.OnAnimationEnd(this);
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
    }
    
}
