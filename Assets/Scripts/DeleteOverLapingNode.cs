using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteOverLapingNode : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Grid" || collision.collider.tag == "GridPath")
        {
            Destroy(collision.gameObject);
        }

    }
}
