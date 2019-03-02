using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public int shieldHealth;
    public int maxShieldHealth;
    public float explosionScale = 1f;

    //Amount of damage, component to determine if shield or hull, energy multiplier to do extra/less damage to shields/hull
    public virtual void Damage(int amount, GameObject componentHit, float energyMultipler)
    {
        if(componentHit.CompareTag("Shield")) {
            //Energy multiplier > 1 means extra damage to shields
            shieldHealth -= (int)(amount * energyMultipler);
            if(shieldHealth > 0)
            {
                //If shields still hold, we're done
                return;
            }
            else
            {
                shieldHealth = 0;
                LowerShields();
            }
        } else {
            //Energy multiplier < 1 means extra damage to hull
            health -= (int)(amount / energyMultipler);
            if(health <= 0)
            {
                Kill();
            }
        }

    }

    /* Code to re introduce to deal with shield spillover if desired */
    //if(shieldHealth < 0) {
    //    //There is spillover damage
    //    health += shieldHealth; //shieldhealth is negative at this point
    //    if(health <= 0)
    //    {
    //        Kill();
    //    }
    //    shieldHealth = 0; //reset shields to 0
    //}

    public virtual void LowerShields() { }

    public virtual void Kill()
    {
        GameObject explosion = Object.Instantiate(GameController.Instance.smallExplosionPrefab, gameObject.transform.position, gameObject.transform.rotation);
        explosion.transform.localScale = new Vector3(explosionScale, explosionScale, explosionScale);
        Destroy(explosion, 2);
        Destroy(gameObject);
    }
}
