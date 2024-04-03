using UnityEngine;

public class KeyBehaviour : MonoBehaviour
{
    

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Chest")
        {
            Debug.Log("Key collided with chest");

            collision.gameObject.GetComponent<Animator>().SetTrigger("Reached");
            GameManager.Instance.Canvas.SetActive(true);
            Destroy(gameObject);
        }
    }
    // void Start()
    // {
        
    // }


    // void Update()
    // {
        
    // }
}
