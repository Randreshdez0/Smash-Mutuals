using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxScript : MonoBehaviour
{
/*    public GameObject hitboxTest;
    void CreateHitBox(HitBox hitbox)
    {
        if (!hitbox.behindPlayer)
        {
            if (GetComponent<PlayerMovement>().facingLeft)
            {
                hitbox.offset.x = -Mathf.Abs(hitbox.offset.x);
                hitbox.direction.x = -Mathf.Abs(hitbox.direction.x);
            }
            else
            {
                hitbox.offset.x = Mathf.Abs(hitbox.offset.x);
                hitbox.direction.x = Mathf.Abs(hitbox.direction.x);
            }
        } 
        else if (hitbox.behindPlayer)
        {
            if (GetComponent<PlayerMovement>().facingLeft)
            {
                hitbox.offset.x = Mathf.Abs(hitbox.offset.x);
                hitbox.direction.x = Mathf.Abs(hitbox.direction.x);
            }
            else
            {
                hitbox.offset.x = -Mathf.Abs(hitbox.offset.x);
                hitbox.direction.x = -Mathf.Abs(hitbox.direction.x);
            }
        }
        var newHitBox = Instantiate(hitboxTest, transform.position + hitbox.offset, Quaternion.identity);
        // Create CircleCast instead of gameobject

        newHitBox.transform.parent = gameObject.transform;
        newHitBox.transform.localScale *= hitbox.size;
        newHitBox.GetComponent<HitboxData>().hitboxdata = hitbox;
        Destroy(newHitBox, hitbox.timeActive * Time.deltaTime);*/
    
}