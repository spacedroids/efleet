using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumEnemy : EnemyShip
{
    public override void Start()
    {
        base.Start();

        health = 500;
        shieldHealth = 1000;
        shieldsUp = true;
        explosionScale = 0.2f;
        healthGUI.updateText(shieldHealth.ToString(), health.ToString());
    }
}
