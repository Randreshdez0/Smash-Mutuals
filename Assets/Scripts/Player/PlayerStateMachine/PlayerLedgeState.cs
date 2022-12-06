using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLedgeState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        player.anim.ChangeAnimationState(player.anim.PLAYER_LEDGEGRAB);
        player.boxCollider2d.enabled = false;

        player.ledgeGrabBox.SetActive(false);
        player.rb.gravityScale = 0f;
        player.rb.velocity = Vector2.zero;
        player.transform.GetChild(1).gameObject.SetActive(false);
    }
    public override void UpdateState(PlayerStateManager player)
    {

        if (Input.GetKeyDown(player.inp.jumpButton))
        {
            player.boxCollider2d.enabled = true;
            player.SwitchState(player.JumpState);
        }
        if (player.inp.Holding(PlayerInput.InputState.Down))
        {
            player.boxCollider2d.enabled = true;
            player.SwitchState(player.FallState);
        }

    }

    public override void OnTriggerEnter2D(PlayerStateManager player, Collider2D collision)
    {

    }

    public override void OnAnimationEnd(PlayerStateManager player)
    {
        
    }

}
