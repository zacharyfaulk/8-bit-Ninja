using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is used to manage the ranged projectile object created by Enemy3
/// </summary>

public class Enemy3Projectiles : MonoBehaviour
{
    public float attDamage;         //how much damage the attack does
    public float speed = 1;         //speed at which this projectile moves
                
    public Transform player;        //player object         
    PlayerHealth playerHealth;      //player health
    Rigidbody2D rBody;              //object's rigidbody component (prob dont need to do it this way)

    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();                        //get rigidbody component
        player = GameObject.FindWithTag("Player").transform;        //get player object
        playerHealth = player.GetComponent<PlayerHealth>();         //get player's health

        //rotate the object towards the player
        float z = Mathf.Atan2((player.transform.position.y - transform.position.y), (player.transform.position.x - transform.position.x)) * Mathf.Rad2Deg - 180;
        transform.eulerAngles = new Vector3(0, 0, z - 90);
    }

    void Update()
    {
        //move the object in a straight line after being rotated
        rBody.velocity = gameObject.transform.up * -speed;
    }

    //on collision with the player or a surface object, destroy this object
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Surface" || collision.gameObject.tag == "Player")
        {
            //if collided with a player, damage the player
            if (collision.gameObject.tag == "Player")
            {
                if (playerHealth.currentHealth > 0)
                {
                    playerHealth.TakeDamage(attDamage);
                }
            }
            Destroy(gameObject);
        }
    }
}
