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
    [SerializeField] AudioClip hitSoundFX = null;
    [SerializeField] AudioClip deathSoundFX = null;
    [SerializeField] float deathSFXVolume = 1f;

    AudioSource enemyAudioSource;

    private void Start()
    {
        enemyAudioSource = GetComponent<AudioSource>();
    }

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
        enemyAudioSource.PlayOneShot(hitSoundFX);
        healthPoints -= 1;
        hitParticleFX.Play();
    }

    void Death()
    {
        var deathfx = Instantiate(deathParticleFX, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(deathSoundFX, Camera.main.transform.position, deathSFXVolume);
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
