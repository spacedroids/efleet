using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt : MonoBehaviour
{
    public Rigidbody _rigidBody;
    public Transform _transform;
    public float speed = 3;

    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
    }

    void Start()
    {
        _rigidBody.velocity = _transform.up * speed;
        _rigidBody.angularVelocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
