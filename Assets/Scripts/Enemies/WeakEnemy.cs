using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakEnemy : EnemyShip
{
    public override void Start()
    {
        base.Start();

        health = 1;
        shieldHealth = 0;
        shieldsUp = false;
        explosionScale = 0.2f;
        healthGUI.updateHullHealth(health.ToString());
    }

}
