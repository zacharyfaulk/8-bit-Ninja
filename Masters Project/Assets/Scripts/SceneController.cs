using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///  This is used to manage the game itself
//   The game currently has 10 waves
//   each wave has a set of hardcoded enemies that will spawn
//   -> the location and type (dark, light, or hybrid) of the enemy will be choosen randomly
//   The enemies will spawn 1 second after each other and the next wave will start
//   once all the enemies on the current wave are defeated
/// </summary>

public class SceneController : MonoBehaviour
{
    public List<GameObject> activeEnemies;      //manages the enemies in each wave   
    public Text waveText;                       //manages the wave text in the player UI
    public Transform global;                    //global object

    //all the enemy prefabs that can be used
    public GameObject e1D, e1L, e1H, e2D, e2L, e2H, e3D, e3L, e3H, e4D, e4L, e4H, FireBoss;

    void Start ()
    {
        waveText = GameObject.Find("WaveText").GetComponent<Text>();        //get wave text
        global = GameObject.FindWithTag("Global").transform;                //get the global object
        StartCoroutine(Game1());
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    //the enemies that spawn each wave of the game is programmed manually at the moment
    IEnumerator Game1()
    {
        //wait 5 seconds before the first wave starts
        yield return new WaitForSeconds(5f);
        
        for (int i = 1; i < 11; i++)
        {
            //spawn 1 Enemy1 on wave 1
            if(i == 1)
            {
                waveText.text = "Wave: " + i;
                GameObject e1 = RandomE1();
                activeEnemies.Add(e1);

                while (activeEnemies.Count > 0)
                {
                    yield return null;
                }
                yield return new WaitForSeconds(2f);
            }
            //spawn 2 Enemy1s on wave 2
            if (i == 2)
            {
                waveText.text = "Wave: " + i;
                GameObject e1 = RandomE1();
                yield return new WaitForSeconds(1f);
                activeEnemies.Add(e1);
                GameObject e2 = RandomE1();
                activeEnemies.Add(e2);

                while (activeEnemies.Count > 0)
                {
                    yield return null;
                }
                yield return new WaitForSeconds(2f);
            }
            //spawn 1 Enemy2 on wave 3
            if (i == 3)
            {
                waveText.text = "Wave: " + i;
                //GameObject e1 = RandomE1();
                //activeEnemies.Add(e1);
                //GameObject e2 = RandomE1();
                //activeEnemies.Add(e2);
                GameObject e3 = RandomE2();
                activeEnemies.Add(e3);

                while (activeEnemies.Count > 0)
                {
                    yield return null;
                }
                yield return new WaitForSeconds(2f);
            }
            //spawn 2 Enemy2s on wave 4
            if (i == 4)
            {
                waveText.text = "Wave: " + i;
                //GameObject e1 = RandomE1();
                //activeEnemies.Add(e1);
                //GameObject e2 = RandomE1();
                //activeEnemies.Add(e2);
                GameObject e3 = RandomE2();
                activeEnemies.Add(e3);
                yield return new WaitForSeconds(1f);
                GameObject e4 = RandomE2();
                activeEnemies.Add(e4);

                while (activeEnemies.Count > 0)
                {
                    yield return null;
                }
                yield return new WaitForSeconds(2f);
            }
            //spawn 1 Enemy3 on wave 5
            if (i == 5)
            {
                waveText.text = "Wave: " + i;
                //GameObject e1 = RandomE1();
                //activeEnemies.Add(e1);
                //GameObject e2 = RandomE1();
                //activeEnemies.Add(e2);
                //GameObject e3 = RandomE2();
                //activeEnemies.Add(e3);
                GameObject e4 = RandomE3();
                activeEnemies.Add(e4);

                while (activeEnemies.Count > 0)
                {
                    yield return null;
                }
                yield return new WaitForSeconds(2f);
            }
            //spawn 2 Enemy3s on wave 6
            if (i == 6)
            {
                waveText.text = "Wave: " + i;
                //GameObject e1 = RandomE1();
                //activeEnemies.Add(e1);
                //GameObject e2 = RandomE1();
                //activeEnemies.Add(e2);
                //GameObject e3 = RandomE1();
                //activeEnemies.Add(e3);
                GameObject e4 = RandomE3();
                activeEnemies.Add(e4);
                yield return new WaitForSeconds(1f);
                GameObject e5 = RandomE3();
                activeEnemies.Add(e5);

                while (activeEnemies.Count > 0)
                {
                    yield return null;
                }
                yield return new WaitForSeconds(2f);
            }
            //spawn 1 Enemy4 on wave 7
            if (i == 7)
            {
                waveText.text = "Wave: " + i;
                //GameObject e1 = RandomE1();
                //activeEnemies.Add(e1);
                //GameObject e2 = RandomE1();
                //activeEnemies.Add(e2);
                //GameObject e3 = RandomE1();
                //activeEnemies.Add(e3);
                //GameObject e4 = RandomE2();
                //activeEnemies.Add(e4);
                //GameObject e5 = RandomE2();
                //activeEnemies.Add(e5);
                GameObject e6 = RandomE4();
                activeEnemies.Add(e6);

                while (activeEnemies.Count > 0)
                {
                    yield return null;
                }
                yield return new WaitForSeconds(2f);
            }
            //spawn 2 Enemy4s on wave 8
            if (i == 8)
            {
                waveText.text = "Wave: " + i;
                //GameObject e1 = RandomE1();
                //activeEnemies.Add(e1);
                //GameObject e2 = RandomE1();
                //activeEnemies.Add(e2);
                //GameObject e3 = RandomE1();
                //activeEnemies.Add(e3);
                //GameObject e4 = RandomE2();
                //activeEnemies.Add(e4);
                //GameObject e5 = RandomE2();
                //activeEnemies.Add(e5);
                GameObject e6 = RandomE4();
                activeEnemies.Add(e6);
                yield return new WaitForSeconds(1f);
                GameObject e7 = RandomE4();
                activeEnemies.Add(e7);

                while (activeEnemies.Count > 0)
                {
                    yield return null;
                }
                yield return new WaitForSeconds(2f);
            }
            //spawn 1 of each of the basic enemies on wave 9
            if (i == 9)
            {
                waveText.text = "Wave: " + i;
                GameObject e1 = RandomE1();
                activeEnemies.Add(e1);
                yield return new WaitForSeconds(1f);
                //GameObject e2 = RandomE1();
                //activeEnemies.Add(e2);
                //GameObject e3 = RandomE1();
                //activeEnemies.Add(e3);
                GameObject e4 = RandomE2();
                activeEnemies.Add(e4);
                yield return new WaitForSeconds(1f);
                //GameObject e5 = RandomE2();
                //activeEnemies.Add(e5);
                GameObject e6 = RandomE3();
                activeEnemies.Add(e6);
                yield return new WaitForSeconds(1f);
                GameObject e7 = RandomE4();
                activeEnemies.Add(e7);
                //GameObject e8 = RandomE4();
                //activeEnemies.Add(e8);

                while (activeEnemies.Count > 0)
                {
                    yield return null;
                }
                yield return new WaitForSeconds(2f);
            }
            //spawn the FireBoss on wave 10
            if (i == 10)
            {
                waveText.text = "Wave: " + i;
                var position = new Vector3(0, 0, 0);
                GameObject e1 = Instantiate(FireBoss, position, Quaternion.identity);
                activeEnemies.Add(e1);
                
                while (activeEnemies.Count > 0)
                {
                    yield return null;
                }
                yield return new WaitForSeconds(2f);
            }

        }

        //text displayed once the game is beaten
        if (global.transform.GetComponent<GlobalVar>().easyMode == true)
        {
            waveText.text = "You beat the game on easy mode!";
        }
        else if (global.transform.GetComponent<GlobalVar>().easyMode == false)
        {
            waveText.text = "You beat the game on normal mode!";
        }
        
        yield return null;
    }

    //function to spawn an Enemy1 at a random location
    public GameObject RandomE1()
    {
        var position = new Vector3(Random.Range(-50f, 50f), Random.Range(-10f, 10f), 0);

        //20% a hybrid version will spawn
        if (Random.value < .2)
        {           
            GameObject e1 = Instantiate(e1H, position, Quaternion.identity);
            return e1;
        }
        //80% a dark or light version will spawn
        else
        {
            if (Random.value > .5)
            {
                GameObject e1 = Instantiate(e1D, position, Quaternion.identity);
                return e1;
            }
            else
            {
                GameObject e1 = Instantiate(e1L, position, Quaternion.identity);
                return e1;
            }
        }
    }

    //function to spawn an Enemy2 at a random location
    public GameObject RandomE2()
    {
        var position = new Vector3(Random.Range(-50f, 50f), Random.Range(-10f, 10f), 0);

        //20% a hybrid version will spawn
        if (Random.value < .2)
        {
            GameObject e = Instantiate(e2H, position, Quaternion.identity);
            return e;
        }
        //80% a dark or light version will spawn
        else
        {
            if (Random.value > .5)
            {
                GameObject e = Instantiate(e2D, position, Quaternion.identity);
                return e;
            }
            else
            {
                GameObject e = Instantiate(e2L, position, Quaternion.identity);
                return e;
            }
        }
    }

    //function to spawn an Enemy3 at a random location
    public GameObject RandomE3()
    {
        var position = new Vector3(Random.Range(-50f, 50f), Random.Range(-10f, 10f), 0);

        //20% a hybrid version will spawn
        if (Random.value < .2)
        {
            GameObject e = Instantiate(e3H, position, Quaternion.identity);
            return e;
        }
        //80% a dark or light version will spawn
        else
        {
            if (Random.value > .5)
            {
                GameObject e = Instantiate(e3D, position, Quaternion.identity);
                return e;
            }
            else
            {
                GameObject e = Instantiate(e3L, position, Quaternion.identity);
                return e;
            }
        }
    }

    //function to spawn an Enemy4 at a random location
    public GameObject RandomE4()
    {
        var position = new Vector3(Random.Range(-50f, 50f), Random.Range(-10f, 10f), 0);

        //20% a hybrid version will spawn
        if (Random.value < .2)
        {
            GameObject e = Instantiate(e4H, position, Quaternion.identity);
            return e;
        }
        //80% a dark or light version will spawn
        else
        {
            if (Random.value > .5)
            {
                GameObject e = Instantiate(e4D, position, Quaternion.identity);
                return e;
            }
            else
            {
                GameObject e = Instantiate(e4L, position, Quaternion.identity);
                return e;
            }
        }
    }

}
