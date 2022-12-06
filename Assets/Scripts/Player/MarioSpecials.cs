using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MarioSpecials : FighterScript
{
    public GameObject fireball;
    public override void NeutralSpecial()
    {
        Instantiate(fireball);
        throw new System.NotImplementedException();
    }
    public override void DownSpecial()
    {

        throw new System.NotImplementedException();
    }
    public override void UpSpecial()
    {

        throw new System.NotImplementedException();
    }
}