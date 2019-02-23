using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int health = 100;
    public float shotFrequency = 0.5f;
    public TurretBattery turretBattery;
    private float sinceLastShot;
    public GameObject enemy;

    void Start()
    {
        enemy = GameObject.Find("Player");
    }

    void Update()
    {
        sinceLastShot += Time.deltaTime;
        if(sinceLastShot >= shotFrequency) {
            Debug.Log("Firing at" + enemy);
            Vector3 pos = enemy.transform.position;
            turretBattery.fire(pos);
            sinceLastShot = 0f;
        }
    }

    public void Damage(int amount) {
        health -= amount;
        if(health <= 0) {
            Kill();    
        }
    }

    public void Kill() {
        Destroy(gameObject);
    }
}
