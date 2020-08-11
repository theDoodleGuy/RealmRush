using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float healthPoints = 10;
    [SerializeField] Text healthText = null;


    private void Start()
    {
        UpdatePlayerHealthText();
    }

    private void UpdatePlayerHealthText()
    {
        healthText.text = "Player Health : " + healthPoints.ToString();
    }

    public void ProcessHit(float damage)
    {
        if (damage >= healthPoints)
        {
            healthPoints = 0f;
            UpdatePlayerHealthText();
            PlayerDeath();
        }
        else
        {
            healthPoints -= damage;
            UpdatePlayerHealthText();
            Debug.Log("hit for " + damage + "hp");   
        }
    }

    void PlayerDeath()
    {
        Debug.Log("Player died");
    }
}
