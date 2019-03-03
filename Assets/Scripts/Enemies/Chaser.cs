using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : EnemyShip
{
    public override void Update()
    {
        if(player != null)
        {
            gameObject.transform.LookAt(player.transform, Vector3.back);
            _rigidbody.AddForce(transform.forward * 5);
        }
        base.Update();
    }
}
