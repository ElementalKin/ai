using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DijkstraAIMovment : MonoBehaviour
{
    public GameObject AIControl;
    public Queue<Djikstra.Node> path = new Queue<Djikstra.Node>();
    Quaternion RotationTarget;
    public Vector3 CurrentVelocity;
    private Vector3 force;
    private Vector3 v;
    public float MaxVelocity;
    public float Speed;
    private float RotationSpeed;
    public float weight;
    public float dot;
    // Start is called before the first frame update
    void Start()
    {
        Djikstra a = AIControl.GetComponent<Djikstra>();
        a.AI[a.numberOfAI] = gameObject;
        a.numberOfAI++;
    }
    // Update is called once per frame
    void Update()
    {
        if (transform.position == path.Peek().position)
        {
            path.Dequeue();
        }
        if (path.Count == 0)
        {

        }
        else
        {
            v = ((path.Peek().position - transform.position) * MaxVelocity).normalized;
            force = v - CurrentVelocity;
            CurrentVelocity += force * Time.deltaTime;
            transform.position += CurrentVelocity * Time.deltaTime;
            transform.rotation = Quaternion.LookRotation(CurrentVelocity);
        }
    }
}
