using System.Collections;
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
        health = 500;
        shieldHealth = 1000;
        explosionScale = 0.2f;
        player = GameObject.Find("Player");
        healthGUI = Instantiate(GameController.Instance.healthGUI).GetComponent<HealthGUI>();
        healthGUI.target = gameObject;
        healthGUI.transform.SetParent(GameObject.FindGameObjectWithTag("MainCanvas").transform, false);
        healthGUI.updateText(shieldHealth.ToString(), health.ToString());
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

    public override void Damage(int amount, GameObject componentHit, float energyMultipler)
    {
        base.Damage(amount, componentHit, energyMultipler);
        healthGUI.updateText(shieldHealth.ToString(), health.ToString());
    }

    public override void LowerShields()
    {
        shield.SetActive(false);
    }

    public override void Kill()
    {
        Destroy(healthGUI.gameObject);
        base.Kill();
    }
}
