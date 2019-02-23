using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Ship
{
    public TurretBattery turretBattery;
    public float shotFrequency = 0.5f;

    public GameObject enemy;
    private float sinceLastShot;

    void Start()
    {
        health = 1000;
        explosionScale = 0.4f;
    }

    void Update()
    {
        //Shooting logic
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
            enemy = GameObject.FindGameObjectWithTag("Enemy");
        }

    }

}
