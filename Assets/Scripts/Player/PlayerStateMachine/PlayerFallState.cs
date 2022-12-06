using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerBaseState
{
    private bool fastFalling = false;

    public override void EnterState(PlayerStateManager player)
    {
        player.ledgeGrabBox.SetActive(true);
        player.anim.ChangeAnimationState(player.anim.PLAYER_FALL);
    }
    public override void UpdateState(PlayerStateManager player)
    {
        if (player.inp.movX != 0)
        {
            player.rb.velocity = new Vector2(Mathf.Lerp(player.rb.velocity.x, player.inp.movX * player.runSpeed, .05f), player.rb.velocity.y);
        }
        if (player.IsGrounded())
        {
            fastFalling = false;
            player.rb.gravityScale = player.normalGravity;
            player.extraJumps = player.extraJumpsValue;
            player.SwitchState(player.RunState);
        }
        if (player.inp.movY < 0 && !fastFalling)
        {
            fastFalling = true;
            player.rb.gravityScale = player.fastGravity;
        }

        //Midair Jumps
        if (Input.GetKeyDown(player.inp.jumpButton) && player.extraJumps > 0)
        {
            player.extraJumps--;
            player.SwitchState(player.JumpState);
        }
        if(Input.GetKeyDown(player.inp.attackButton))
        {
            player.SwitchState(player.airAttackState);
        }
    }
    public override void OnAnimationEnd(PlayerStateManager player)
    {

    }
    public override void OnTriggerEnter2D(PlayerStateManager player, Collider2D collision)
    {

    }
}
