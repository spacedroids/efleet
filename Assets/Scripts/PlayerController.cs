using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Ship
{
    public TurretBattery turretBattery;
    public SecondaryBattery missileBattery;
    public float primaryShotCooldownTime = 0.2f;

    public GameObject enemy;
    public HealthGUI healthGUI;

    private bool openFirePrimary;
    private Vector3 fireZonePos;

    public bool autoFireMode = true;

    public bool warpOverheated;
    public bool secondaryOverheated;
    public bool primaryOverheated;

    public override void Start()
    {
        health = 1000;
        explosionScale = 0.4f;
        healthGUI = Instantiate(GameController.Instance.healthGUI).GetComponent<HealthGUI>();
        healthGUI.target = gameObject;
        healthGUI.transform.SetParent(GameObject.FindGameObjectWithTag("MainCanvas").transform, false);
        healthGUI.updateHullHealth(health.ToString());
        base.Start();
    }

    public override void Update()
    {
        //Shooting logic
        if(enemy != null && !warpOverheated)
        {
            if(autoFireMode && !primaryOverheated)
            {
                turretBattery.fire(enemy.transform.position);
                primaryFireCooldown();
            }
            //Missiles controlled by button press
            if(GameController.Instance.missileButtonPressed || Input.GetKey("m")) {
                if(!secondaryOverheated)
                {
                    GameController.Instance.missileButtonPressed = false;
                    missileBattery.fire(enemy.transform.position);
                    secondaryFireCooldown();
                }
            }
        }
        else
        {
            enemy = GameObject.FindGameObjectWithTag("Enemy");
        }

        /* Fire zone gameplay test */
        if(openFirePrimary && !primaryOverheated)
        {
            turretBattery.fire(fireZonePos);
            primaryFireCooldown();
        }

    }

    public override void Damage(int amount, float energyMultipler)
    {
        base.Damage(amount, energyMultipler);
        healthGUI.updateHullHealth(health.ToString());
    }

    public void shootAtZone(Vector3 target) {
        openFirePrimary = true;
        fireZonePos = target;
    }

    public void stopZoneShooting()
    {
        openFirePrimary = false;
    }

    /* Warp cool down coroutine logic */
    private IEnumerator warpCooldownCoroutine;
    public void warpCooldown() {
        warpOverheated = true;
        warpCooldownCoroutine = coolDownWarp();
        StartCoroutine(warpCooldownCoroutine);
    }
    private IEnumerator coolDownWarp()
    {
        yield return new WaitForSeconds(1f);
        warpOverheated = false;
    }

    /* Primary fire cool down coroutine logic */
    private IEnumerator primaryFireCooldownCoroutine;
    public void primaryFireCooldown() {
        primaryOverheated = true;
        primaryFireCooldownCoroutine = coolDownPrimary();
        StartCoroutine(primaryFireCooldownCoroutine);
    }
    private IEnumerator coolDownPrimary() {
        yield return new WaitForSeconds(primaryShotCooldownTime);
        primaryOverheated = false;
    }

    /* Secondary fire cool down coroutine logic */
    private IEnumerator secondaryFireCooldownCoroutine;
    public void secondaryFireCooldown()
    {
        secondaryOverheated = true;
        secondaryFireCooldownCoroutine = coolDownSecondary();
        StartCoroutine(secondaryFireCooldownCoroutine);
    }
    private IEnumerator coolDownSecondary()
    {
        yield return new WaitForSeconds(0.5f);
        secondaryOverheated = false;
    }


}
