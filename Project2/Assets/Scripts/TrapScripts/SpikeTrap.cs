using System.Collections;
using UnityEngine;

public class SpikeTrap : MonoBehaviour {

    
    private PlayerController playerController;


    public Animator spikeTrapAnim;


    private SpearBehaviour[] spears;

    public bool Ontrap;

  

    

    void Start()
    {
        Ontrap = false;
        playerController = null;
        spikeTrapAnim = GetComponent<Animator>();
        spears = GetComponentsInChildren<SpearBehaviour>();
        StartCoroutine(OpenTrap());
    }

    private void OnTriggerEnter(Collider other){
       playerController = other.GetComponent<PlayerController>();
       if (playerController != null){
            Debug.Log("Player is on trap");
            Ontrap = true;
        }
    
    }
    
    private void OnTriggerExit(Collider other){
        playerController = other.GetComponent<PlayerController>();
        if (playerController != null){
            Ontrap = false;
            
        }
    }

    private void Update(){
        foreach (SpearBehaviour spear in spears){
                spear.Ontrap = Ontrap;
            }     
    }

    public IEnumerator OpenTrap(){
        
        spikeTrapAnim.SetTrigger("open");
        yield return new WaitForSeconds(1);
        StartCoroutine(CloseTrap());
    }

    public IEnumerator CloseTrap(){
        
        spikeTrapAnim.SetTrigger("close");
        yield return new WaitForSeconds(3);
        StartCoroutine(OpenTrap());
    }

    public void RestartTrap(){
        StopAllCoroutines();
        Ontrap = false;        
        for (int i = 0; i < spears.Length; i++){
            spears[i].Ontrap = false;
        }
        StartCoroutine(OpenTrap());
        
    }

    

   

   
}