using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerDamage : MonoBehaviour
{
    [Header("Info")]
    public int percentage = 0;
    public int lives = 1;

    [Header("UI")]
    public TextMeshProUGUI percentText;
    public TextMeshProUGUI livesText;

    [Header("HitStun")]
    public GameObject hitEffect;
    public float hitstunModifier;
    [SerializeField] private float knockbackScale = 1;
    public PlayerStateManager pm;
    private ComboScript cs;
    
    void Start()
    {
        pm = GetComponent<PlayerStateManager>();
        cs = GetComponent<ComboScript>();
        percentText.text = percentage + "%";
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Hitbox"))
        {
            //collision.GetComponent<AudioSource>().Play
            var hb = collision.GetComponent<HitBox>();

            if (pm.currentState == pm.HitStunState)
            {
                cs.AddCombo(hb.damage);
            }
            else cs.ResetCombo(hb.damage);
            TakeDamage(hb);

            

        }
        if (collision.CompareTag("Blastzone"))
        {
            //Create Blast
            //Lose Life
            if (lives <= 1)
            {
                GameManagerScript gms = FindObjectOfType<GameManagerScript>();
                gms.RemovePlayer();
                gms.HandleLives();
                PlayerStateManager player = collision.GetComponent<PlayerStateManager>();
                player.extraJumps = player.extraJumpsValue;
            }
            else
            {
                SetLives(lives - 1);
            }

            // Disable player thingie so that way the other player can't see them falling down or the camera doens't follow them
            Respawn();
        }
    }
    public void TakeDamage(HitBox hitbox)
    {

        AddPercent(hitbox.damage);
        ApplyKnockback(hitbox);
        pm.SwitchState(pm.HitStunState);
        pm.stunTime = hitbox.knockback * hitstunModifier;
        //Get X and Y for Vector2 from angle and knockback
        var hitFX = Instantiate(hitEffect, hitbox.transform.position, hitEffect.transform.rotation);
    }

    private void ApplyKnockback(HitBox hitbox)
    {
        Rigidbody2D r = GetComponent<Rigidbody2D>();
        r.velocity = Vector2.zero;

        int p = percentage;
        int d = hitbox.damage;
        int w = 1; // might change in the future
        float s = hitbox.knockbackScaling; // knockback scaling = how much more knockback the attack gives after percent goes up
        float b = hitbox.knockback * 100;

        float kb = (float)(((((p / 10 + ((p * d) / 20)) * 1.4) + 18) * s) + b) * knockbackScale;

        /*r.AddForce(LaunchVec(hitbox.angle, hitbox.Controller().GetComponent<PlayerStateManager>().facingLeft) * kb);*/


        float degrees = hitbox.angle;

        if(hitbox.Controller().GetComponent<PlayerStateManager>().facingLeft)
        {
            degrees = -(hitbox.angle - 90) + 90;
        }

        Vector2 dir = (Vector2)(Quaternion.Euler(0, 0, degrees) * Vector2.right);

        print("Knockback: " + kb + ", Angle: " + degrees);
        r.AddForce(kb * dir);

    }
    public Vector2 LaunchVec(float angle, bool left)
    {
        if (left) angle = -(angle - 90) + 90;
        var rot = Quaternion.AngleAxis(angle, Vector3.forward);
        Vector2 xyzDir = rot * new Vector2(1f, 0f);
        print(xyzDir);
        return xyzDir.normalized;

    }
    private void Respawn()
    {
        transform.position = Vector3.zero;
        AddPercent(-percentage);                //Reset percentage
    }
    private void AddPercent(int amount)
    {
        percentage += amount;
        percentText.text = percentage + "%";
    }
    public void SetLives(int amount)
    {
        lives = amount;
        livesText.text = lives.ToString();
    }
}