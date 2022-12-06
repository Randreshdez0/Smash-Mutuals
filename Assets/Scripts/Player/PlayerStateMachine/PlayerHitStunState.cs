using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitStunState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player)
    {
        player.ledgeGrabBox.SetActive(false);
        //StartCoroutine(SpawnParticles());
        player.anim.ChangeAnimationState(player.anim.PLAYER_HITSTUN);
    }
    public override void UpdateState(PlayerStateManager player)
    {
        if (player.stunTime > 0)
        {
            player.stunTime -= Time.deltaTime;
        }
        if (player.stunTime <= 0)
        {
            if (!player.IsGrounded())
            {
                player.SwitchState(player.FallState);
            }
            else
            {
                player.SwitchState(player.RunState);
            }
        }
    }

    public override void OnTriggerEnter2D(PlayerStateManager player, Collider2D collision)
    {

    }

    public override void OnAnimationEnd(PlayerStateManager player)
    {
        throw new System.NotImplementedException();
    }

/*    private IEnumerator SpawnParticles()
    {
        while (true)
        {
            var particle = Instantiate(stunParticles, transform.position, Quaternion.identity);
            Destroy(particle, 1f);
            yield
            return new WaitForSeconds(0.3f);
        }
    }*/
}
