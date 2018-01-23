using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public string attackKey = "a", defendKey = "d";

    public float movementSpeed = 2.0f;

    public float health = 2, damage = 1;

    public float parryTime = 0.9f;
    private float parryTimer = 0.0f;

    public float parryCooldown = 0.7f;
    private float parryCooldownTimer = 0.0f;

    private bool inBattle = false, isDefending = false, isParrying = false, recentlyParried = false;
    private Enemy opponent;


	// Use this for initialization
	void Start () {
        parryTimer = parryTime;
        parryCooldownTimer = parryCooldown;
	}

    private void FixedUpdate()
    {
        if (!inBattle)
            Movement();
    }

    // Update is called once per frame
    void Update () {
        if (inBattle)
            Combat();

        if(recentlyParried && parryCooldownTimer <= 0)
        {
            recentlyParried = false;
            parryCooldownTimer = parryCooldown;
        }
        else
        {
            parryCooldownTimer -= Time.deltaTime;
        }

        if(isParrying && parryTimer <= 0)
        {
            parryTimer = parryTime;
            recentlyParried = true;
            isParrying = false;
            isDefending = true;
        }else
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
        if (Input.GetKeyDown(attackKey))
        {
            opponent.TakeDamage(damage);
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
            opponent.TakeDamage(damage * 2);
        } else if (isDefending)
        {
            health -= damage * 0.5f;
            if (health <= 0)
                OnDeath();
        }
        else
        {
            health -= damage;
            if (health <= 0)
                OnDeath();
        }
    }

    void OnDeath()
    {
        print("RIP DIED");
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
}
