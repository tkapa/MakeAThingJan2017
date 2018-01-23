using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCollider : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            print("Battle Initiated!");

            other.gameObject.GetComponent<PlayerController>().EnterBattle(GetComponentInParent<Enemy>());
            GetComponentInParent<Enemy>().inBattle = true;
            //Insert Camera Zoom Function Here, Cue music
        }
    }
}
