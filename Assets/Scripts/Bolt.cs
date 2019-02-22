using System.Collections;
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
        Destroy(gameObject);
        EnemyController damageRecipient = other.gameObject.GetComponent(typeof(EnemyController)) as EnemyController;
        if(damageRecipient != null)
        {
            damageRecipient.Damage(50);
        }
    }

    void Update()
    {
        
    }
}
