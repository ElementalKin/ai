using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;


public class Djikstra : MonoBehaviour
{
    //public GameObject FleeFrom;
    //Quaternion RotationTarget;
    //public Vector3 CurrentVelocity;
    //private Vector3 force;
    //private Vector3 v;
    //public float MaxVelocity;
    //public float Speed;
    //private float RotationSpeed;
    //public float weight;
    //public float dot;


    struct Edges
    {
        public int x, y;
    };
    struct Node
    {
        public Vector3 position;
        public bool visited;
        public float gScore;
        public float fScore;
        public Edges[] edges;
        public GameObject Display;
    };
    public GameObject Terrain;
    public Rigidbody AI;
    public GameObject Target;
    public GameObject NodeDispaly;
    public GameObject NodeDispaly2;
    public GameObject NodeDispaly3;
    public GameObject NodeDispaly4;
    public GameObject NodeDispaly5;
    private Node CurrentNode;
    private Node DestinationNode;
    Node[,] path = new Node[100, 100];
    int PathSize = 100;

    // Start is called before the first frame update
    void Start()
    {
        for (int x = 0; x < PathSize; x++)
        {
            for (int i = 0; i < PathSize; i++)
            {
                path[i, x].position = new Vector3(-PathSize/2 + i,0, -PathSize / 2 + x);
                path[i, x].gScore = 0;
                path[i, x].visited = false;
            }
        }
        for (int x = 0; x < PathSize; x++)
        {
            for (int i = 0; i < PathSize; i++)
            {
                AddEdges(path, i, x);
                //path[i, x].Display = NodeDispaly; Instantiate(path[i, x].Display, path[i, x].position, Quaternion.identity); 
            }
        }
        FindPath();
    }
    void AddEdges(Node[,] main, int x, int y)
    {
        main[x, y].edges = new Edges[4]; 
        if (y + 1 >= PathSize) { }
        else { main[x, y].edges[0].x = x;
               main[x, y].edges[0].x = y + 1;}

        if (x + 1 >= PathSize) { }
        else { main[x, y].edges[2].x = x + 1;
               main[x, y].edges[2].y = y;}

        if (y - 1 < 0) { }
        else { main[x, y].edges[3].x = x;
               main[x, y].edges[3].y = y- 1;}

        if (x - 1 < 0) { }
        else { main[x, y].edges[1].x = x -1;
               main[x, y].edges[1].y = y;}

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
                if (Vector3.Distance(path[i, x].position, Target.transform.position) < Vector3.Distance(CurrentNode.position, Target.transform.position)) { DestinationNode = path[i, x]; }
            }
        }
        CheckEdges(CurrentNode, path);
    }
    void CheckEdges(Node main, Node[,] path)
    {
        if (main.Equals(CurrentNode))
        {
            for (int i = 0; i < 4; i++)
            {
                if (path[main.edges[i].x, main.edges[i].y].edges != null)
                {
                    if (path[main.edges[i].x, main.edges[i].y].visited == false)
                    {

                        path[main.edges[i].x, main.edges[i].y].gScore = main.gScore + 1;
                        Instantiate(NodeDispaly2, path[main.edges[i].x, main.edges[i].y].position, Quaternion.identity);

                        if (main.edges[i].Equals(DestinationNode))
                        {
                            CalculateWeight(DestinationNode);
                            break;
                        }


                    }
                }
            }
            main.visited = true;
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                if (path[main.edges[i].x, main.edges[i].y].edges != null)
                {
                    if (path[main.edges[i].x, main.edges[i].y].visited == false)
                    {
                        path[main.edges[i].x, main.edges[i].y].gScore = main.gScore + 1;
                        Instantiate(NodeDispaly2, path[main.edges[i].x, main.edges[i].y].position, Quaternion.identity);

                        if (main.edges[i].Equals(DestinationNode))
                        {
                            CalculateWeight(DestinationNode);
                            break;
                        }


                    }
                }
            }
        }
        for(int i = 0; i < 4; i++)
        {
            if (path[main.edges[i].x, main.edges[i].y].visited == false)
            {
                if (path[main.edges[i].x, main.edges[i].y].edges != null)
                {
                    path[main.edges[i].x, main.edges[i].y].visited = true;
                    CheckEdges(path[main.edges[i].x, main.edges[i].y], path);

                }
            }
        }
    }
    void CalculateWeight(Node main)
    {
        for (int i = 0; i < 4; i++)
        {
            //if (main.edges[i].gScore)
        }
    }
    void Move(Node targetNode)
    {
        //float dot = Vector3.Dot(transform.forward,(DestinationNode.position - transform.position).normalized);
        //if (dot < .9f) { transform.Rotate(new Vector3(0, 1, 0)); }
        //else if (dot < 0f) { transform.Rotate(new Vector3(0, -1, 0)); }
        //if(transform.position != targetNode.position) { AI.AddForce(transform.forward, ForceMode.VelocityChange); }
        //v = ((targetNode.position - transform.position) * MaxVelocity).normalized;
        //force = v - CurrentVelocity;
        //CurrentVelocity += force * Time.deltaTime;
        //transform.position += CurrentVelocity * Time.deltaTime;
        //Quaternion.RotateTowards(transform.rotation,new Quaternion(CurrentVelocity.x,CurrentVelocity.y,CurrentVelocity.z,0),10);
    }

}
