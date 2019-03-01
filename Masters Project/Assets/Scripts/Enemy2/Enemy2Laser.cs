using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is used to create the laser that shoots from the MOUTH of Enemy2
/// </summary>

public class Enemy2Laser : MonoBehaviour
{
    public bool laser1 = false;         //used to transistion between laser stages - laser stage 1
    public bool laser2 = false;         //used to transistion between laser stages - laser stage 2
    public bool shoot1 = false;         //used to transistion between laser stages - determines if the laser is shooting
    public float laserWidth = 0.1f;     //width of rendered line

    public GameObject player;           //player object
    public GameObject laserObject;      //actual laser that damages the player
    public GameObject laserH1;          //child object controlling one of the hand lasers
    public GameObject laserH2;          //child object controlling one of the hand lasers
    public Animator animate;            //animator componenet of the enemy

    //RaycastHit hit;                     //dont think i need this?
    LineRenderer lineRenderer;          //linerenderer component
    Vector2 newHit;                     //vector used for the lasers



	void Start ()
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
        if (animate.GetBool("Enemy2Attack1") == true)
        {
            float dist1;    //length of laser
            Ray2D ray;      //ray to render line on
            StartCoroutine("ChooseLaser");      //coroutine to transition laser stages

            //laser stage 1
            while (laser1 == true)
            {
                //render a line between the enemy and the player for 1.333333 seconds
                lineRenderer.enabled = true;
                ray = new Ray2D(transform.localPosition, player.transform.position - transform.position);
                lineRenderer.SetPosition(0, ray.origin);

                dist1 = Vector3.Distance(transform.position, player.transform.position); //ok
                newHit.x = (ray.direction * dist1).x;
                newHit.y = (ray.direction * dist1).y;
                lineRenderer.SetPosition(1, newHit);
                yield return null;
            }
            //laser stage 2
            if (laser2 == true)
            {
                //get the final line the real laser will be created on
                lineRenderer.enabled = true;
                ray = new Ray2D(transform.localPosition, player.transform.position - transform.position);
                lineRenderer.SetPosition(0, ray.origin);

                dist1 = Vector3.Distance(transform.position, player.transform.position); //ok
                newHit.x = (ray.direction * dist1).x;
                newHit.y = (ray.direction * dist1).y;
                lineRenderer.SetPosition(1, newHit);


                dist1 = Vector3.Distance(transform.position, player.transform.position);
                laser2 = false;
                //get direction and angle between the enemy and the player's current postion
                Vector3 dir = player.transform.position - transform.position;
                dir = player.transform.InverseTransformDirection(dir);
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                //wait 0.3333333 seconds to allow the player to move out of the way
                yield return new WaitForSeconds(0.33333333f);
                //create the actual laser object that can damage the player and set its damage
                //managed through DestroyLaser.cs
                GameObject L1 = Instantiate(laserObject, transform.position, Quaternion.identity);
                L1.GetComponent<DestoryLaser>().attackDamage = transform.parent.GetComponent<Enemy2>().attDamage;
                //rotate the laser towards the player and increase its length slightly
                if (angle > 0)
                {
                    L1.transform.Rotate(0, 0, angle - 90);
                    L1.transform.localScale = new Vector3(1, (dist1 * ray.direction).y + 15, 1);
                }
                else
                {
                    L1.transform.Rotate(0, 0, angle + 90);
                    L1.transform.localScale = new Vector3(1, (dist1 * ray.direction).y - 15, 1);                    
                }             
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
            yield return new WaitForSeconds(1.33333333f);   //wait for animation (mouth opens)
            laser1 = false;
            laser2 = true;
            yield return new WaitForSeconds(2f);    //wait until the animation is complete  
            shoot1 = false;

            //set the childen's shoot1 (checks if the laser is shooting) to false because the
            //children objects will be deativated before they can do it themselves
            laserH1.GetComponentInChildren<Enemy2LaserH>().shoot1 = false;
            laserH2.GetComponentInChildren<Enemy2LaserH>().shoot1 = false;
        }

    }


}