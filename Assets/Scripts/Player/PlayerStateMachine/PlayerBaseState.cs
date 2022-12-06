using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState
{
    public abstract void EnterState(PlayerStateManager player);
    public abstract void UpdateState(PlayerStateManager player);
    public abstract void OnAnimationEnd(PlayerStateManager player);
    public abstract void OnTriggerEnter2D(PlayerStateManager player, Collider2D collision);
}

