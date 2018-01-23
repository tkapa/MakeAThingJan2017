using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float health = 1;

    public float damage = 1;

    [Range(0, 1)]
    public float defendPriorityHealthPercentage = 0.75f;

    [Range(0, 1)]
    public float defendPercentage = 0.5f;

    public float minimumTimeToAction= 2.0f, maximumTimeToAction = 4.0f;

    [HideInInspector]
    public bool inBattle = false;

    public float attackTime = 1.0f;
    private float attackTimer = 0.0f;

    public float defendTime = 0.5f;
    private float defendTimer = 0.0f;

    public bool canParry = false;

    public float parryTime = 0.2f;


    private float defendHealth = 0.0f, parryTimer = 0.0f;
    private float timeToAction = 0.0f, actionCoinFlip = 0.0f; 
    private GameManager gameManager;
    private Renderer myRenderer;
    private bool isDefending = false, isAttacking = false, isParrying = false;

	// Use this for initialization
	void Start () {
        myRenderer = GetComponent<Renderer>();
        gameManager = FindObjectOfType<GameManager>();
        timeToAction = minimumTimeToAction;
        attackTimer = attackTime;
        defendTimer = defendTime;
        defendHealth = defendPriorityHealthPercentage * health;
        parryTimer = parryTime;
	}
	
	// Update is called once per frame
	void Update () {
        if (inBattle)
            ActionDecision();

        if (isAttacking)
            Attack();

        if (isParrying || isDefending)
            Defend();
	}

    //Takes Damage if the player attempts to hit
    public void TakeDamage(float damage)
    {
        if (isParrying)
        {
            print("Enemy Boom Parry");
            gameManager.player.TakeDamage(damage * 2);
        }
        else if (!isDefending)
        {
            print("Enemy OOF!");
            health -= damage;
            if (health <= 0)
                OnDeath();
        }
    }

    //Makes a decision of what action to take
    void ActionDecision()
    {
        if(timeToAction <= 0 && (!isAttacking || !isDefending))
        {
            timeToAction = Random.Range(minimumTimeToAction, maximumTimeToAction);

            if (health <= defendHealth)
            {           
                actionCoinFlip = Random.Range(0.0f, 1.0f);
            }

            if (actionCoinFlip > defendPercentage)
            {
                print("Chose defend");
                isParrying = true;
                myRenderer.material.color = Color.yellow;
            }
            else
            {
                print("Chose attack");
                isAttacking = true;
                myRenderer.material.color = Color.red;
            }
        } else if(!isAttacking || !isDefending)
        {
            timeToAction -= Time.deltaTime;
        }
    }

    void Attack()
    {
        print("Prepping Attack!");
        
        if(attackTimer <= 0)
        {
            print("I'm Attacking!");
            attackTimer = attackTime;
            isAttacking = false;
            gameManager.player.TakeDamage(1);
            myRenderer.material.color = Color.white;
        }
        else
        {
            attackTimer -= Time.deltaTime;
        }
    }

    void Defend()
    {
        print("Shields up! " + isParrying);

        if (isParrying && parryTimer <= 0)
        {
            print("Enemy Parry Off");

            parryTimer = parryTime;
            isDefending = true;
            isParrying = false;
        }
        else
            parryTimer -= Time.deltaTime;

        if (isDefending && defendTimer <= 0)
        {
            defendTimer = defendTime;
            isDefending = false;
            myRenderer.material.color = Color.white;
        }
        else
        {
            defendTimer -= Time.deltaTime;
        }
    }

    void OnDeath()
    {
        print(gameObject.name + " has died, progress!");
        gameManager.player.ExitBattle();
        Destroy(gameObject);
    }
}
