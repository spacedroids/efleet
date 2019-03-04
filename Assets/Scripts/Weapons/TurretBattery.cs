using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBattery : MonoBehaviour
{
    public BoltTurret boltTurret;
    public List<BoltTurret> turrets;

    private void Start()
    {
        foreach(Transform child in gameObject.transform)
        {
            if(child.gameObject.CompareTag("Turret"))
            {
                turrets.Add(child.gameObject.GetComponent<BoltTurret>());
            }
        }
    }

    public void fire(Vector3 direction) {
        foreach(BoltTurret turret in turrets) {
            Vector2 offset = Random.insideUnitCircle * 0.5f;
            Vector3 offsetV3 = new Vector3(offset.x, offset.y, 0);
            turret.fire(direction + offsetV3);
        }
    }
}
