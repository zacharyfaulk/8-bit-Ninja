using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is used to change animations, set speeds, and manage attacks for Enemy1
/// </summary>

public class Enemy1 : MonoBehaviour
{
    public bool cd = false;             //determines if the player can take damage from this enemy
    public bool attacking = false;      //checks if the enemy is attacking
    public bool stunned = false;        //checks if the enemy is stunned
    public float attDamage;             //how much damage the attack does
    public float speed = 1;             //determines speed of enemy  
    
    public Animator animate;            //animator component of the enemy
    public Transform player;            //player object
    PlayerHealth playerHealth;          //player health


    void Start ()
    {
        player = GameObject.FindWithTag("Player").transform;    //gets player object
        playerHealth = player.GetComponent<PlayerHealth>();     //gets player's health
        animate = GetComponent<Animator>();                     //gets animator component
    }
	
	void FixedUpdate ()
    {

        StartCoroutine(Attack());
        StartCoroutine(Charge());

        //if the enemy is not stunned, start moving towards the player
        if ((animate.GetBool("Enemy1Stun") == false))
        {
            //start moving animation and moving towards the player
            animate.SetBool("Enemy1Move", true);
            var direction = (player.position - transform.position)/(player.position - transform.position).magnitude;
            GetComponent<Rigidbody2D>().AddForce(direction * speed);    //using rigidbody.addforce to use drag

            //increase the enemy speed the longer its been moving
            if (speed < 20f)
            {
                speed = speed + 0.05f;
            }          
        }
    }

    IEnumerator Charge()
    {
        //once the enemy has reached a certain speed, start the charge animation
        if (speed > 15f)
        {
            animate.SetBool("Enemy1Charge", true);
            yield return new WaitForSeconds(1f);
            animate.SetBool("Enemy1Charge", false);
            animate.SetBool("Enemy1ChargeOut", true);
        }

        //if the enemy has been stunned, start the stun animation and reset the speed after 2 seconds
        if (stunned == true)
        {
            speed = 0;
            animate.SetBool("Enemy1ChargeOut", false);
            animate.SetBool("Enemy1Stun", true);
            yield return new WaitForSeconds(2f);
            stunned = false;
            animate.SetBool("Enemy1Stun", false);
            speed = 1;
        }
    }

    IEnumerator Attack()
    {     
        //if the enemy is set to attack, start the attack animation and reset the speed
        if (attacking == true)
        {
            speed = 1;
            animate.SetBool("Enemy1Charge", false);
            animate.SetBool("Enemy1ChargeOut", false);
            animate.SetBool("Enemy1Attack1", true);
            yield return new WaitForSeconds(1f);
            animate.SetBool("Enemy1Attack1", false);
            attacking = false;
        }
    }

    //if the enemy's trigger collides with the player, start the attacking process
    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            attacking = true;
            //if the player is not dead, and hasnt taken damage from this enemy in "cd" time (0.6666667 seconds)
            //then damage the player while starting the cooldown coroutine and damage the player
            if ((playerHealth.currentHealth > 0) && (cd == false))
            {
                cd = true;
                StartCoroutine(OffCD());
                playerHealth.TakeDamage(attDamage);
            }
        }
    }

    IEnumerator OffCD()
    {
        //the time interval in which this enemy can damage the player
        yield return new WaitForSeconds(0.666666667f);
        cd = false;
    }

    //if the enemy's box collider collides with a surface object or another enemy object while "charging"
    //start the stun process, otherwise reset the speed upon collision
    void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "Surface") || (collision.gameObject.tag == "Enemy"))
        {
            if ((animate.GetBool("Enemy1ChargeOut") == true))
            {
                stunned = true;
            }
            else
            {
                //if the enemy was in the middle of the charging animation collided
                animate.SetBool("Enemy1Charge", false);
                speed = 1;
            }
        }
    }
}
