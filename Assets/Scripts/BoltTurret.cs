using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltTurret : MonoBehaviour
{
    public static GameObject boltprefab;
    public Vector3 rotateFix = new Vector3(-90, 0, 0);

    void Awake()
    {
        boltprefab = (GameObject)Resources.Load("Shots/Bolt");
    }

    public void fire(Vector3 direction)
    {
        gameObject.transform.LookAt(direction, Vector3.back);
        GameObject newShot = Object.Instantiate(boltprefab, gameObject.transform.position, gameObject.transform.rotation);
    }

    void Update()
    {
    }
}
