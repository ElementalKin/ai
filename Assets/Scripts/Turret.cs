using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public int damage;
    public int range;
    public int attackspeed;
    public int type;
    float timer;

    // Update is called once per frame
    void Update()
    {
        //Used for the attackspeed so that the tower isnt firing all the time.
        timer += Time.deltaTime;
        //If enough time has passed.
        if (timer > attackspeed)
        {
            //find an AI in the turrets range.
            GameObject[] Targets = GameObject.FindGameObjectsWithTag("AI");
            for (int i = 0; i < Targets.Length; i++)
            {
                if (Vector3.Distance(transform.position, Targets[i].transform.position) < range)
                {
                    Targets[i].GetComponent<DijkstraAIMovment>().health -=  (damage -Targets[i].GetComponent<DijkstraAIMovment>().armour);
                    break;
                }
            }
            timer = 0;
        }
    }
}
