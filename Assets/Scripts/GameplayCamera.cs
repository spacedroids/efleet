using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayCamera : MonoBehaviour
{
    protected GameObject player;

    //Screen shake vars
    public float shakeAmount = 0.1f;
    public float decreaseFactor = 5.0f;
    public float maxShake = 200;
    public float shakeTimer;

    public void Start()
    {
        player = GameObject.Find("Player");
        StartFollow(player.transform);
    }

    // Transforms to act as start and end markers for the journey.
    public Transform startMarker;
    public Vector3 endMarker;

    // Movement speed in units/sec.
    public float speed = 1.0F;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;

    public void StartFollow(Transform target)
    {
        endMarker = target.position;
        endMarker.z = transform.position.z;
        startMarker = transform;

        // Keep a note of the time the movement started.
        startTime = Time.time;

        // Calculate the journey length.
        journeyLength = Vector3.Distance(startMarker.position, endMarker);
    }

    public void screenShake(float amount)
    {
        amount = Mathf.Clamp(amount, -maxShake, maxShake);
            shakeAmount = amount * 0.01f;
            shakeTimer = 2.0f;
            Debug.Log("shake " + shakeAmount);
    }

    void LateUpdate()
    {
        if(shakeTimer > 0)
        {
            transform.localPosition += Random.insideUnitSphere * shakeAmount;
            shakeTimer -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeTimer = 0.0f;
        }
    }

    // Follows the target position like with a spring
    void Update()
    {
        // Distance moved = time * speed.
        float distCovered = (Time.time - startTime) * speed;

        // Fraction of journey completed = current distance divided by total distance.
        float fracJourney = distCovered / journeyLength;

        // Set our position as a fraction of the distance between the markers.
        transform.position = Vector3.Lerp(startMarker.position, endMarker, fracJourney);

        //if(fracJourney > 0.9)
        //{
        //    StartFollow(player.transform);
        //}
    }
}
