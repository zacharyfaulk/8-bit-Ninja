using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is used to manage the melee attack for the FireBoss, this is attached to an empty child object
/// of FireBoss that has a collider that encompasses the FireBoss's melee range
/// -> used to determine if the player is able to be hit with melee
/// </summary>

public class FireBossMeleeAttack : MonoBehaviour
{
    public bool cd = false;         //determines if the player can take damage from this attack
    public bool mAttack = false;    //determines if the enemy can attack

    public Transform player;        //player object
    PlayerHealth playerHealth;      //player health
    

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;        //gets player object
        playerHealth = player.GetComponent<PlayerHealth>();         //gets player health
    }

    void Update()
    {
        StartCoroutine(MeleeAttack());
    }

    //if the player collides with the trigger, start the melee attack
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            mAttack = true;
            
            if ((playerHealth.currentHealth > 0) && (cd == false))
            {
                cd = true;
                StartCoroutine(OffCD());
                playerHealth.TakeDamage(transform.parent.GetComponent<FireBoss>().meleeAttDamage);
                
            }
        }

    }

    IEnumerator MeleeAttack()
    {
        //manage the melee attack animations
        if ((mAttack == true)) ///&& (this.transform.parent.GetComponent<Animator>().GetBool("FireBossMeleeAttack") == false))
        {
            this.transform.parent.GetComponent<Animator>().SetBool("FireBossMeleeAttack", true);
            yield return new WaitForSeconds(1f);    //wait for animation
            this.transform.parent.GetComponent<Animator>().SetBool("FireBossMeleeAttack", false);
            mAttack = false;
        }

    }

    //the player can only take damage from this attack every 1 second
    IEnumerator OffCD()
    {
        yield return new WaitForSeconds(1f);
        cd = false;
    }
}
