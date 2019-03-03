using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt : MonoBehaviour
{
    public Rigidbody _rigidBody;
    protected float speed = 2;
    protected float energyDamageBonus = 5f;
    protected int damage = 20;

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    public virtual void Start()
    {
        _rigidBody.velocity = transform.forward * speed;
        _rigidBody.angularVelocity = Vector3.zero;
    }

    public virtual void OnCollisionEnter(Collision other)
    {
        EnemyShip enemy = other.gameObject.GetComponentInParent(typeof(EnemyShip)) as EnemyShip;
        PlayerController player = other.gameObject.GetComponentInParent(typeof(PlayerController)) as PlayerController;
        if(enemy != null)
        {
            enemy.Damage(damage, energyDamageBonus);
        }
        if(player != null)
        {
            player.Damage(damage, energyDamageBonus);
        }
        //Destroy self
        Destroy(gameObject);
    }
}
