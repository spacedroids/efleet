﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Ship
{
    public TurretBattery turretBattery;
    public float shotFrequency = 0.2f;

    public GameObject enemy;
    public HealthGUI healthGUI;

    private float sinceLastShot;

    public bool warpOverheated;

    void Start()
    {
        health = 1000;
        explosionScale = 0.4f;
        healthGUI = Instantiate(GameController.Instance.healthGUI).GetComponent<HealthGUI>();
        healthGUI.target = gameObject;
        healthGUI.transform.SetParent(GameObject.FindGameObjectWithTag("MainCanvas").transform, false);
        healthGUI.updateText(health.ToString());
    }

    void Update()
    {
        //Shooting logic
        if(enemy != null && !warpOverheated)
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

    public override void Damage(int amount)
    {
        base.Damage(amount);
        healthGUI.updateText(health.ToString());
    }

    private IEnumerator warpCooldownCoroutine;
    public void warpCooldown() {
        warpCooldownCoroutine = coolDownWarp();
        StartCoroutine(warpCooldownCoroutine);
    }

    private IEnumerator coolDownWarp()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("cooldown over");
        warpOverheated = false;
    }
}
