using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class DragonController : MonoBehaviour
{


    public List<Transform> path;

    public float speed = 5f;

    private bool alreadyHit;

    private Animator anim;

    protected void Awake()
    {
        GameManager.OnGameStateChanged += RestartDragon;
    }

    protected void OnDestroy()
    {
        GameManager.OnGameStateChanged -= RestartDragon;
    }

    private void Start()
    {   
        alreadyHit = false;
        Transform StartPoint = new GameObject().transform;
        anim = GetComponent<Animator>();
    
        StartCoroutine(MoveDragonForward(path, speed));
    }

    private void Update()
    {

    }

    

   

    IEnumerator MoveDragonForward(List<Transform> path, float speed)
    {   
        anim.SetTrigger("Walk");
        for (int i = 0; i < path.Count - 1; i++)
        {
            Transform startPoint = path[i];
            Transform endPoint = path[i+1];
            

            float length = Vector3.Distance(startPoint.position, endPoint.position);

            float start = Time.time;

            while(Vector3.Distance(transform.position, endPoint.position) > 0.001f){
                // movement 
                float fraction = (Time.time - start)*speed / length;
                transform.position = Vector3.Lerp(startPoint.position, endPoint.position, fraction);
                //

                yield return null;
            }
        }

        yield return new WaitForSeconds(3);

        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, 0f);
        float rotationSpeed = 30f; // Adjust the rotation speed as needed

        float t = 0f;
        while (t < 1f)
        {
            t += rotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, t);
            yield return null;
        }
        transform.rotation = targetRotation;

        StartCoroutine(MoveDragonBackward(path, speed)); 
    }

    IEnumerator MoveDragonBackward(List<Transform> path, float speed)
    {

        
        for (int i = path.Count - 1; i > 0; i--)
        {
            Transform startPoint = path[i];
            Transform endPoint = path[i-1];
            

            float length = Vector3.Distance(startPoint.position, endPoint.position);

            float start = Time.time;

            while(Vector3.Distance(transform.position, endPoint.position) > 0.001f){
                // movement
                float fraction = (Time.time - start)*speed / length;
                transform.position = Vector3.Lerp(startPoint.position, endPoint.position, fraction);

                yield return null;
        }
        }
        yield return new WaitForSeconds(3); 
        
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0f, 180f, 0f);
        float rotationSpeed = 30f; 

        float t = 0f;
        while (t < 1f)
        {
            t += rotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, t);
            yield return null;
        }
        transform.rotation = targetRotation;

        StartCoroutine(MoveDragonForward(path, speed)); 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (alreadyHit) return;
        
        PlayerController playerController =  other.GetComponent<PlayerController>();
    
        if (playerController != null)
        {
            playerController.Die();
            alreadyHit = true;
            StopAllCoroutines();
        }
    }

    private void RestartDragon(GameState state){
        if (state != GameState.Restart) return;
       
        alreadyHit = false;
        StartCoroutine(MoveDragonForward(path, speed));
    }

    

    
  

}
