using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is used to change animations, set speeds, and determine attacks for Enemy2
/// </summary>

public class Enemy2 : MonoBehaviour
{
    public bool offCD = true;           //determines if the enemy can attack again
    public bool attacking = false;      //checks if the enemy is attacking
    public float attDamage;             //how much damage the attack does
    public float startSpeed = 1;        //original starting speed of the enemy
    public float speed;                 //speed the enemy is moving at
    public float attackCD = 3f;         //the min time between attacks

    public Animator animate;            //animator component of the enemy
    public Transform player;            //player object
   


    void Start()
    {
        speed = startSpeed;     //set speed
        player = GameObject.FindWithTag("Player").transform;    //gets player object
        animate = GetComponent<Animator>();                     //gets animator component
    }

    void Update()
    {
        StartCoroutine(Attack());

        //if the enemy is not attacking, start moving towards the player
        if (attacking == false)
        {
            //start moving animation and moving towards the player
            animate.SetBool("Enemy2Move", true);
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }

    IEnumerator Attack()
    {
        //if the enemy is set to attack and is off cd, start the attacking process
        if ((attacking == true) && (offCD == true))
        {
            speed = 0;  //stop the enemy while attacking
            
            //set the animations and after 3.3 seconds resume moving
            animate.SetBool("Enemy2Attack1", true);
            animate.SetBool("Enemy2Move", false);
            yield return new WaitForSeconds(3.3f);
            attacking = false;
            animate.SetBool("Enemy2Attack1", false);
            offCD = false;
            speed = startSpeed;
            yield return new WaitForSeconds(attackCD);  //wait until enemy can attack again
            offCD = true;
        }
        else
        {
            attacking = false;
        }
    }

    //if the enemy's trigger collides with the player, start the attacking process
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            attacking = true;
        }
    }
}
