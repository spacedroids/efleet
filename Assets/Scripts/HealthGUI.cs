using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthGUI : MonoBehaviour
{
    public GameObject target;

    void Start()
    {
        
    }

    void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(target.transform.position);
    }
}
