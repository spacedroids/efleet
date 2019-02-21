using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltTurret : MonoBehaviour
{
    public static GameObject boltprefab;
    public Vector3 rotateFix = new Vector3(0, 90, 0);

    void Awake()
    {
        boltprefab = (GameObject)Resources.Load("Shots/Bolt");
    }

    public void fire(Vector3 direction)
    {
        gameObject.transform.LookAt(direction);
        //HACK - not sure why but lookat is always off by 90deg on X axis. A problem for another day.
        gameObject.transform.Rotate(rotateFix);
        GameObject newShot = Object.Instantiate(boltprefab, gameObject.transform.position, gameObject.transform.rotation);
    }

    void Update()
    {
    }
}
