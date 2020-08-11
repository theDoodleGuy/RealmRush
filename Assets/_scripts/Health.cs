using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float healthPoints = 2f;
    [SerializeField] ParticleSystem hitParticleFX = null;
    [SerializeField] ParticleSystem deathParticleFX = null;
    [SerializeField] ParticleSystem explodeParticleFX = null;

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        if (healthPoints < 1)
        {
            Death();
        }
    }

    void ProcessHit()
    {
        healthPoints -= 1;
        hitParticleFX.Play();
    }

    void Death()
    {
        var deathfx = Instantiate(deathParticleFX, transform.position, Quaternion.identity);
        deathfx.Play();
        Destroy(gameObject);
    }

    public void Explode()
    {
        var explodefx = Instantiate(explodeParticleFX, transform.position, Quaternion.identity);
        explodefx.Play();
        FindObjectOfType<PlayerHealth>().ProcessHit(healthPoints);
        Destroy(explodefx.gameObject, explodefx.main.duration);
        Destroy(gameObject);
    }
}
