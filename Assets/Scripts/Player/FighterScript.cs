using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Fighter", menuName = "Fighter")]
public abstract class FighterScript : ScriptableObject
{
    public float fighterJumpStrength;
    public float fighterRunSpeed;
    public float fighterExtraJumps;
    public Sprite selectSprite;

    public abstract void NeutralSpecial();
    public abstract void UpSpecial();
    public abstract void DownSpecial();

}