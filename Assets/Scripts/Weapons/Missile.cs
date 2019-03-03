using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public Rigidbody _rigidBody;
    protected float speed = 0.2f;
    protected int damage = 200;
    protected float energyDamageBonus = 0.1f;

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        _rigidBody.velocity = transform.forward * speed * 3;
        _rigidBody.angularVelocity = Vector3.zero;
    }

    private void Update()
    {
        _rigidBody.velocity += transform.forward * speed;
    }

    void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
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
    }
}
