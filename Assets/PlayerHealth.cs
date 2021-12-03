using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    private float health = 0f;
    [SerializeField] TMP_Text text;
    [SerializeField] private float maxHealth = 100f;

    private void Start() {
        health = maxHealth;
            text.text = health.ToString();

    }

    public void UpdateHealth(float mod)
    {
        health += mod;
        if(health > maxHealth)
        {
            health = maxHealth;
        }else if (health <= 0f)
        {
            health = 0f;
            Debug.Log("Die");
        }
        text.text = health.ToString();
    }
}
