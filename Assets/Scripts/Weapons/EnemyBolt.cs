using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBolt : Bolt
{
    public override void Start()
    {
        speed = 2;
        energyDamageBonus = 5f;
        damage = 20;
        base.Start();
    }
}
