using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is used to change animations, set speeds, and determine attacks for Enemy4
/// </summary>

public class Enemy4 : MonoBehaviour
{
    public bool offCD = true;               //determines if the enemy can attack again
    public bool attacking1 = false;         //checks if the enemy is using a magic attack
    public bool attacking2 = false;         //checks if the enemy is using the melee attack
    public float attDamage;                 //how much damage the melee attack will do
    public float startSpeed = 2;            //original starting speed of the enemy
    public float speed;                     //speed the enemy is moving at
    public float attackCD = 3;              //the min time between magic attacks

    public Transform player;                //player object
    public Animator animate;                //animator component of the enemy
    public GameObject projectile;           //magic projectile object
    
    
    void Start()
    {
        speed = startSpeed;     //set speed
        player = GameObject.FindWithTag("Player").transform;    //gets player object
        animate = GetComponent<Animator>();                     //gets animator component
    }

    void Update()
    {
        StartCoroutine(Attack());

        //if the enemy is not using a magic attack, start moving towards the player
        if (animate.GetBool("Enemy4Attack1") == false)
        {
            animate.SetBool("Enemy4Move", true);
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }


    }

    IEnumerator Attack()
    {
        //if attack 1 is triggered and is able to be used, start attack1
        if ((attacking1 == true) && (offCD == true))
        {
            //if the enemy is not already attacking, start the animation for attack1 (magic attack)
            if ((animate.GetBool("Enemy4Attack1") == false) && (animate.GetBool("Enemy4Attack2") == false))
            {
                speed = 0;
                animate.SetBool("Enemy4Attack1", true);
                animate.SetBool("Enemy4Move", false);
                //start SpawnAttack coroutine
                StartCoroutine(SpawnAttack());
                yield return new WaitForSeconds(2f);        //wait for animation
                attacking1 = false;
                animate.SetBool("Enemy4Attack1", false);

                offCD = false;
                speed = startSpeed;

                yield return new WaitForSeconds(attackCD);  //wait until the enemy can attack again
                offCD = true;
            }
            else
            {
                attacking1 = false;
            }

        }
        else
        {
            attacking1 = false;
        }
     
        //if attack2 is triggered and the enemy is not already attacking, start attack2
        //attack2 (melee attack) is managed through Enemy4Attack2.cs
        if (attacking2 == true)
        {
            if ((animate.GetBool("Enemy4Attack1") == false) && (animate.GetBool("Enemy4Attack2") == false))
            {
                animate.SetBool("Enemy4Attack2", true);
                yield return new WaitForSeconds(1.25f);     //wait for animation
                attacking2 = false;
                animate.SetBool("Enemy4Attack2", false);
            }
            else
            {
                attacking2 = false;
            }

        }

    }

    //create the magic projectile object
    //managed through Enemy4Projectiles.cs
    IEnumerator SpawnAttack()
    {
        var pos = transform.position + (new Vector3(0, 1, 0));
        yield return new WaitForSeconds(1f);
        Instantiate(projectile, pos, Quaternion.identity);
    }

    //if the enemy's outer trigger collides with the player, start attack1 (magic attack)
    void OnTriggerStay2D(Collider2D collision)
    {
        //prob overkill but i dont wanna break it at this point - it might also conflict with the if statement in attacking2
        if ((collision.gameObject.tag == "Player") && (animate.GetBool("Enemy4Attack1") == false) && (animate.GetBool("Enemy4Attack2") == false))
        {
            attacking1 = true;
        }

    }

}
