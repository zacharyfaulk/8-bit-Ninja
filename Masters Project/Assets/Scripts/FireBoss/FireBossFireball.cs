using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is used to manage the Fireball object created by FireBoss's ranged attack
/// </summary>

public class FireBossFireball : MonoBehaviour
{
    public bool offCD = true;       //determines if a new fire can be created
    public float attDamage;         //how much damage the attack does
    public float speed = 1;         //speed at which the projectile moves

    public GameObject fire;         //fire gameobject
    public Transform player;        //player object
    PlayerHealth playerHealth;      //player health
    Rigidbody2D rBody;              //object's rigidbody componenet (prob dont need to do it this way)



    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();                    //get rigidbody component
        player = GameObject.FindWithTag("Player").transform;    //get player object
        playerHealth = player.GetComponent<PlayerHealth>();     //get player health

        //rotate the object towards the player
        float z = Mathf.Atan2((player.transform.position.y - transform.position.y), (player.transform.position.x - transform.position.x)) * Mathf.Rad2Deg - 90;
        transform.eulerAngles = new Vector3(0, 0, z - 90);
    }

    void Update()
    {
        //move the object in a straight line after being rotated
        rBody.velocity = gameObject.transform.right * -speed;
        StartCoroutine(CreateFire());
    }

    //create a fire object every 0.5 seconds at this object's current location
    //fire object managed with Fire.cs
    IEnumerator CreateFire()
    {
        if (offCD == true)
        {
            offCD = false;
            yield return new WaitForSeconds(0.5f);
            Instantiate(fire, transform.position, Quaternion.identity);
            offCD = true;
        }
        
    }

    //on collision with the player or a surface object, destroy this object
    private void OnTriggerEnter2D(Collider2D collision)
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
