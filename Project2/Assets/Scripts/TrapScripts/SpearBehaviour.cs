using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearBehaviour : MonoBehaviour
{
    

    private PlayerController playerController;

    public bool Ontrap;

    void Start()
    {
        Ontrap = false;
        playerController = null; 
      
    }

    
    void Update()
    {
      
        
    }

    private void OnTriggerEnter(Collider other){
        playerController = other.GetComponent<PlayerController>();
        if (Ontrap && playerController != null){ 
            playerController.Die();
            GetComponentInParent<SpikeTrap>().StopAllCoroutines();
            GetComponentInParent<SpikeTrap>().Ontrap = false;
        }
    
       
    }

    
}
