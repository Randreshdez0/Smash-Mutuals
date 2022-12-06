using UnityEngine;

public class HitBox : MonoBehaviour
{
    [Range(0,50)]
    public int damage;
    public float angle;
    public float knockback;
    public bool behindPlayer = false;
    public int knockbackScaling = 0;
    [SerializeField] GameObject controller;
    public float GetAngle()
    {
        Vector2 a = new Vector2(5, 2);

        float ang = Mathf.Atan2(a.y,a.x) * 180/Mathf.PI;


        return 1f;
    }
    public GameObject Controller()
    {
        return transform.parent.parent.parent.gameObject;
    }
}
