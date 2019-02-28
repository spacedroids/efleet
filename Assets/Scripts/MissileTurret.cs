using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileTurret : Turret
{
    public override void fire(Vector3 direction)
    {
        Debug.Log("fire missile");
        gameObject.transform.LookAt(direction, Vector3.back);
        GameObject newShot = Object.Instantiate(GameController.Instance.missilePrefab, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(newShot, 5f); //Auto expire after 5s
    }
}
