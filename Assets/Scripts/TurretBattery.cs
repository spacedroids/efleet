using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBattery : MonoBehaviour
{
    public BoltTurret boltTurret;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void fire(Vector3 direction) {
        boltTurret.fire(direction);
    }
}
