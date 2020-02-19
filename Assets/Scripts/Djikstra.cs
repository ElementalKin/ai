using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;

public class Djikstra : MonoBehaviour
{
    struct Node
    {
        public Vector2 position;
        public float gScore;
        public float fScore;
    };
    public GameObject Terrain;
    public GameObject AI;
    public GameObject Target;
    public GameObject NodeDispaly;
    Node[,] path = new Node[100, 100];

    // Start is called before the first frame update
    void Start()
    {
        path[50, 50].position = new Vector2(Terrain.transform.position.x, Terrain.transform.position.z);
        for (int x = 0; x < 50; x++)
        {
            for (int i = 0; i < 50; i++)
            {
                path[50 + i, 50 + x].position = new Vector2(path[50,50].position.x + i, path[50, 50].position.y + x);
                path[50 - i, 50 - x].position = new Vector2(path[50, 50].position.x - i, path[50, 50].position.y - x);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FindPath()
    {
        for (int i = 0; i < 100; i++)
        {
            //if (path[i,0].position >=) { }
        }
    }

    
}
