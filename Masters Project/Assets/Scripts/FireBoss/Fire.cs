using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is used to manage the Fire object created by FireBoss's Fireball ranged attack
/// </summary>

public class Fire : MonoBehaviour
{
    //public bool offCD = true;       //determines if the player can take damage from this object
    public float attDamage;         //how much damage the attack does

    public Transform player;        //player object
    PlayerHealth playerHealth;      //player health
    
    

    void Start()
    {
        StartCoroutine("Destroy");

        player = GameObject.FindWithTag("Player").transform;    //get player object
        playerHealth = player.GetComponent<PlayerHealth>();     //get player health
    }

    void Update()
    {

    }

    //if the player collides with the trigger, start damaging the player
    private void OnTriggerStay2D(Collider2D collision)
    {

        if ((collision.gameObject.tag == "Player")) //&& (offCD == true))
        {
            //offCD = false;
            if (playerHealth.currentHealth > 0)
            {
                    playerHealth.TakeDamage(attDamage);
                    //StartCoroutine(FireCD());
            }
        }

    }

    //was created to try to limit how often the player would take
    //damage from a fire, but the collision was causing issues so
    //i lowered the damage the fire does and it instead damages the player every collision
    //DOESN'T DAMAGE THE PLAYER IF THE PLAYER IS NOT MOVING IN THE FIRE ***UNITY BUG***
    /*IEnumerator FireCD()
    {
        if (offCD == false)
        {
            yield return new WaitForSeconds(1f);
            offCD = true;
        }
    }*/

    //destroy the fire after 30 seconds to reduce framerate drops
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(30f);
        Destroy(gameObject);
    }
}
