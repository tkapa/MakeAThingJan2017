﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public string attackKey = "a", defendKey = "d";

    public float movementSpeed = 2.0f;

    public float health = 2, damage = 1, parryMultiplier = 2;

    public float parryTime = 0.9f;
    private float parryTimer = 0.0f;

    public float parryCooldown = 0.7f;
    private float parryCooldownTimer = 0.0f;

    public float attackCooldown = 0.5f;
    private float attackCooldownTimer = 0.0f;

    public bool inBattle = false;

    public AudioClip powerUpClip, damageClip, parryClip, hitClip, defendClip, deathClip;

    private bool isDefending = false, isParrying = false, recentlyParried = false, recentlyAttacked = false;
    private Enemy opponent;
    private GameManager gameManager;

	// Use this for initialization
	void Start () {
        gameManager = FindObjectOfType<GameManager>();
        parryTimer = parryTime;
        parryCooldownTimer = parryCooldown;
        attackCooldownTimer = attackCooldown;
	}

    private void FixedUpdate()
    {
        if (!inBattle && gameManager.inGame)
            Movement();
    }

    // Update is called once per frame
    void Update () {
        if (inBattle)
            Combat();

        if(recentlyAttacked && attackCooldownTimer <= 0)
        {
            attackCooldownTimer = attackCooldown;
            recentlyAttacked = false;
        }
        else if(recentlyAttacked)
        {
            attackCooldownTimer -= Time.deltaTime;
        }

        if(recentlyParried && parryCooldownTimer <= 0)
        {
            recentlyParried = false;
            parryCooldownTimer = parryCooldown;
        }
        else if (recentlyParried)
        {
            parryCooldownTimer -= Time.deltaTime;
        }

        if(isParrying && parryTimer <= 0)
        {
            parryTimer = parryTime;
            recentlyParried = true;
            isParrying = false;
            isDefending = true;
        }
        else if (isParrying)
        {
            parryTimer -= Time.deltaTime;
        }
	}

    //Take care of moving across the overworld
    void Movement()
    {
        if (Input.GetKey(attackKey))
        {
            transform.position += Vector3.left * (movementSpeed * Time.fixedDeltaTime);
        }
        else if (Input.GetKey(defendKey))
        {
            transform.position -= Vector3.left * (movementSpeed * Time.fixedDeltaTime);
        }
    }

    //Use the same keys to attack and defend
    void Combat()
    {
        if (Input.GetKeyDown(attackKey) && !recentlyAttacked)
        {
            opponent.TakeDamage(damage);
            recentlyAttacked = true;
        }
        else if (Input.GetKeyDown(defendKey))
        {
            if (!recentlyParried)
            {
                isParrying = true;
                parryTimer = parryTime;
            }
            else
            {
                isDefending = true;
            } 
        }
        else if (Input.GetKeyUp(defendKey))
        {
            if (isParrying)
            {
                recentlyParried = true;
                isParrying = false;
            }
            isDefending = false;
        }
    }

    //Takes Damage if the enemy attempts to hit
    public void TakeDamage(float damage)
    {
        if (isParrying)
        {
            opponent.TakeDamage(damage * parryMultiplier);
            AudioManager.instance.PlaySFX(parryClip);
        } else if (isDefending)
        {
            health -= damage * 0.5f;
            if (health <= 0)
                OnDeath();
            AudioManager.instance.PlaySFX(defendClip);
        }
        else
        {
            health -= damage;
            if (health <= 0)
                OnDeath();
            AudioManager.instance.PlaySFX(hitClip);
        }
    }

    void OnDeath()
    {
        print("RIP DIED");
        AudioManager.instance.PlaySFX(deathClip);
    }

    //Function used to declare that the player is in combat
    public void EnterBattle(Enemy enemy)
    {
        inBattle = true;
        opponent = enemy;
    }

    public void ExitBattle()
    {
        inBattle = false;
        opponent = null;
        print("I WON!");
        //Add points here?
    }

    public void Upgrade(PowerUp.PowerUpTypes typeOfPowerUp)
    {
        print("Upgrade, Sweet!");

        switch (typeOfPowerUp)
        {
            case PowerUp.PowerUpTypes.eput_HealthUp:
                {
                    health += 0.5f;
                }
                break;

            case PowerUp.PowerUpTypes.eput_SwordUp:
                {
                    damage += 0.5f;
                }
                break;

            case PowerUp.PowerUpTypes.eput_ParryUp:
                {
                    parryMultiplier += 0.5f;
                }
                break;
        }

        AudioManager.instance.PlaySFX(powerUpClip);
    }
}
