using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
struct Node
{
    Vector2 position;
    float gScore;
    float hScore;
    float fScore;
    Node* previous;
    std::vector<Edge> connections;
};
public class Djikstra : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
