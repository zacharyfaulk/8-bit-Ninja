using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is used to change animations, set speeds, and manage attacks for Enemy3
/// </summary>

public class Enemy3 : MonoBehaviour
{
    public bool offCD = true;           //determines if the enemy can attack again
    public bool attacking = false;      //checks if the enemy is attacking
    public bool hit = false;            //checks if the player was hit by the teleport melee attack
    public float attDamage;             //how much damage the melee attack does
    public float startSpeed = 1;        //original starting speed of the enemy
    public float speed;                 //speed the enemy is moving at
    public float attackCD = 3;          //the min time between attacks

    public Transform player;            //player object 
    PlayerHealth playerHealth;          //player health
    public Animator animate;            //animator component of the enemy
    public GameObject projectile;       //ranged projectile object


    void Start()
    {
        speed = startSpeed;     //set speed
        player = GameObject.FindWithTag("Player").transform;    //gets player object
        playerHealth = player.GetComponent<PlayerHealth>();     //gets player's health
        animate = GetComponent<Animator>();                     //gets animator component
    }

    void Update()
    {
        StartCoroutine(Attack());

        //if the enemy is not attacking, start moving towards the player
        if ((animate.GetBool("Enemy3Attack1") == false) && (animate.GetBool("Enemy3Attack2") == false))
        {
            animate.SetBool("Enemy3Move", true);
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }


    }

    IEnumerator Attack()
    {
        //if the enemy is able to attack, start one of the attacks
        if ((attacking == true) && (offCD == true))
        {
            //70% that attack 1 will be choosen (ranged projectile attack) if not already attacking
            if ((Random.value > .3) && (animate.GetBool("Enemy3Attack1") == false) && (animate.GetBool("Enemy3Attack2") == false))
            {
                speed = 0;
                animate.SetBool("Enemy3Attack1", true);
                animate.SetBool("Enemy3Move", false);
                //start SpawnAttack coroutine
                StartCoroutine(SpawnAttack());
                yield return new WaitForSeconds(2.5f);      //wait for animation
                attacking = false;
                animate.SetBool("Enemy3Attack1", false);

                offCD = false;
                speed = startSpeed;

                yield return new WaitForSeconds(attackCD);  //wait until the enemy can attack again
                offCD = true;
            }
            //30% that attack 2 will be choosen (teleport attack) if not already attacking
            else if ((Random.value <= .3) && (animate.GetBool("Enemy3Attack1") == false) && (animate.GetBool("Enemy3Attack2") == false))
            {
                speed = 0;
                animate.SetBool("Enemy3Attack2", true);
                animate.SetBool("Enemy3Move", false);
                //start teleport coroutine
                StartCoroutine(Teleport());
                yield return new WaitForSeconds(2f);        //wait for animation
                attacking = false;
                animate.SetBool("Enemy3Attack2", false);

                offCD = false;
                speed = startSpeed;

                yield return new WaitForSeconds(attackCD);  //wait until the enemy can attack again
                offCD = true;
            }
            else
            {
                attacking = false;
            }


        }

    }

    //create the ranged project object
    //managed through Enemy3Projectile.cs
    IEnumerator SpawnAttack()
    {
        var pos = transform.position + (new Vector3(0, 2, 0));
        yield return new WaitForSeconds(1.5f);
        Instantiate(projectile, pos, Quaternion.identity);
    }

    IEnumerator Teleport()
    {
        yield return new WaitForSeconds(1.10f);     //wait for animation
        var temp = player.transform.position;       //get player's current position
        yield return new WaitForSeconds(0.65f);     //give time for player to move
        this.transform.position = temp;             //move enemy to saved player location
        hit = true;
        yield return new WaitForSeconds(0.25f);     //the time when the enemy can damage the player through melee
        hit = false;
    }

    //if the enemy's trigger collides with the player, start the attacking process
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            attacking = true;
        }
    }

    //if the enemy's box collider collides with the player while the enemy is able to
    //damage the player with melee, damage the player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "Player") && (hit == true))
        {
            if (playerHealth.currentHealth > 0)
            {
                playerHealth.TakeDamage(attDamage);
            }
        }
    }
}
