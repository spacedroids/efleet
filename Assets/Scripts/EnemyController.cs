using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Ship
{
    public float shotFrequency = 0.5f;
    public TurretBattery turretBattery;
    private float sinceLastShot;
    public GameObject enemy;

    void Start()
    {
        health = 100;
        explosionScale = 0.2f;
        enemy = GameObject.Find("Player");
    }

    void Update()
    {
        if(enemy != null)
        {
            sinceLastShot += Time.deltaTime;
            if(sinceLastShot >= shotFrequency)
            {
                turretBattery.fire(enemy.transform.position);
                sinceLastShot = 0f;
            }
        }
        else
        {
            sinceLastShot = 0f;
            enemy = GameObject.Find("Player");
        }
    }

    public override void Kill()
    {
        if(--GameController.Instance.enemiesAlive < 0) { 
            GameController.Instance.enemiesAlive = 0;
        }
        base.Kill();
    }
}
