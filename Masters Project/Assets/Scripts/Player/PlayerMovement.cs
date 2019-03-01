using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is used to move the player based on the keys pressed
/// </summary>

public class PlayerMovement : MonoBehaviour
{
    public float speed;         //speed at which the player moves
    public Animator animate;    //animator component

    void Start()
    {
        animate = GetComponent<Animator>();     //get animator component
    }

    //move with WASD and set the moving animation to true when moving
    void Update ()
    {
        //move up
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector2.up * (speed * Time.deltaTime));
            animate.SetBool("Moving", true);
        }

        //move left
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.left * (speed * Time.deltaTime));
            animate.SetBool("Moving", true);
        }

        //move down
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector2.down * (speed * Time.deltaTime));
            animate.SetBool("Moving", true);
        }

        //move right
        else if ( Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * (speed * Time.deltaTime));
            animate.SetBool("Moving", true);
        }

        //not moving
        else
        {
            animate.SetBool("Moving", false);
        }

	}
}
