using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerBaseState
{
    private float rayLength = 1f;
    private float runSpeed;

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider2d;
    private LayerMask groundLayerMask;

    private PlayerInput inp;

    float characterScale = 1;
    public override void EnterState(PlayerStateManager player)
    {
        player.extraJumps = player.extraJumpsValue;
        player.ledgeGrabBox.SetActive(false);
        SetVariables(player);
    }
    public override void UpdateState(PlayerStateManager player)
    {
        TurningAround(player);

        rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, inp.movX * runSpeed, .05f), rb.velocity.y);

        if (inp.movX != 0)
        {
            player.anim.ChangeAnimationState(player.anim.PLAYER_RUN);
        }
        else
        {
            player.anim.ChangeAnimationState(player.anim.PLAYER_IDLE);
        }

        if(Input.GetKeyDown(inp.jumpButton))
        {
            player.SwitchState(player.JumpState);
        }

        if(Input.GetKeyDown(inp.attackButton))
        {
            player.SwitchState(player.groundAttackState);
        }

        if (!player.IsGrounded())
        {
            player.SwitchState(player.FallState);
        }
    }
    public override void OnAnimationEnd(PlayerStateManager player)
    {

    }
    public override void OnTriggerEnter2D(PlayerStateManager player, Collider2D collision)
    {

    }

    private void SetVariables(PlayerStateManager player)
    {
        runSpeed = player.runSpeed;
        rayLength = player.rayLength;
        rb = player.rb;
        boxCollider2d = player.boxCollider2d;
        groundLayerMask = player.groundLayerMask;
        inp = player.inp;
    }
    private void TurningAround(PlayerStateManager player)
    {
        //Turning Around
        if (inp.Holding(PlayerInput.InputState.Left) && !player.facingLeft)
        {
            /*characterScale = -1;
            player.facingLeft = true;*/
            player.FlipPlayer(true);
        }
        if (inp.Holding(PlayerInput.InputState.Right) && player.facingLeft)
        {
            /*characterScale = 1;
            player.facingLeft = false;*/
            player.FlipPlayer(false);
        }
//        player.transform.localScale = new Vector3(characterScale, player.transform.localScale.y, player.transform.localScale.z);
    }

}
