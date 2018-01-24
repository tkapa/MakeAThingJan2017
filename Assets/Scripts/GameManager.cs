using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public bool inGame = false;

    public PlayerController player;



    // Use this for initialization
    void Start () {
        player = FindObjectOfType<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
        if(!inGame && (Input.GetKey(player.attackKey) && Input.GetKey(player.defendKey)))
        {
            inGame = true;
            UIManager uiManager = FindObjectOfType<UIManager>();
            foreach (var btn in uiManager.buttons)
            {
                btn.gameObject.SetActive(false);
            }
            uiManager.text.gameObject.SetActive(false);
        }
	}
}
