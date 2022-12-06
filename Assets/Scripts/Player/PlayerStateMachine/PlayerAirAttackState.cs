using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirAttackState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {

        player.ledgeGrabBox.SetActive(false);

        if (player.inp.Holding(PlayerInput.InputState.Right) && player.facingLeft
                    || player.inp.Holding(PlayerInput.InputState.Left) && !player.facingLeft) // BAIR
        {
            player.anim.ChangeAnimationState(player.anim.PLAYER_BAIR);
        }
        if (player.inp.Holding(PlayerInput.InputState.Left) && player.facingLeft
            || player.inp.Holding(PlayerInput.InputState.Right) && !player.facingLeft) // FAIR
        {
            player.anim.ChangeAnimationState(player.anim.PLAYER_FAIR);
        }
        if (player.inp.Holding(PlayerInput.InputState.Up)) // UAIR
        {
            player.anim.ChangeAnimationState(player.anim.PLAYER_UAIR);
        }
        if (player.inp.Holding(PlayerInput.InputState.Down)) // DAIR
        {
            player.anim.ChangeAnimationState(player.anim.PLAYER_DAIR);
        }
        if (player.inp.movX == 0 && player.inp.movY == 0) // NAIR
        {
            player.anim.ChangeAnimationState(player.anim.PLAYER_NAIR);
        }
    }
    public override void UpdateState(PlayerStateManager player)
    {
        if (player.inp.movX != 0)
        {
            player.rb.velocity = new Vector2(Mathf.Lerp(player.rb.velocity.x, player.inp.movX * player.runSpeed, .05f), player.rb.velocity.y);
        }
        
        if (player.IsGrounded())
        {
            player.SwitchState(player.RunState);
        }
        //Eventually will be landing
    }

    public override void OnAnimationEnd(PlayerStateManager player)
    {
        player.ledgeGrabBox.SetActive(true);
        if(player.IsGrounded())
        {
            player.SwitchState(player.RunState);
        }
        player.SwitchState(player.FallState);
    }

    public override void OnTriggerEnter2D(PlayerStateManager player, Collider2D collision)
    {

    }
    
}
