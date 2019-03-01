using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is used to keep track of what mode the user wants to play on through scene restarts
/// this is attached to empty game object in the scene that never gets destroyed
/// </summary>

public class GlobalVar : MonoBehaviour
{

    public bool easyMode = false;       //keeps track of what mode the player is on
    public Transform player;            //player object


    //dont destroy the object this is attached to
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);         
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        
    }

    //update the player on what mode it's on so on death 
    //the scene is not restarted if on easy mode
    void Update ()
    {
        player = GameObject.FindWithTag("Player").transform;
        player.transform.GetComponent<PlayerHealth>().easyMode = easyMode;
	}
}
