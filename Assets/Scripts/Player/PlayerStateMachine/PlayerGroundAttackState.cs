using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundAttackState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        player.ledgeGrabBox.SetActive(false);
        player.rb.velocity /= 2;

        if (player.inp.Holding(PlayerInput.InputState.Down)) //DTILT
        {
            player.anim.ChangeAnimationState(player.anim.PLAYER_DTILT);
        }
        if (player.inp.Holding(PlayerInput.InputState.Right)
            || player.inp.Holding(PlayerInput.InputState.Left)) // FTILT
        {
            player.anim.ChangeAnimationState(player.anim.PLAYER_FTILT);
        }
        if (player.inp.movX == 0 && player.inp.movY == 0) // JAB
        {
            player.anim.ChangeAnimationState(player.anim.PLAYER_JAB);
        }
        if (player.inp.Holding(PlayerInput.InputState.Up)) //UTILT
        {
            player.anim.ChangeAnimationState(player.anim.PLAYER_UTILT);
        }
    }
    public override void UpdateState(PlayerStateManager player)
    {

    }

    public override void OnAnimationEnd(PlayerStateManager player)
    {
        player.SwitchState(player.RunState);
    }

    public override void OnTriggerEnter2D(PlayerStateManager player, Collider2D collision)
    {

    }
    
}
