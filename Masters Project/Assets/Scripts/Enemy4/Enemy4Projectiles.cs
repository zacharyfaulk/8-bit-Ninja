using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is used to manage the magic projectile object created by Enemy4
/// </summary>

public class Enemy4Projectiles : MonoBehaviour
{
    public float attDamage;         //how much damage the attack does
    public float speed = 1;         //speed at which the projectile moves

    public Transform player;        //player object
    PlayerHealth playerHealth;      //player health
    Rigidbody2D rBody;              //object's rigidbody componenet (prob dont need to do it this way)

    void Start ()
    {
        player = GameObject.FindWithTag("Player").transform;        //get player object
        playerHealth = player.GetComponent<PlayerHealth>();         //get player health
        rBody = GetComponent<Rigidbody2D>();                        //get rigidbody component
    }

	void Update ()
    {
        //rotate and follow the player
        float z = Mathf.Atan2((player.transform.position.y - transform.position.y), (player.transform.position.x - transform.position.x)) * Mathf.Rad2Deg - 90;
        transform.eulerAngles = new Vector3(0, 0, z - 90);
        rBody.velocity = gameObject.transform.right * -speed;
    }

    //on collision with the player, a non-Enemy4 enemy, or a surface object, destroy this object
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Player" || collision.gameObject.tag == "Surface")
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
