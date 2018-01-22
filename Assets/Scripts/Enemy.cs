using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int health = 1;
    GameManager gameManager;

	// Use this for initialization
	void Start () {
        gameManager = FindObjectOfType<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Takes Damage if the player attempts to hit
    public void TakeDamage(int damage)
    {
        print("OOF!");
        health -= damage;
        if (health <= 0)
            OnDeath();
    }

    void OnDeath()
    {
        gameManager.player.ExitBattle();
        Destroy(gameObject);
    }
}
