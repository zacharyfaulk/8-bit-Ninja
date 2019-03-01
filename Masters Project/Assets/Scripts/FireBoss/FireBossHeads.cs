using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is used to manage each of the individual 3 heads attached to the FireBoss
/// each head has this script attached and the headNum int is used to differentiate between the heads
/// </summary>

public class FireBossHeads : MonoBehaviour
{
    public bool offCD = true;           //determines if the enemy can attack again
    public bool attacking = false;      //checks if the enemy is attacking
    public int headNum = 0;             //the specific head number (light = 0, hybrid = 1, dark = 2)
    public float attDamage;             //how much damage the attack does
    public float attackCD = 3f;         //the min time between attacks

    public Transform player;            //player object
    public Animator animate;            //animator component of the enemy

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;    //get player object
        animate = GetComponent<Animator>();                     //get animator component
    }

    void Update()
    {
        StartCoroutine(Attack());
    }


    IEnumerator Attack()
    {
        //if the enemy is able to attack, start the laser attack of the specific head
        if ((attacking == true) && (offCD == true))
        {
            //inform the parent that this specific head is attacking -> stop the FireBoss's movement
            transform.parent.GetComponent<FireBoss>().headAttack[headNum] = true;
            animate.SetBool("BossHeadAttack1", true);
            yield return new WaitForSeconds(3.3f);          //wait for animation
            attacking = false;
            animate.SetBool("BossHeadAttack1", false);
            //inform the parent that this head is finished attacking
            transform.parent.GetComponent<FireBoss>().headAttack[headNum] = false;
            offCD = false;
            yield return new WaitForSeconds(attackCD);      //wait until the enemy can attack again
            offCD = true;
        }
        else
        {
            attacking = false;
        }
    }

    //if the player collides with the heads trigger, start attacking
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            attacking = true;
        }
    }

}
