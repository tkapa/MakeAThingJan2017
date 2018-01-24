using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {
    
    public Transform overworldZoom;
    public Transform battleZoom;
    public float lerpTime = 1.0f;

    public Transform playerPan, endGamePan;

    private Transform destination;
    private PlayerController player;
    private GameManager gameManager;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        gameManager = FindObjectOfType<GameManager>();
        destination = endGamePan;
    }

    private void FixedUpdate()
    {
        if (gameManager.inGame)
            GameCamera();
        else
            PanCamera();
    }

    void GameCamera()
    {
        if (player.inBattle)
        {
            transform.position = Vector3.Lerp(transform.position, battleZoom.position, lerpTime);
            GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, 2.5f, 0.25f);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, overworldZoom.position, lerpTime);
            GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, 5, 0.25f);
        }
    }

    void PanCamera()
    {
        if (Vector3.Distance(transform.position, playerPan.position) <= 5)
            destination = endGamePan;
        else if (Vector3.Distance(transform.position, endGamePan.position) <= 5)
            destination = playerPan;

        transform.position = Vector3.MoveTowards(transform.position, destination.position, 3 * Time.deltaTime);
    }
}
