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

    void Start()
    {
        health = 100;
        explosionScale = 0.2f;
        enemy = GameObject.Find("Player");
        healthGUI = Instantiate(GameController.Instance.healthGUI).GetComponent<HealthGUI>();
        healthGUI.target = gameObject;
        healthGUI.transform.SetParent(GameObject.FindGameObjectWithTag("MainCanvas").transform, false);
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
        Destroy(healthGUI.gameObject);
        base.Kill();
    }
}
