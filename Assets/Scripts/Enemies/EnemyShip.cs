﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : Ship
{
    public float shotFrequency = 0.5f;
    public TurretBattery turretBattery;
    private float sinceLastShot;
    public GameObject player;
    public HealthGUI healthGUI;
    public GameObject shield;

    public override void Start()
    {
        player = GameObject.Find("Player");
        healthGUI = Instantiate(GameController.Instance.healthGUI).GetComponent<HealthGUI>();
        healthGUI.target = gameObject;
        healthGUI.transform.SetParent(GameObject.FindGameObjectWithTag("MainCanvas").transform, false);
        turretBattery = gameObject.GetComponentInChildren<TurretBattery>();
        base.Start();
    }

    public override void Update()
    {
        if(player != null)
        {
            sinceLastShot += Time.deltaTime;
            if(sinceLastShot >= shotFrequency)
            {
                turretBattery.fire(player.transform.position);
                sinceLastShot = 0f;
            }
        }
        else
        {
            sinceLastShot = 0f;
            player = GameObject.Find("Player");
        }
    }

    public override void Damage(int amount, float energyMultipler)
    {
        base.Damage(amount, energyMultipler);
        healthGUI.updateText(shieldHealth.ToString(), health.ToString());
    }

    public override void LowerShields()
    {
        shield.SetActive(false);
        base.LowerShields();
    }

    public override void Kill()
    {
        Destroy(healthGUI.gameObject);
        base.Kill();
    }
}
