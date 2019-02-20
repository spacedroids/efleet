using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltTurret : MonoBehaviour
{
    public static GameObject boltprefab;

    void Awake()
    {
        boltprefab = (GameObject)Resources.Load("Shots/Bolt");
    }

    public void fire(Vector3 direction)
    {
        GameObject newShot = Object.Instantiate(boltprefab, direction, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
