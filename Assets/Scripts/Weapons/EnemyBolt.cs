using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBolt : Bolt
{
    public override void Start()
    {
        speed = 0.5f;
        energyDamageBonus = 5f;
        damage = 20;
        base.Start();
    }
}
