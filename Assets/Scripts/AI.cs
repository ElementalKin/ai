using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public GameObject Target;
    public GameObject FleeFrom;
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
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, FleeFrom.transform.position) < 5)
        {
            v = ((FleeFrom.transform.position - transform.position) * MaxVelocity).normalized;
            force = v - CurrentVelocity;
            CurrentVelocity += force * Time.deltaTime;
            transform.position += -(CurrentVelocity * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(CurrentVelocity); ;
        }
        else
        {
            v = ((Target.transform.position - transform.position) * MaxVelocity).normalized;
            force = v - CurrentVelocity;
            CurrentVelocity += force * Time.deltaTime;
            transform.position += CurrentVelocity * Time.deltaTime;
            transform.rotation = Quaternion.LookRotation(CurrentVelocity);
        }







    }

}
