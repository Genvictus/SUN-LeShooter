using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pet : MonoBehaviour
{
    public Transform target;
    public NavMeshAgent pet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(this.transform.position, target.position, 10 * Time.deltaTime);
    }
}
