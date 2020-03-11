using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DijkstraAIMovment : MonoBehaviour
{
    int startingHealth;
    public int health;
    public int armour;
    public GameObject AIControl;
    public LinkedList<Djikstra.Node> path = new LinkedList<Djikstra.Node>();
    Quaternion RotationTarget;
    private LinkedListNode<Djikstra.Node> CurrentTarget;
    public Vector3 CurrentVelocity;
    private Vector3 force;
    private Vector3 v;
    public float MaxVelocity;
    public float Speed;
    public Image HealthBar;
    // Start is called before the First frame update
    void Start()
    {
        startingHealth = health;
    }
    // Update is called once per frame
    void Update()
    {

        HealthBar.fillAmount = (float)health / startingHealth;
        if (CurrentTarget != null)
        {
            if (health <= 0 || CurrentTarget.Next == null)
            {
                Destroy(gameObject);
            }
        }
        if (path.Count != 0)
        {
            if (CurrentTarget == null)
            {
                CurrentTarget = path.First;
            }
            if (transform.position.x >= CurrentTarget.Value.position.x - .5f && transform.position.x <= CurrentTarget.Value.position.x + .5f && transform.position.z >= CurrentTarget.Value.position.z - .5f && transform.position.z <= CurrentTarget.Value.position.z + .5f)
            {
                CurrentTarget = CurrentTarget.Next;
            }

            v = ((CurrentTarget.Value.position - transform.position) * MaxVelocity).normalized;
            force = v - CurrentVelocity;
            CurrentVelocity += force * Time.deltaTime;
            transform.position += CurrentVelocity * (Speed *Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(new Vector3(90,CurrentVelocity.y, 0));
        }
    }
}
