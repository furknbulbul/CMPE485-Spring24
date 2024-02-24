using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    public GameObject prefab;
    public Vector3 spawnRange = new Vector3(3, 3, 3);
    private Vector3 pos;

    private static int count = 0; 

    // Start is called before the first frame update
    void Start()
    {
        Transform obj = transform;
        pos = obj.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) ){
            if (count >= 10){
                return;
            }
            
            Vector3 spawnPos = new Vector3(Random.Range(pos.x-spawnRange.x,pos.x + spawnRange.x), Random.Range(pos.y-spawnRange.y, pos.y + spawnRange.y), Random.Range(pos.z-spawnRange.z, pos.z + spawnRange.z));
            Instantiate(prefab, spawnPos, Quaternion.identity);
            count++;
        }
       
        
    }
}
