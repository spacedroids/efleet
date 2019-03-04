using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBolt : Bolt
{
    public override void Start()
    {
        speed = 7;
        energyDamageBonus = 5f;
        damage = 20;
        base.Start();
    }
}
