using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{


    public override void EnterState(PlayerStateManager player)
    {
        Jump(player);    
    }
    public override void UpdateState(PlayerStateManager player)
    {
        
        if (player.inp.movX != 0)
        {
            player.rb.velocity = new Vector2(Mathf.Lerp(player.rb.velocity.x, player.inp.movX * player.runSpeed, .05f), player.rb.velocity.y);
        }
        //Midair Jumps
        if (Input.GetKeyDown(player.inp.jumpButton) && player.extraJumps > 0)
        {
            Jump(player);
            player.extraJumps--;
            //var particle = Instantiate(jumpParticle, new Vector2(player.transform.position.x, player.transform.position.y - player.boxCollider2d.size.y), Quaternion.identity);
            //Destroy(particle, 1.1f);
        }

        /*if (!isFalling && !isAttacking && rb.velocity.y > 0.1f)
          {
              player.anim.ChangeAnimationState(player.anim.PLAYER_JUMP);
          }*/

        if (player.rb.velocity.y < 0.1f)
        {
            player.ledgeGrabBox.SetActive(true);
            player.SwitchState(player.FallState);
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
    void Jump(PlayerStateManager player)
    {
        player.rb.velocity = new Vector2(player.rb.velocity.x, 0);
        player.rb.AddForce(Vector2.up * player.jumpStrength, ForceMode2D.Impulse);
        player.rb.gravityScale = player.normalGravity;
        player.anim.ChangeAnimationState(player.anim.PLAYER_JUMP);
    }
}
