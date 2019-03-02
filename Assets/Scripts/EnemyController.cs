using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Ship
{
    public float shotFrequency = 0.5f;
    public TurretBattery turretBattery;
    private float sinceLastShot;
    public GameObject enemy;
    public HealthGUI healthGUI;
    public GameObject shield;

    void Start()
    {
        health = 500;
        shieldHealth = 1000;
        explosionScale = 0.2f;
        enemy = GameObject.Find("Player");
        healthGUI = Instantiate(GameController.Instance.healthGUI).GetComponent<HealthGUI>();
        healthGUI.target = gameObject;
        healthGUI.transform.SetParent(GameObject.FindGameObjectWithTag("MainCanvas").transform, false);
        healthGUI.updateText(health.ToString());
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

    public override void Damage(int amount, GameObject componentHit, float energyMultipler)
    {
        base.Damage(amount, componentHit, energyMultipler);
        healthGUI.updateText(health.ToString());
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
