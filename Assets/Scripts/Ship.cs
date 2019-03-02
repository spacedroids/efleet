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

    public virtual void Damage(int amount)
    {
        shieldHealth -= amount; //Damage shields first
        if(shieldHealth > 0) {
            //If shields still hold, we're done
            return;
        } else {
            LowerShields();
            if(shieldHealth < 0) {
                //There is spillover damage
                health += shieldHealth; //shieldhealth is negative at this point
                if(health <= 0)
                {
                    Kill();
                }
                shieldHealth = 0; //reset shields to 0
            }
        }

    }

    public virtual void LowerShields() { }

    public virtual void Kill()
    {
        GameObject explosion = Object.Instantiate(GameController.Instance.smallExplosionPrefab, gameObject.transform.position, gameObject.transform.rotation);
        explosion.transform.localScale = new Vector3(explosionScale, explosionScale, explosionScale);
        Destroy(explosion, 2);
        Destroy(gameObject);
    }
}
