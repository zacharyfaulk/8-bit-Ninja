using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This is used to manage the health of the 4 basic enemies
/// </summary

public class EnemyHealth : MonoBehaviour
{
    public bool cd = false;             //determines if the attack range is being increased
    bool damaged = false;               //if the enemy is currently being damaged
    bool isDead = false;                //if the enemy is dead (not really used)
    public int i = 0;                   //increase attack range counter
    public float startHealth = 100;     //starting health of the enemy
    public float currentHealth;         //current health of the enemy
    public float darkDamage;            //how much damage the enemy takes from a dark attack
    public float lightDamage;           //how much damage the enemy takes from a light attack
    public float hybridDamage;          //how much damage the enemy takes from a hybrid attack

    public Slider healthSlider1;        //health slider object
    CapsuleCollider2D capsule;          //capsule collider object
    //public AudioClip damageClip;        //danaged sound audio clip
    //AudioSource playerAudio;            //audio player object

    void Start()
    {
        //playerAudio = GetComponent<AudioSource>();        //get audio component
        currentHealth = startHealth;                        //set current health
        if (GetComponent<CapsuleCollider2D>() != null)      //get the enemy's outer collider
        {
            capsule = GetComponent<CapsuleCollider2D>();
        }
    }

    void Update()
    {
        StartCoroutine(IncreaseRange());

        if (damaged == true)
        {
            //not used currently
        }
        damaged = false;       
    }

    //if the enemy is hit by the player's attacks, damage the enemy according to
    //the type of attack the player used
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "attDarkPrefab(Clone)")
        {
            damaged = true;
            currentHealth -= darkDamage;
            healthSlider1.value = currentHealth;

            if ((currentHealth <= 0) && (isDead == false))
            {
                Death();
            }
        }
        else if (collision.gameObject.name == "attLightPrefab(Clone)")
        {
            damaged = true;
            currentHealth -= lightDamage;
            healthSlider1.value = currentHealth;

            if ((currentHealth <= 0) && (isDead == false))
            {
                Death();
            }
        }
        else if (collision.gameObject.name == "attHybridPrefab(Clone)")
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

    //increase the attack range of the enemy (if applicable) - (very soft enrage)
    IEnumerator IncreaseRange()
    {
        
        if((i < 10) && (cd == false) && (GetComponent<CapsuleCollider2D>() != null))
        {
            cd = true;
            //yield return new WaitForSeconds(1f);
            yield return new WaitForSeconds(10f);
            capsule.size = new Vector2(capsule.size.x + 1f, capsule.size.y + 0.5f);
            
            i++;
            cd = false;

        }
    }

    
}
