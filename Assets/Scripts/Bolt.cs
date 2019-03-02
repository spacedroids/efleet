using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt : MonoBehaviour
{
    public Rigidbody _rigidBody;
    private float speed = 2;
    private float energyDamageBonus = 5f;
    private int damage = 20;

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
        EnemyController enemy = other.gameObject.GetComponentInParent(typeof(EnemyController)) as EnemyController;
        PlayerController player = other.gameObject.GetComponentInParent(typeof(PlayerController)) as PlayerController;
        if(enemy != null)
        {
            enemy.Damage(damage, other.gameObject, energyDamageBonus);
        }
        if(player != null)
        {
            player.Damage(damage, other.gameObject, energyDamageBonus);
        }
        //Destroy self
        Destroy(gameObject);
    }

    void Update()
    {
        
    }
}
