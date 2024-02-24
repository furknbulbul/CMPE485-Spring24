using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlScript : MonoBehaviour
{   

    protected Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.UpArrow)) {
            rb.AddRelativeForce(Vector3.forward * 5, ForceMode.Acceleration);
        } 
        if(Input.GetKey(KeyCode.DownArrow)) {
            rb.AddRelativeForce(Vector3.back * 5, ForceMode.Acceleration);
        }
        if(Input.GetKey(KeyCode.LeftArrow)) {
            rb.AddRelativeForce(Vector3.left * 5, ForceMode.Acceleration);
        }
        if(Input.GetKey(KeyCode.RightArrow)) {
            rb.AddRelativeForce(Vector3.right * 5, ForceMode.Acceleration);
        }
    }
}
