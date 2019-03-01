using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is used to change animations, set speeds, and determine attacks for FireBoss
/// this doesn't include the laser heads, those are managaged through
/// FireBossHeads.cs and FireBossHeadsLaser.cs
/// </summary>

public class FireBoss : MonoBehaviour
{
    public bool offCD = true;                   //determines if the enemy can attack again
    public bool attacking = false;              //checks if the enemy is attacking
    public bool teleport = true;                //determines if the enemy is able to teleport (in phase 1)
    public bool[] headAttack = new bool[3];     //bool array to manage the status of the laser heads attached to the boss
    public float meleeAttDamage;                //how much damage the melee attack does
    public float startSpeed = 1;                //original starting speed of the enemy
    public float speed;                         //speed the enemy is moving at
    public float attackCD = 3f;                 //the min time between attacks
    public float teleportCD = 10f;              //the time the enemy waits to telelport if it doesn't collide with the player
    public float teleportWait = 0;              //teleport wait timer

    public Transform player;        //player object
    public Animator animate;        //animator component of the enemy
    

    void Start()
    {
        speed = startSpeed;     //set speed
        player = GameObject.FindWithTag("Player").transform;    //gets player object
        animate = GetComponent<Animator>();                     //gets animator component
    }

    void Update()
    {

        StartCoroutine(Attack());
        StartCoroutine(Teleport());

        //if none of the heads are attacking, start moving towards the player
        //FireBoss will still move during its ranged attack
        if (/*(attacking == false) &&*/ (headAttack[0] == false) && (headAttack[1] == false) && (headAttack[2] == false))
        {
            animate.SetBool("FireBossMove", true);
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        else
        {
            animate.SetBool("FireBossMove", false);
        }


    }

    IEnumerator Attack()
    {
        //if the enemy is able to attack, start the ranged attacking process
        //enemy doesn't stop to attack
        if (offCD == true)
        {           
            offCD = false;
            attacking = true;
            //speed = 0;

            animate.SetBool("FireBossRangeAttack1", true);
            //animate.SetBool("FireBossMove", false);
            yield return new WaitForSeconds(3.3f);
            attacking = false;
            animate.SetBool("FireBossRangeAttack1", false);
            //speed = startSpeed;
            attacking = false;
            yield return new WaitForSeconds(attackCD);  //wait until the enemy can attack again
            offCD = true;
        }
        //else
        //{
        //    attacking = false;
        //}
    }

    //if the enemy hasnt't collided with the player for 10 seconds
    //telelport to a random location
    IEnumerator Teleport()
    {
        if (teleport == true)
        {
            if (teleportWait < teleportCD)
            {
                teleportWait += Time.deltaTime;
            }
            else
            {
                var position = new Vector3(Random.Range(-50f, 50f), Random.Range(-10f, 10f), 0);
                transform.position = new Vector3(position.x, position.y, 0);
                teleportWait = 0;
            }
        }

        yield return null;
    }

    //reset the wait timer on trigger collision with the player
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            teleportWait = 0;
        }
    }
}
