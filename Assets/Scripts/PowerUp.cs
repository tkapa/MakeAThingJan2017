using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {
    
    public enum PowerUpTypes
    {
        eput_HealthUp,
        eput_SwordUp,
        eput_ParryUp
    }

    public PowerUpTypes powerUpType;

    private PlayerController playerController;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerController.Upgrade(powerUpType);
            Destroy(gameObject);
        }            
    }
}
