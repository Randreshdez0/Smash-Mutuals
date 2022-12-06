using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboScript : MonoBehaviour
{
    public PlayerMovement pm;
    public PlayerDamage pd;

    int combo = 0;
    int comboPercent = 0;
    private void Start()
    {
        pm = GetComponent<PlayerMovement>();
        pd = GetComponent<PlayerDamage>();
    }
    public void AddCombo(int percent)
    {
        combo++;
        comboPercent += percent;
        print(combo + " HIT COMBO FOR " + comboPercent + "%");
    }
    public void ResetCombo(int percent)
    {
        combo = 1;
        comboPercent = percent;
        //print(combo + " HIT COMBO FOR " + comboPercent + "%");
    }
}
