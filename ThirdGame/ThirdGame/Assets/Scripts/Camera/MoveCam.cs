using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCam : MonoBehaviour
{
    public Transform cameraPosition;
    public Transform orientation;

    MoveCam moveCam;
    PlayerCamera playerCamera;

    void Awake()
    {
        moveCam = GetComponent<MoveCam>();
        playerCamera = GetComponentInChildren<PlayerCamera>();
    }
    
    void Update()
    {
        if(cameraPosition != null)
        {
            transform.position = cameraPosition.position;
        }
        else if(cameraPosition == null)
        {
            moveCam.enabled = false;
        }

        // bug fix with many errors after death due to no referenced object - object was destroyed

        if(orientation == null)
        {
            Debug.Log("orientation is gone");
            playerCamera.enabled = false;
        }
    }
}
