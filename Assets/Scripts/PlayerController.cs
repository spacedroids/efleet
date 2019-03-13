using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Ship
{
    public PlayerShield pshield;
    public TurretBattery turretBattery;
    public SecondaryBattery missileBattery;
    public float primaryShotCooldownTime = 0.2f;

    public Transform enemy;
    public HealthGUI healthGUI;
    public TextoutGUI comboGUI;

    private bool openFirePrimary;
    private Vector3 fireZonePos;

    public bool autoFireMode = true;

    public bool warpOverheated;
    public bool secondaryOverheated;
    public bool primaryOverheated;
    public bool powerComboWarmingUp;
    public bool powerComboReady;

    private int primaryComboState;

    public override void Start()
    {
        health = 1000;
        explosionScale = 0.4f;
        healthGUI = Instantiate(GameController.Instance.healthGUI).GetComponent<HealthGUI>();
        healthGUI.target = gameObject;
        healthGUI.transform.SetParent(GameObject.FindGameObjectWithTag("MainCanvas").transform, false);
        healthGUI.updateHullHealth(health.ToString());
        comboGUI = Instantiate(GameController.Instance.textoutGUI).GetComponent<TextoutGUI>();
        comboGUI.target = gameObject;
        comboGUI.transform.SetParent(GameObject.FindGameObjectWithTag("MainCanvas").transform, false);
        pshield = gameObject.GetComponentInChildren<PlayerShield>();
        pshield.disable();
        base.Start();
    }

    Transform GetClosestEnemy(Transform[] enemies)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach(Transform potentialTarget in enemies)
        {
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if(dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }

        return bestTarget;
    }

    public override void Update()
    {
        //Shield logic
        if(Input.GetKeyDown("k")) {
            enableShield(1f);
        }

        //Shooting logic
        if(enemy != null)
        {
            if(autoFireMode && !primaryOverheated)
            {
                turretBattery.fire(enemy.position);
                primaryFireCooldown(primaryShotCooldownTime);
            }
            //Missiles controlled by button press
            if(GameController.Instance.missileButtonPressed || Input.GetKey("m")) {
                if(!secondaryOverheated)
                {
                    GameController.Instance.missileButtonPressed = false;
                    missileBattery.fire(enemy.position);
                    secondaryFireCooldown();
                }
            }

            float regularCooldown = 0.1f;
            float postComboRecover = 0.6f;
            //Primary fire controlled by button press
            if(Input.GetKeyDown("l"))
            {
                switch(primaryComboState) {
                    case 0: //No combo/starting state
                        if(!primaryOverheated) { 
                            fireAndCool(enemy.position, regularCooldown, 0.2f);
                            primaryComboState = 1;
                            comboGUI.updateIntensity(primaryComboState);
                            powerComboWarmUp(0.3f);
                        }
                        break;
                    case 1: //First shot already away
                        if(powerComboReady) { //Just right
                            fireAndCool(enemy.position, regularCooldown, 0.6f);
                            powerComboWarmUp(0.3f);
                            primaryComboState = 2; //Move to final combo stage
                            comboGUI.updateIntensity(primaryComboState);
                        }
                        else { //Too slow or too fast
                            fireAndCool(enemy.position, regularCooldown, 0.2f);
                            powerComboWarmUp(0.3f);
                        }
                        break;
                    case 2: //Second shot already away
                        if(powerComboReady) { //COMBO ACHIEVED
                            fireAndCool(enemy.position, postComboRecover, 1f);
                            missileBattery.fire(enemy.position);
                            primaryComboState = 0; //Combo achieved
                            comboGUI.updateIntensity(3);
                        }
                        else //shot attempt while overheated
                        {
                            fireAndCool(enemy.position, regularCooldown, 0.2f);
                            powerComboWarmUp(0.3f);
                            primaryComboState = 1; //Combo breaker
                            comboGUI.updateIntensity(primaryComboState);
                        }
                        break;
                }
            }
        }
        else if(enemy == null)
        {
            GameObject[] enemiesGO = GameObject.FindGameObjectsWithTag("Enemy");
            Transform[] enemies = new Transform[enemiesGO.Length];
            if(enemies.Length != 0) {
                for(int i=0;i<enemies.Length;i++) {
                    enemies[i] = enemiesGO[i].transform;
                }
            }
            enemy = GetClosestEnemy(enemies);
        }

        /* Fire zone gameplay test */
        //if(openFirePrimary && !primaryOverheated)
        //{
        //    turretBattery.fire(fireZonePos);
        //    primaryFireCooldown(primaryShotCooldownTime);
        //}

    }

    private void fireAndCool(Vector3 target, float coolingTime, float intensity) {
        if(!primaryOverheated) { 
            turretBattery.fire(target, intensity);
            primaryFireCooldown(coolingTime);
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

    /* Shield cooldown coroutine logic */
    private IEnumerator shieldTimerCoroutine;
    public void enableShield(float shieldDuration)
    {
        shieldsUp = true;
        shieldHealth = 1000;
        pshield.enable();
        if(shieldTimerCoroutine != null)
        {
            StopCoroutine(shieldTimerCoroutine);
        }
        shieldTimerCoroutine = shieldTimer(shieldDuration);
        StartCoroutine(shieldTimerCoroutine);
    }
    private IEnumerator shieldTimer(float shieldDuration)
    {
        yield return new WaitForSeconds(shieldDuration);
        pshield.disable();
        shieldsUp = false;
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
        yield return new WaitForSeconds(0f);
        warpOverheated = false;
    }

    /* Primary fire cool down coroutine logic */
    private IEnumerator primaryFireCooldownCoroutine;
    public void primaryFireCooldown(float coolingTime) {
        primaryOverheated = true;
        if(primaryFireCooldownCoroutine != null) { 
            StopCoroutine(primaryFireCooldownCoroutine);
        }
        primaryFireCooldownCoroutine = coolDownPrimary(coolingTime);
        StartCoroutine(primaryFireCooldownCoroutine);
    }
    private IEnumerator coolDownPrimary(float coolingTime) {
        yield return new WaitForSeconds(coolingTime);
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

    /* Power combo timer coroutine logic */
    private IEnumerator powerComboWarmUpCoroutine;
    public void powerComboWarmUp(float timer) {
        powerComboWarmingUp = true;
        if(powerComboWarmUpCoroutine != null)
        {
            StopCoroutine(powerComboWarmUpCoroutine);
        }
        powerComboWarmUpCoroutine = comboWarmup(timer);
        StartCoroutine(powerComboWarmUpCoroutine);
    }
    private IEnumerator comboWarmup(float duration) {
        yield return new WaitForSeconds(duration);
        powerComboWarmingUp = false;
        powerComboReadyCooldown(0.3f); //Start timer on combo ready, so that we can check if the user hits the button during this time window
    }

    /* Power combo timer coroutine logic */
    private IEnumerator powerComboReadyCoroutine;
    public void powerComboReadyCooldown(float timer)
    {
        powerComboReady = true;
        if(powerComboReadyCoroutine != null)
        {
            StopCoroutine(powerComboReadyCoroutine);
        }
        powerComboReadyCoroutine = comboReadyStart(timer);
        StartCoroutine(powerComboReadyCoroutine);
    }
    private IEnumerator comboReadyStart(float duration)
    {
        yield return new WaitForSeconds(duration);
        powerComboReady = false;
    }

}
