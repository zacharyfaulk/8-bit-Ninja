using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This is used to manage the health of the FireBoss
/// </summary

public class FireBossHealth : MonoBehaviour
{
    bool damaged = false;                   //if the enemy is currently being damaged (not used)
    bool isDead = false;                    //if the enemy is dead (not really used)
    public bool damagable = false;          //if the enemy is able to be damaged
    public float startHealth = 100;         //starting health of the enemy
    public float currentHealth;             //current health of the enemy
    public float darkDamage;                //how much damage the enemy takes from a dark attack
    public float lightDamage;               //how much damage the enemy takes from a light attack
    public float hybridDamage;              //how much damage the enemy takes from a hybrid attack

    public Slider healthSlider1;                //health slider object
    public GameObject head1, head2, head3;      //the head objects attached to the FireBoss
    public List<GameObject> headList = new List<GameObject>();      //list to manage the status of the head objects
    //public AudioClip damageClip;            //damaged sound clip
    //AudioSource playerAudio;                //audio player object

    void Start()
    {
        //playerAudio = GetComponent<AudioSource>();     //get audio component
        currentHealth = startHealth;    //set current health
        headList.Add(head1);            //add the first head (light head)
        headList.Add(head2);            //add the second head (hybrid head)
        headList.Add(head3);            //add the third head (dark head)

    }

    void Update()
    {
        StartCoroutine(HeadManagement());

        if (damaged == true)
        {
            //not used currently
        }
        damaged = false;        
    }

    //manage the status of the heads -> transition from FireBoss phase 1 to phase 2
    IEnumerator HeadManagement()
    {
        if (headList[0] == null)
        {
            //if the light head is dead, make sure the boss can resume moving if it died while attacking
            transform.GetComponent<FireBoss>().headAttack[0] = false;
        }
        if (headList[1] == null)
        {
            //if the hybrid head is dead, make sure the boss can resume moving if it died while attacking
            transform.GetComponent<FireBoss>().headAttack[1] = false;
        }
        if (headList[2] == null)
        {
            //if the dark head is dead, make sure the boss can resume moving if it died while attacking
            transform.GetComponent<FireBoss>().headAttack[2] = false;
        }

        //if all 3 heads have died
        if ((headList[0] == null) && (headList[1] == null) && (headList[2] == null))
        {
            damagable = true;   //allow the boss to take damage
            transform.GetComponent<FireBoss>().teleport = false;    //stop the boss from teleporting
            transform.GetComponent<FireBoss>().speed = 4;           //increase the speed
            transform.GetComponent<FireBoss>().startSpeed = 4;      //increase the speed
            transform.GetComponent<FireBoss>().attackCD = 1.5f;     //decrease attack cooldown
        }

        yield return null;
    }

    //if the enemy is hit by the player's attacks
    //don't damage the boss unless all the attached heads have been destroyed
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.name == "attDarkPrefab(Clone)") && (damagable == true))
        {
            damaged = true;
            currentHealth -= darkDamage;
            healthSlider1.value = currentHealth;

            if ((currentHealth <= 0) && (isDead == false))
            {
                Death();
            }
        }
        else if ((collision.gameObject.name == "attLightPrefab(Clone)") && (damagable == true))
        {
            damaged = true;
            currentHealth -= lightDamage;
            healthSlider1.value = currentHealth;

            if ((currentHealth <= 0) && (isDead == false))
            {
                Death();
            }
        }
        else if ((collision.gameObject.name == "attHybridPrefab(Clone)") && (damagable == true))
        {
            damaged = true;
            currentHealth -= hybridDamage;
            healthSlider1.value = currentHealth;

            if ((currentHealth <= 0) && (isDead == false))
            {
                Death();
            }
        }
    }

    //destroy this object on death (when current health == 0)
    void Death()
    {
        GameObject.Find("SceneController").GetComponent<SceneController>().activeEnemies.Remove(this.gameObject);
        Destroy(gameObject);
    }

}
