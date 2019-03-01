using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is used to manage the player's projectiles
/// </summary>

public class PlayerProjectiles : MonoBehaviour
{
    public float speed;                 //projectile's speed
    public Vector3 target;              //attacking location passed by PlayerAttack.cs
    public Vector3 startPosition;       //starting postion of the projectile

	void Start ()
    {
        startPosition = transform.position;     //set starting position
	}


    void Update()
    {      
        //move the projectile in the direction of the target location
        transform.position = Vector3.MoveTowards(transform.position, (target-startPosition).normalized*1000, speed * Time.deltaTime);
    }

    //destroy the projectile upon collision with an enemy or a surface object
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Surface")
        {
            Destroy(gameObject);
        }
    }
}
