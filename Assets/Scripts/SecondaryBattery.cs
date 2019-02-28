using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryBattery : MonoBehaviour
{
    public List<MissileTurret> turrets;

    private void Start()
    {
        foreach(Transform child in gameObject.transform)
        {
            if(child.gameObject.CompareTag("Turret"))
            {
                turrets.Add(child.gameObject.GetComponent<MissileTurret>());
            }
        }
    }

    public void fire(Vector3 direction)
    {
        foreach(MissileTurret turret in turrets)
        {
            turret.fire(direction);
        }
    }

}
