using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Ship
{
    public TurretBattery turretBattery;
    public Vector3 shootAt;

    void Start()
    {
        health = 100;
        explosionScale = 0.4f;
        shootAt = new Vector3(0, 1, 0);
    }

    void Update()
    {
        GameObject enemy = GameObject.Find("Enemy");

        if(Input.GetMouseButtonDown(0)) {
            shootAt = enemy.transform.position;
            turretBattery.fire(shootAt);
        }
    }

}
