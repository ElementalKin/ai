using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTower : MonoBehaviour
{
    //If something collides with the MainTower.
    private void OnCollisionEnter(Collision collision)
    {
        //Check if it is an AI.
        if (collision.collider.tag == "AI")
        {
            //Get the EventSystem GameObject.
            GameObject a = GameObject.FindGameObjectWithTag("EventControler");
            //Get the GeneralStuff Script attached to it.
            GeneralStuff b = a.GetComponent<GeneralStuff>();
            //Lower the players health by one
            b.playerHealth--;
            //Destroy the AI that collided with the Tower.
            Destroy(collision.collider.gameObject);
        }
    }
}
