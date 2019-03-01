using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is used to create the laser and fireball object
/// that shoot from the mouth for FireBoss
/// </summary>

public class FireBossRangeAttack : MonoBehaviour
{
    public bool laser1 = false;         //used to transistion between laser stages - laser stage 1
    public bool laser2 = false;         //used to transistion between laser stages - shooting fireball
    public bool shoot1 = false;         //used to transistion between laser stages - determines if enemy is attacking
    public float laserWidth = 0.5f;     //width of rendered line

    public GameObject player;           //player object
    public GameObject rangeObject;      //fireball object
    public Animator animate;            //animator component

    //RaycastHit hit;                     //dont think i need this?
    LineRenderer lineRenderer;          //linerender component
    Vector2 newHit;                     //vector used for the lasers
    


    void Start()
    {
        player = GameObject.FindWithTag("Player");      //get player object
        lineRenderer = GetComponent<LineRenderer>();    //gets linerenderer
        lineRenderer.startWidth = laserWidth;           //sets laser width
        lineRenderer.endWidth = laserWidth;             //sets laser width
        animate = GetComponentInParent<Animator>();     //gets animator component
    }


    void Update()
    {
        StartCoroutine(ShootLaser());
    }


    IEnumerator ShootLaser()
    {
        //start the attacking process if the enemy is "attacking"
        if (animate.GetBool("FireBossRangeAttack1") == true)
        {
            float dist1;    //length of laser
            Ray2D ray;      //ray to render line on
            StartCoroutine("ChooseLaser");      //coroutine to transition laser stages

            //laser stage 1
            while (laser1 == true)
            {
                //render a line between the enemy and the player for 2.2222222 seconds
                lineRenderer.enabled = true;
                ray = new Ray2D(transform.localPosition, player.transform.position - transform.position);
                lineRenderer.SetPosition(0, ray.origin);

                dist1 = Vector3.Distance(transform.position, player.transform.position); //ok
                newHit.x = (ray.direction * dist1).x;
                newHit.y = (ray.direction * dist1).y;
                lineRenderer.SetPosition(1, newHit);
                yield return null;
            }

            //laser stage 2 (creating the fireball)
            if (laser2 == true)
            {
                //create the actual fireball object that can damage the player
                //managed through FireBossFireball.cs
                Instantiate(rangeObject, transform.position, Quaternion.identity);
                laser2 = false;

                //below would be used for a laser attack instead of a fireball attack
                ///////////////////////////////////////////////////////////////////////////////////////////
                //L1.GetComponent<DestoryLaser>().attackDamage = transform.parent.GetComponent<FireBossHeads>().attDamage;

                /*lineRenderer.enabled = true;
                ray = new Ray2D(transform.localPosition, player.transform.position - transform.position);
                lineRenderer.SetPosition(0, ray.origin);

                dist1 = Vector3.Distance(transform.position, player.transform.position); //ok
                newHit.x = (ray.direction * dist1).x;
                newHit.y = (ray.direction * dist1).y;
                lineRenderer.SetPosition(1, newHit);
                dist1 = Vector3.Distance(transform.position, player.transform.position);
                laser2 = false;
                Vector3 dir = player.transform.position - transform.position;
                dir = player.transform.InverseTransformDirection(dir);
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                yield return new WaitForSeconds(0.33333333f);
                GameObject L1 = Instantiate(rangeObject, transform.position, Quaternion.identity);
                L1.GetComponent<DestoryLaser>().attackDamage = transform.parent.GetComponent<FireBossHeads>().attDamage;


                if (angle > 0)
                {
                    L1.transform.Rotate(0, 0, angle - 90);
                    L1.transform.localScale = new Vector3(1, (dist1 * ray.direction).y + 15, 1);
                }
                else
                {
                    L1.transform.Rotate(0, 0, angle + 90);
                    L1.transform.localScale = new Vector3(1, (dist1 * ray.direction).y - 15, 1);
                }*/
                ///////////////////////////////////////////////////////////////////////////////////////////
            }

        }
        //disable linerenderer when not attacking   
        lineRenderer.enabled = false;
    }

    //transitions betweeen laser stages
    IEnumerator ChooseLaser()
    {
        if (shoot1 == false)
        {
            shoot1 = true;
            laser1 = true;
            yield return new WaitForSeconds(2.22222222f);   //wait for animation (mouth opens)
            laser1 = false;
            laser2 = true;
            yield return new WaitForSeconds(1.11111111f);   //wait until the animation is complete  
            shoot1 = false;
        }

    }
}
