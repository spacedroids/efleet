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

    public void fire(Vector3 direction, float intensity=1f) {
        int salvoSize = (int)(turrets.Count * intensity);
        for(int i=0;i<salvoSize;i++) {
            Vector2 offset = Random.insideUnitCircle * 0.5f;
            Vector3 offsetV3 = new Vector3(offset.x, offset.y, 0);
            turrets[i].fire(direction + offsetV3);
        }
    }
}
