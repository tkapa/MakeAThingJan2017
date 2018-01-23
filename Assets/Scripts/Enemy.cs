using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int health = 1;

    [Range(0, 1)]
    public float defendPriorityHealthPercentage = 0.75f;

    [Range(0, 1)]
    public float defendPercentage = 0.5f;

    public float minimumTimeToAttack = 2.0f, maximumTimeToAttack = 4.0f;

    public bool inBattle = false;

    private float timeToAction = 0.0f, actionCoinFlip = 0.0f; 
    private GameManager gameManager;
    private bool isDefending = false;

	// Use this for initialization
	void Start () {
        gameManager = FindObjectOfType<GameManager>();
        timeToAction = minimumTimeToAttack;
	}
	
	// Update is called once per frame
	void Update () {
        if (inBattle)
            ActionDecision();
	}

    //Takes Damage if the player attempts to hit
    public void TakeDamage(int damage)
    {
        if (!isDefending)
        {
            print("Enemy OOF!");
            health -= damage;
            if (health <= 0)
                OnDeath();
        }
        else
        {
            print("Enemy TING!");
        }
    }

    //Makes a decision of what action to take
    void ActionDecision()
    {
        if(timeToAction <= 0)
        {
            timeToAction = Random.Range(minimumTimeToAttack, maximumTimeToAttack);

            if(health <= (health * defendPriorityHealthPercentage))
            {
                actionCoinFlip = Random.Range(0, 1);
            }

            if (actionCoinFlip > defendPercentage)
            {
                Defend();
            }
            else
            {

            }

        }
    }

    void Attack()
    {

    }

    void Defend()
    {

    }

    void OnDeath()
    {
        print(gameObject.name + " has died, progress!");
        gameManager.player.ExitBattle();
        Destroy(gameObject);
    }
}
