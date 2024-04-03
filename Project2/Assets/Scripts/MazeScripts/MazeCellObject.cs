using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeCellObject : MonoBehaviour
{

    [SerializeField] GameObject topWall, bottomWall, leftWall, rightWall;

    public void InitializeWalls(bool top, bool bottom, bool left, bool right){
        topWall.SetActive(top);
        bottomWall.SetActive(bottom);
        leftWall.SetActive(left);
        rightWall.SetActive(right);
    }
    
}
