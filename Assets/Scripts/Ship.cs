using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public int health;
    public float explosionScale = 1f;

    public void Damage(int amount)
    {
        health -= amount;
        if(health <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        GameObject explosion = Object.Instantiate(GameController.Instance.smallExplosionPrefab, gameObject.transform.position, gameObject.transform.rotation);
        explosion.transform.localScale = new Vector3(explosionScale, explosionScale, explosionScale);
        Destroy(explosion, 2);
        Destroy(gameObject);
    }
}
