using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCollider : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("Collision");
        if (collision.gameObject.tag == "Player")
        {
            print("Battle Initiated!");

            collision.gameObject.GetComponent<PlayerController>().EnterBattle(GetComponentInParent<Enemy>());
            GetComponentInParent<Enemy>().inBattle = true;
            //Insert Camera Zoom Function Here, Cue music
        }
    }
}
