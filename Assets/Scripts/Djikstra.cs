using System.Collections.Generic;
using UnityEngine;



public class Djikstra : MonoBehaviour
{

    public struct Edges
    {
        public Node EdgeNode;
        public int x, y;
    };
    [System.Serializable]
    public struct Node
    {
        public Vector3 position;
        public int beforex, beforez;
        public bool IsAssigned;
        public float gScore;
        public Edges[] edges;
    };
    public GameObject Terrain;
    public int numberOfAI = 0;
    public GameObject StartingPosition;
    public GameObject[] AI = new GameObject[1000];
    public GameObject Target;
    public GameObject NodeDispaly;
    public GameObject PathDispaly;
    private Node CurrentNode;
    private Node DestinationNode;
    Node[,] path = new Node[100, 100];
    Queue<Node> NodePath = new Queue<Node>();
    GameObject[,] pathDisplay = new GameObject[100,100];
    int PathSize = 100;

    // Start is called before the first frame update
    void Start()
    {
        for (int x = 0; x < PathSize; x++)
        {
            for (int i = 0; i < PathSize; i++)
            {
                path[i, x].position = new Vector3(-PathSize / 2 + i, 0, -PathSize / 2 + x);
                path[i, x].gScore = 0;
                path[i, x].IsAssigned = true;
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
        CreateNodes();
        FindPath();
        Move();
    }
    void Update()
    {

    }
    void AddEdges(Node[,] main, int x, int y)
    {
        main[x, y].edges = new Edges[4];
        if (y + 1 >= PathSize) { }
        else
        {
            main[x, y].edges[0].EdgeNode = path[x, y + 1];
            main[x, y].edges[0].x = x;
            main[x, y].edges[0].y = y + 1;
        }

        if (x + 1 >= PathSize) { }
        else
        {
            main[x, y].edges[2].EdgeNode = path[x + 1, y];
            main[x, y].edges[2].x = x + 1;
            main[x, y].edges[2].y = y;
        }

        if (y - 1 < 0) { }
        else
        {
            main[x, y].edges[3].EdgeNode = path[x, y - 1];
            main[x, y].edges[3].x = x;
            main[x, y].edges[3].y = y - 1;
        }

        if (x - 1 < 0) { }
        else
        {
            main[x, y].edges[1].EdgeNode = path[x - 1, y];
            main[x, y].edges[1].x = x - 1;
            main[x, y].edges[1].y = y;
        }

    }



    // Update is called once per frame

    void CreateNodes()
    {
        for (int x = 0; x < PathSize; x++)
        {
            for (int i = 0; i < PathSize; i++)
            {
                pathDisplay[i,x] = Instantiate(NodeDispaly, path[i,x].position, Quaternion.identity);
                NodeDisplayScript a = pathDisplay[i,x].GetComponent<NodeDisplayScript>();
                a.node = path[i, x];
            }
        }
    }
    void FindPath()
    {

        for (int x = 0; x < PathSize; x++)
        {
            for (int i = 0; i < PathSize; i++)
            {
                if (AI != null)
                {
                    if (Vector3.Distance(path[i, x].position, path[0,0].position) < Vector3.Distance(CurrentNode.position, path[0, 0].position)) { CurrentNode = path[i, x]; }
                    if (Vector3.Distance(path[i, x].position, Target.transform.position) < Vector3.Distance(DestinationNode.position, Target.transform.position)) { DestinationNode = path[i, x]; }
                }
            }
        }
        CheckEdges(CurrentNode, path);
    }
    void CheckEdges(Node main, Node[,] path)
    {
        Queue<Node> ToDoList = new Queue<Node>();
        Queue<Node> CalculatedMap = new Queue<Node>();
        Queue<Vector3> Done = new Queue<Vector3>();
        Node cur = main;
        ToDoList.Enqueue(cur);
        while (ToDoList.Count > 0)
        {
            cur = ToDoList.Peek();
            if (cur.IsAssigned)
            {
                if (!Done.Contains(cur.position))
                {
                    if (cur.position.Equals(CurrentNode.position))
                    {

                        CalculatedMap.Enqueue(cur);
                        if (cur.position.Equals(DestinationNode.position))
                        {
                            CalculatePath(cur, main, CalculatedMap);
                            break;
                        }

                        Done.Enqueue(cur.position);
                    }
                    else
                    {
                        CalculatedMap.Enqueue(cur);
                        if (cur.position.Equals(DestinationNode.position))
                        {
                            CalculatePath(cur, main, CalculatedMap);
                            return;
                        }

                        Done.Enqueue(cur.position);

                    }
                    for (int i = 0; i < 4; i++)
                    {
                        if (cur.edges[i].EdgeNode.IsAssigned)
                        {
                            if (!Done.Contains(path[cur.edges[i].x, cur.edges[i].y].position) && !(ToDoList.Contains(path[cur.edges[i].x,cur.edges[i].y])))
                            {
                                path[cur.edges[i].x, cur.edges[i].y].beforex = (int)cur.position.x + PathSize/2;
                                path[cur.edges[i].x, cur.edges[i].y].beforez = (int)cur.position.z + PathSize/2;
                                path[cur.edges[i].x, cur.edges[i].y].gScore = cur.gScore + 1;
                                ToDoList.Enqueue(path[cur.edges[i].x, cur.edges[i].y]);
                            }
                        }
                    }

                }
                ToDoList.Dequeue();
            }
        }
    }
    void CalculatePath(Node EndNode, Node Start, Queue<Node> CalculatedMap)
    {
        Queue<Vector3> Done = new Queue<Vector3>();
        Queue<Node> CalculatedPath = new Queue<Node>();
        Node cur = EndNode;
        Node tmp = EndNode;
        Instantiate(PathDispaly, tmp.position, Quaternion.identity);
        CalculatedPath.Enqueue(tmp);
        while (!cur.Equals(Start) && CalculatedPath.Count < 1000)
        {
            for (int i = 0; i < 4; i++)
            {
                if (CalculatedMap.Contains(path[cur.edges[i].x, cur.edges[i].y]))
                {
                    if (path[cur.edges[i].x, cur.edges[i].y].IsAssigned)
                    {
                        if (path[cur.edges[i].x, cur.edges[i].y].gScore == tmp.gScore)
                        {
                            if (Vector3.Distance(path[cur.edges[i].x, cur.edges[i].y].position, Start.position) < Vector3.Distance(tmp.position, Start.position))
                            {
                                tmp = path[cur.edges[i].x, cur.edges[i].y];
        }
                        }
                        else if (path[cur.edges[i].x, cur.edges[i].y].gScore < tmp.gScore)
                        {
                            tmp = path[cur.edges[i].x, cur.edges[i].y];
                        }
                    }
                }
            }
            Instantiate(PathDispaly, tmp.position, Quaternion.identity);
            CalculatedPath.Enqueue(tmp);
            cur = tmp;
        }
        NodePath = CalculatedPath;
    }
    void Move()
    {
        Node[] tmp = new Node[NodePath.Count];
        for (int i = NodePath.Count; i > 0; i--)
        {
            tmp[i-1] = NodePath.Peek();
            NodePath.Dequeue();
        }
        for (int i = tmp.Length; i > 0; i--)
        {
            NodePath.Enqueue(tmp[i-1]);
        }
        for(int i = 0; i < numberOfAI; i++)
        {
            DijkstraAIMovment a = AI[i].GetComponent<DijkstraAIMovment>();
            a.path = NodePath;
        }
    }

}
