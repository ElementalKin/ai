using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;

public class Djikstra : MonoBehaviour
{
    struct Node
    {
        public Vector3 position;
        public bool visited;
        public float gScore;
        public float fScore;
        public Node[] edges;
    };
    public GameObject Terrain;
    public Rigidbody AI;
    public GameObject Target;
    public GameObject NodeDispaly;
    private Node CurrentNode;
    private Node DestinationNode;
    Node[,] path = new Node[100, 100];

    // Start is called before the first frame update
    void Start()
    {
        path[50, 50].position = new Vector3(Terrain.transform.position.x,0, Terrain.transform.position.z);
        path[50, 50].gScore = 0;
        for (int x = 0; x < 50; x++)
        {
            for (int i = 0; i < 50; i++)
            {
                path[50 + i, 50 + x].position = new Vector3(path[50, 50].position.x + i,0, path[50, 50].position.z + x);
                path[50 + i, 50 + x].gScore = 0;
                AddEdges(path, 50+ i, 50+ x);
                path[50 + i, 50 + x].visited = false;
                path[50 - i, 50 - x].position = new Vector3(path[50, 50].position.x - i,0, path[50, 50].position.z - x);
                path[50 - i, 50 - x].gScore = 0;
                AddEdges(path, 50 - i, 50 - x);
                path[50 - i, 50 - x].visited = false;
                path[50 - i, 50 + x].position = new Vector3(path[50, 50].position.x - i,0, path[50, 50].position.z + x);
                path[50 - i, 50 + x].gScore = 0;
                AddEdges(path, 50 - i, 50 + x);
                path[50 - i, 50 + x].visited = false;
                path[50 + i, 50 - x].position = new Vector3(path[50, 50].position.x + i,0, path[50, 50].position.z - x);
                path[50 + i, 50 - x].gScore = 0;
                AddEdges(path, 50 + i, 50 - x);
                path[50 + i, 50 - x].visited = false;
            }
        }
        for (int x = 0; x < 100; x++)
        {
            for (int i = 0; i < 100; i++)
            {
                Instantiate(NodeDispaly, path[i,x].position, Quaternion.identity);
            }
        }
        CurrentNode = path[50, 50];
        InvokeRepeating("FindPath", 0, 1.0f);
    }
    void AddEdges(Node[,] main, int x, int y)
    {
        if ( y + 1 > 100) { }
        else {main[x, y].edges[0] = main[x , y + 1];}
        if (x + 1 > 100) { }
        else { main[x, y].edges[2] = main[x + 1, y]; }
        if (y - 1 < 0) { }
        else {main[x, y].edges[3] = main[x, y - 1];}
        if (x - 1 < 0) { }
        else {main[x, y].edges[1] = main[x - 1, y]; }
    }
    


    // Update is called once per frame
    void Update()
    {
    }
    void FindPath()
    {
        for (int x = 0; x < 100; x++)
        {
            for (int i = 0; i < 100; i++)
            {
                if (Vector3.Distance(path[i,x].position, AI.transform.position) < Vector3.Distance(CurrentNode.position, AI.transform.position)) { CurrentNode = path[i, x]; }
            }
        }
        CheckEdges(CurrentNode, CurrentNode.gScore);
    }
    void CheckEdges(Node main, float s)
    {
        for (int i = 0; i < 4; i++)
        {
            if(main.edges[i].visited == false)
            {
                main.edges[i].gScore = s + 1;
                main.edges[i].visited = true;
            }
            CheckEdges(main.edges[i], s + 1);
        }
    }
    void Move()
    {
        float dot = Vector3.Dot(transform.forward,(DestinationNode.position - transform.position).normalized);
        if (dot < .9f) { transform.Rotate(new Vector3(0, 1, 0)); }
        //else if (dot < 0f) { transform.Rotate(new Vector3(0, -1, 0)); }
        //if(transform.position != DestinationNode.position) { AI.AddForce(transform.forward, ForceMode.VelocityChange); }

    }
}
