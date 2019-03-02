using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Ship
{
    public TurretBattery turretBattery;
    public SecondaryBattery missileBattery;
    public float primaryShotFrequency = 0.2f;

    public GameObject enemy;
    public HealthGUI healthGUI;

    private float sinceLastPrimaryShot;
    private float sinceLastSecondaryShot;

    public bool warpOverheated;
    public bool secondaryOverheated;

    void Start()
    {
        health = 1000;
        explosionScale = 0.4f;
        healthGUI = Instantiate(GameController.Instance.healthGUI).GetComponent<HealthGUI>();
        healthGUI.target = gameObject;
        healthGUI.transform.SetParent(GameObject.FindGameObjectWithTag("MainCanvas").transform, false);
        healthGUI.updateText(health.ToString());
    }

    void Update()
    {
        //Shooting logic
        if(enemy != null && !warpOverheated)
        {
            sinceLastPrimaryShot += Time.deltaTime;
            if(sinceLastPrimaryShot >= primaryShotFrequency)
            {
                turretBattery.fire(enemy.transform.position);
                sinceLastPrimaryShot = 0f;
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
            sinceLastPrimaryShot = 0f;
            enemy = GameObject.FindGameObjectWithTag("Enemy");
        }
    }

    public override void Damage(int amount, GameObject componentHit, float energyMultipler)
    {
        base.Damage(amount, componentHit, energyMultipler);
        healthGUI.updateText(health.ToString());
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
