﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt : MonoBehaviour
{
    public Rigidbody _rigidBody;
    public float speed = 3;

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        _rigidBody.velocity = transform.forward * speed;
        _rigidBody.angularVelocity = Vector3.zero;
    }

    void OnCollisionEnter(Collision other)
    {
        EnemyController enemy = other.gameObject.GetComponent(typeof(EnemyController)) as EnemyController;
        PlayerController player = other.gameObject.GetComponentInParent(typeof(PlayerController)) as PlayerController;
        if(other.gameObject.CompareTag("Shield"))
        {
            //The shield gameobject (with its collider) is a child of the enemy ship where the script is attached
            enemy = other.gameObject.GetComponentInParent<EnemyController>() as EnemyController;
        }
        if(enemy != null)
        {
            enemy.Damage(50);
        }
        if(player != null)
        {
            player.Damage(50);
        }
        //Destroy self
        Destroy(gameObject);
    }

    void Update()
    {
        
    }
}
