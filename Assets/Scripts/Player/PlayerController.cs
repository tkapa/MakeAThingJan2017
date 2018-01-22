using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public string attackKey = "a", defendKey = "d";

    public float movementSpeed = 2.0f;

    private bool inBattle = false;
    private Enemy opponent;


	// Use this for initialization
	void Start () {
		
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
            print("ATTACK!");
            opponent.TakeDamage(1);
        }
        else if (Input.GetKeyDown(defendKey))
        {
            print("DEFEND!");
        }
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
    }
}
