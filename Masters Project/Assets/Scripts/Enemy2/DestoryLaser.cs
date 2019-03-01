using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is used to manage the laser objects created by Enemy2
/// </summary>

public class DestoryLaser : MonoBehaviour
{
    public bool cd = false;         //determines if the player can take damage from this object
    public float attackDamage;      //how much damage the attack does

    public Transform player;        //player object
    PlayerHealth playerHealth;      //player health
    
    

    void Start ()
    {
        StartCoroutine("Destroy");

        player = GameObject.FindWithTag("Player").transform;    //get player object
        playerHealth = player.GetComponent<PlayerHealth>();     //get player's health
    }

	void Update ()
    {
        
    }


    //destroy this object after 1.25 seconds of being created
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(1.25f);
        Destroy(gameObject);
    }

    //if this object collides with the player, damage the player 1 time
    void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Player") && (cd == false))
        {
            cd = true;
            if (playerHealth.currentHealth > 0)
            {
                playerHealth.TakeDamage(attackDamage);
            }
        }

    }
}


