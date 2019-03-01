using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This is used to manage the player's attacking animations and attacks
/// </summary>

public class PlayerAttack : MonoBehaviour
{
    private bool isAttacking = false;               //determines if the player is attacking
    int attackType = 1;                             //player's current attack type

    public Animator animate;                        //animator component
    public Text attackText;                         //attack style text
    public Image outline1, outline2, outline3;      //attack style outline
    public GameObject att1, att2, att3;             //player projectile objects


    void Start()
    {
        animate = GetComponent<Animator>();     //get animator component
        attackText = GameObject.Find("AttackStyle").GetComponent<Text>();   //get attack text
    }


    void Update ()
    {
        //get the status of the scrollwheel
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        //switch between player attack styles through scrolling the scroll wheel
        if (scroll < 0f)    //scrolling down i think?
        {
            if (attackType < 3)
            {
                attackType++;
                if (attackType == 2)
                {
                    outline1.enabled = false;
                    outline3.enabled = false;
                    outline2.enabled = true;
                    attackText.text = "Hybrid";
                }
                else if (attackType == 3)
                {
                    outline1.enabled = false;
                    outline2.enabled = false;
                    outline3.enabled = true;
                    attackText.text = "Light";
                }
            }
            else
            {
                attackType = 1;
                outline2.enabled = false;
                outline3.enabled = false;
                outline1.enabled = true;
                attackText.text = "Dark";
            }
        }
        else if (scroll > 0f)   //scrolling up i think?
        {
            if (attackType > 1)
            {
                attackType--;
                if (attackType == 1)
                {
                    outline2.enabled = false;
                    outline3.enabled = false;
                    outline1.enabled = true;
                    attackText.text = "Dark";
                }
                else if (attackType == 2)
                {
                    outline1.enabled = false;
                    outline3.enabled = false;
                    outline2.enabled = true;
                    attackText.text = "Hybrid";
                }
            }
            else
            {
                attackType = 3;
                outline1.enabled = false;
                outline2.enabled = false;
                outline3.enabled = true;               
                attackText.text = "Light";
            }
        }
        //switch to attack style 1 (dark) with the 1 key
        else if (Input.GetKey(KeyCode.Alpha1))
        {
            attackType = 1;
            outline2.enabled = false;
            outline3.enabled = false;
            outline1.enabled = true;
            attackText.text = "Dark";
        }
        //switch to attack style 2 (hybrid) with the 2 key
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            attackType = 2;
            outline1.enabled = false;
            outline3.enabled = false;
            outline2.enabled = true;
            attackText.text = "Hybrid";
        }
        //switch to attack style 3 (light) with the 3 key
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            attackType = 3;
            outline1.enabled = false;
            outline2.enabled = false;
            outline3.enabled = true;
            attackText.text = "Light";
        }
        //if not currently attacking, and left mouse button is pressed, start attacking
        if (isAttacking == false && Input.GetMouseButton(0))
        {
            //save position of where the mouse was clicked
            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            StartCoroutine(Attack(attackType));
            StartCoroutine(SpawnAttack(attackType, pos));
        }
        
    }

    //animate based on which attack style was used
    IEnumerator Attack(int atk)
    {
        //animate light attack
        if(atk == 1)
        {
            isAttacking = true;
            animate.SetBool("Attack1", true);
            yield return new WaitForSeconds(1f);    //wait for animation
            animate.SetBool("Attack1", false);
            isAttacking = false;
        }
        //animate hybrid attack
        else if(atk == 2)
        {
            isAttacking = true;
            animate.SetBool("Attack2", true);
            yield return new WaitForSeconds(1f);    //wait for animation
            animate.SetBool("Attack2", false);
            isAttacking = false;
        }
        //animate dark attack
        else if(atk == 3)
        {
            isAttacking = true;
            animate.SetBool("Attack3", true);
            yield return new WaitForSeconds(1f);    //wait for animation
            animate.SetBool("Attack3", false);
            isAttacking = false;
        }
    }

    //spawn the projectiles and pass the saved location of the mouse when the attack was initiated
    //the projectiles are managed in PlayerProjectiles.cs
    IEnumerator SpawnAttack(int atk, Vector3 pos)
    {
        pos.z = 0;
        yield return new WaitForSeconds(0.5f);  //wait for animation
        var playerPos = transform.position + (new Vector3(0, 1, 0));
        //spawm light attack
        if(atk == 1)
        {
            GameObject a1 = Instantiate(att1, playerPos, Quaternion.identity);
            a1.GetComponent<PlayerProjectiles>().target = pos;
        }
        //spawn hybrid attack
        else if(atk == 2)
        {
            GameObject a2 = Instantiate(att2, playerPos, Quaternion.identity);
            a2.GetComponent<PlayerProjectiles>().target = pos;
        }
        //spawn dark attack
        else if(atk == 3)
        {
            GameObject a3 = Instantiate(att3, playerPos, Quaternion.identity);
            a3.GetComponent<PlayerProjectiles>().target = pos;
        }
    }
}


