using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int health = 100;

    void Start()
    {

    }

    void Update()
    {

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
