using System;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
// Implements a simple oscillation force that mimics a spring
public class ForceScript : MonoBehaviour
{

    protected Rigidbody rb;  
    
    float k = 5f; // Spring constant
    Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(10, 0, 0); // Initial velocity
        startPos = rb.position;
              
    }

    // Update is called once per frame
    void Update()
    {
        applyOscillationForce(rb);
    }
    
    void applyOscillationForce(Rigidbody rb){
        Vector3 force = -k * (rb.position - startPos);
        rb.AddForce(force);
    }
}
