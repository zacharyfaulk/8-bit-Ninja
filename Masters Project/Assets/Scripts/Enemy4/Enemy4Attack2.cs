using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is used to manage Attack2 for Enemy4, this is attached to an empty child object
/// of Enemy4 that has a collider that encompasses Enemy4's "melee range"
/// -> used to determine if the player is able to be hit with melee
/// </summary>

public class Enemy4Attack2 : MonoBehaviour
{
    public bool cd = false;         //determines if the player can take damage from this attack
  
    public Transform player;        //player object
    PlayerHealth playerHealth;      //player health


    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;        //gets player object
        playerHealth = player.GetComponent<PlayerHealth>();         //gets player health
    }

    //if the player collides with the trigger, start Enemy4's attacking2 variable to true
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            this.transform.parent.GetComponent<Enemy4>().attacking2 = true;
            //if Enemy4 is doing the melee anumation and it able to damage the player, damage the player
            if ((playerHealth.currentHealth > 0) && (cd == false) && (this.transform.parent.GetComponent<Animator>().GetBool("Enemy4Attack2") == true))
            {
                playerHealth.TakeDamage(transform.parent.GetComponent<Enemy4>().attDamage);
                cd = true;
                StartCoroutine(OffCD());
            }
        }

    }

    //wait until the enemy can damage the player with this attack again
    IEnumerator OffCD()
    {
        yield return new WaitForSeconds(1.25f);
        cd = false;
    }
}
