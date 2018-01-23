using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {

    public PlayerController player;

    public Transform overworldZoom;
    public Transform battleZoom;
    public float lerpTime = 1.0f;

    private void FixedUpdate()
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
}
