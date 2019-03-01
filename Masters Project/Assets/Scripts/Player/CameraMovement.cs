using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is used to manage the camera and set it to follow the player
/// </summary>

public class CameraMovement : MonoBehaviour
{
    Transform player;   //player object

	void Start ()
    {
        player = GameObject.Find("Player").transform;   //find player object
	}
	

	void Update ()
    {
        //move camera to follow the player
        transform.position = player.position + new Vector3(0, 0, -10);
	}
}
