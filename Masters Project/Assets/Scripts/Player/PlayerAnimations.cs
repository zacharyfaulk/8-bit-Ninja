using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is used to manage the player's moving animations
/// </summary>

public class PlayerAnimations : MonoBehaviour
{
    public Animator animate;    //animator component
    
	void Start ()
    {
        animate = GetComponent<Animator>();     //get animator component
	}
		
	void Update ()
    {
        IdleAnimation();
        MovementAnimation();
    }

    //Idle Animations
    void IdleAnimation()
    {
        if (Input.GetKey(KeyCode.W))
        {
            animate.SetBool("Up", true);
            animate.SetBool("Left", false);
            animate.SetBool("Down", false);
            animate.SetBool("Right", false);
        }

        if (Input.GetKey(KeyCode.A))
        {
            animate.SetBool("Up", false);
            animate.SetBool("Left", true);
            animate.SetBool("Down", false);
            animate.SetBool("Right", false);
        }

        if (Input.GetKey(KeyCode.S))
        {
            animate.SetBool("Up", false);
            animate.SetBool("Left", false);
            animate.SetBool("Down", true);
            animate.SetBool("Right", false);
        }

        if (Input.GetKey(KeyCode.D))
        {
            animate.SetBool("Up", false);
            animate.SetBool("Left", false);
            animate.SetBool("Down", false);
            animate.SetBool("Right", true);
        }
    }

    //Movement Animations
    void MovementAnimation()
    {
        if (Input.GetKey(KeyCode.W))
        {
            animate.SetBool("MoveUp", true);
        }
        else
        {
            animate.SetBool("MoveUp", false);
        }

        if (Input.GetKey(KeyCode.A))
        {
            animate.SetBool("MoveLeft", true);
        }
        else
        {
            animate.SetBool("MoveLeft", false);
        }

        if (Input.GetKey(KeyCode.S))
        {
            animate.SetBool("MoveDown", true);
        }
        else
        {
            animate.SetBool("MoveDown", false);
        }

        if (Input.GetKey(KeyCode.D))
        {
            animate.SetBool("MoveRight", true);
        }
        else
        {
            animate.SetBool("MoveRight", false);
        }
    }
}
