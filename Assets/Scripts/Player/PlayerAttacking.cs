/**
 * TODO: 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacking : MonoBehaviour
{
    public KeyCode attackButton;
    public KeyCode specialButton;

    public ContactFilter2D contactFilter;
    /*public HitBox jab, uTilt, sTilt, dTilt,
    nAir, uAir, bAir, fAir, dAir;*/

    private Vector3 attackPoint;
    float radius;

    public bool airAttacking = false;

    PlayerMovement player;
    PlayerInput inp;
    PlayerAnimationScript anim;
    private void Start()
    {
        player = GetComponent<PlayerMovement>();
        anim = GetComponent<PlayerAnimationScript>();
        inp = GetComponent<PlayerInput>();
    }


    private void Update()
    {
        if (!player.isAttacking && !player.inHitstun && Input.GetKeyDown(attackButton))
        {
            if (!player.IsGrounded()) //Midair
            {
                airAttacking = true;
                if (inp.Holding(PlayerInput.InputState.Right) && player.facingLeft 
                    || inp.Holding(PlayerInput.InputState.Left) && !player.facingLeft) // BAIR
                {
                    anim.ChangeAnimationState(anim.PLAYER_BAIR);
                }
                if (inp.Holding(PlayerInput.InputState.Left) && player.facingLeft 
                    || inp.Holding(PlayerInput.InputState.Right) && !player.facingLeft) // FAIR
                {
                    anim.ChangeAnimationState(anim.PLAYER_FAIR);
                }
                if(inp.Holding(PlayerInput.InputState.Up)) // UAIR
                {
                    anim.ChangeAnimationState(anim.PLAYER_UAIR);
                }
                if (player.StandingStill() && !inp.Holding(PlayerInput.InputState.Down) 
                    && !inp.Holding(PlayerInput.InputState.Up)) // NAIR
                {
                    anim.ChangeAnimationState(anim.PLAYER_NAIR);
                }
            }
            if (player.IsGrounded()) //Grounded
            {
                airAttacking = false;
                if (inp.Holding(PlayerInput.InputState.Down)) //DTILT
                {  
                    anim.ChangeAnimationState(anim.PLAYER_DTILT);
                }
                if (inp.Holding(PlayerInput.InputState.Right) 
                    || inp.Holding(PlayerInput.InputState.Left)) // FTILT
                {
                    anim.ChangeAnimationState(anim.PLAYER_FTILT);
                }
                if (player.StandingStill() && !inp.Holding(PlayerInput.InputState.Down) 
                    && !inp.Holding(PlayerInput.InputState.Up)) // JAB
                {
                    anim.ChangeAnimationState(anim.PLAYER_JAB);
                }
                if (inp.Holding(PlayerInput.InputState.Up)) //UTILT
                {
                    anim.ChangeAnimationState(anim.PLAYER_UTILT);
                }
            }
        }
    }
}