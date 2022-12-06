using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationScript : MonoBehaviour
{
    Animator animator;

    private string currentState;

    public string PLAYER_IDLE = "Player_idle";
    public string PLAYER_RUN = "Player_run";
    public string PLAYER_JUMP = "Player_jump";
    public string PLAYER_FALL = "Player_fall";
    public string PLAYER_FTILT = "Player_forward_tilt";
    public string PLAYER_UTILT = "Player_up_tilt";
    public string PLAYER_DTILT = "Player_down_tilt";
    public string PLAYER_JAB = "Player_nuetral_attack";
    public string PLAYER_BAIR = "Player_back_air";
    public string PLAYER_NAIR = "Player_neutral_air";
    public string PLAYER_UAIR = "Player_up_air";
    public string PLAYER_FAIR = "Player_forward_air";
    public string PLAYER_DAIR = "Player_down_air";
    public string PLAYER_HITSTUN = "Player_hit_stun";
    public string PLAYER_LEDGEGRAB = "Player_ledgegrab";
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void ChangeAnimationState(string newState)
    {
        if(newState == currentState) return;

        animator.Play(newState);

        currentState = newState;
    }
}
