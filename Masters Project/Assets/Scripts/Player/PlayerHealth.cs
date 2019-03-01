using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// This is used to manage the health of the player
/// </summary

public class PlayerHealth : MonoBehaviour
{
    //bool damaged = false;               //if the player is being damaged (not currently used)
    bool isDead = false;                //if the player is dead (not really used)                    
    public bool easyMode = false;       //if the player is on easymode
    public float startHealth = 100;     //starting health of the player
    public float currentHealth;         //current health of the player

    //health bars of the player (large UI bar in the bottom left and smaller bar under the player)
    public Slider healthSlider1, healthSlider2;     
    public AudioClip damageClip;        //damaged sound audio clip
    AudioSource playerAudio;            //audio player object
        

	void Start ()
    {
        playerAudio = GetComponent<AudioSource>();      //get the audio component
        currentHealth = startHealth;                    //set the current health
        healthSlider1 = GameObject.Find("UIHealth").GetComponent<Slider>();     //find the UI component for health
	}
	
	void Update ()
    {
        //if (damaged == true)
        //{

        //}
        //damaged = false;
    }

    //function that removes health from the player
    //called when an enemy attack hits the player
    //the amount of damage taken is determined by the enemy
    public void TakeDamage (float amount)
    {
        //damaged = true;
        currentHealth -= amount;
        healthSlider1.value = currentHealth;
        healthSlider2.value = currentHealth;
        //playerAudio.Play();     //play damaged audio clip

        //if the player's health drop to 0 or below, call the death function
        if ((currentHealth <= 0) && (isDead == false))
        {
            Death();
        }
    }

    //if easy mode is on, allow the player to continue playing on death
    //if easy mode is off, restart the game (scene) on death
    void Death()
    {
        if (easyMode == false)
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Courtyard");
        }
    }
}
