using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DijkstraAIMovment : MonoBehaviour
{
    public GameObject AIControl;
    public Collider TargetNode;
    public LinkedList<Djikstra.Node> path = new LinkedList<Djikstra.Node>();
    Quaternion RotationTarget;
    public Vector3 CurrentVelocity;
    private Vector3 force;
    private Vector3 v;
    public float MaxVelocity;
    public float Speed;
    private float RotationSpeed;
    public float weight;
    public float dot;
    // Start is called before the First frame update
    void Start()
    {
        Djikstra a = AIControl.GetComponent<Djikstra>();
        a.AI[a.numberOfAI] = gameObject;
        a.numberOfAI++;
        GetGridObject(path.First.Value.position);
    }
    // Update is called once per frame
    void Update()
    {
        if (path.Count == 0)
        {

        }
        else
        {
            v = ((path.First.Value.position - transform.position) * MaxVelocity).normalized;
            force = v - CurrentVelocity;
            CurrentVelocity += force * Time.deltaTime;
            transform.position += CurrentVelocity * Time.deltaTime;
            transform.rotation = Quaternion.LookRotation(new Vector3(90,CurrentVelocity.y, 0));
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider == TargetNode)
        {
            path.RemoveFirst();
            GetGridObject(path.First.Value.position);
        }

    }


    void GetGridObject(Vector3 position)
    {
        Ray ray = new Ray(position, new Vector3(0,1,0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.tag == "GridPath") { TargetNode = hit.collider; }
        }
    }
}
