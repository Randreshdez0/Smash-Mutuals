using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ledge : MonoBehaviour
{
    GameObject hangingPlayer;
    float t;
    float timeToSnap = .1f;
    public bool leftSided;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("LedgeGrab"))
        {
            hangingPlayer = collision.transform.parent.gameObject;
            AttachPlayer();
        }
    }
    public void AttachPlayer()
    {
        //Move player to grab point
        PlayerStateManager player = hangingPlayer.GetComponent<PlayerStateManager>();
        if (player.currentState == player.airAttackState)
        {
            return;
        }

        player.SwitchState(player.LedgeState);
        player.FlipPlayer(!leftSided);
        StartCoroutine(MoveToPosition(hangingPlayer.transform, this.transform.position, timeToSnap));

        //Make player to into ledge state
            //invulnerable and can't fall
    }

    public IEnumerator MoveToPosition(Transform transform, Vector3 position, float timeToMove)
    {
        var currentPos = transform.position;
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(currentPos, position, t);
            yield return null;
        }
    }
}
