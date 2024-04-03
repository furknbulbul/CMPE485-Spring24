using System;
using System.Collections.Generic;
using UnityEngine;

public class MazeRenderer : Singleton<MazeRenderer> 
{
    [SerializeField] MazeGenerator mazeGenerator;

    [SerializeField] List<GameObject> mazeCellPrefabs;

    [HideInInspector] public float scale = 12f;

    [SerializeField] GameObject KeyPrefab;

    [SerializeField] GameObject ChestPrefab;

    [SerializeField] GameObject DragonPrefab;


    [SerializeField] Vector2[] DragonPaths;

    [SerializeField] Vector2 KeyPositionIndex;

    [SerializeField] Vector2 ChestPositionIndex;

    [SerializeField] Vector2[] DragonPositionIndexes;

    [SerializeField] Quaternion[] DragonRotations;

    




    private void SpawnMaze(GameState state)
    {

        if (state != GameState.Initialization) return;

        GameObject mazeCellPrefab = mazeCellPrefabs.Find(x => x.name == "Variant1");

        float terrainSize = mazeCellPrefab.GetComponent<MazeCellObject>().GetComponentInChildren<Terrain>().terrainData.size.x;
        scale = terrainSize ;

        MazeCell[,] maze = mazeGenerator.getMaze(GameManager.mazeSize, GameManager.mazeSize);


        for (int i = 0; i < maze.GetLength(0); i++)
        {   
            
            for (int j = 0; j < maze.GetLength(1); j++)
            {
                
                var possiblePrefabs = new List<GameObject> { mazeCellPrefabs.Find(x => x.name == "Variant1") };


                MazeCell cell = maze[i, j];

                GameObject mazeCellObject;
                MazeCellObject mazeCell;
             
                bool top = cell.wallTop;
                bool left = cell.wallLeft;
                bool right = false;
                bool bottom = false;
                if (i==mazeGenerator.width - 1) right = true;
                if (j==0) bottom = true;

                MazeCell topCell = null;
                MazeCell bottomCell = null;
                MazeCell leftCell = null;
                MazeCell rightCell = null;

                if (j < maze.GetLength(1) - 1) topCell = maze[i, j + 1];
                if (j > 0) bottomCell = maze[i, j - 1];
                if (i > 0) leftCell = maze[i - 1, j];
                if (i < maze.GetLength(0) - 1) rightCell = maze[i + 1, j];

                if (left && right ||(left && rightCell != null && rightCell.wallLeft) || (right && leftCell != null && leftCell.wallRight)){ 
                    possiblePrefabs.Add(mazeCellPrefabs.Find(x => x.name == "Variant1_Spike2"));
                } 
                if ((top && bottom) || (bottom && bottomCell != null && bottomCell.wallTop)|| (top && topCell != null && topCell.wallBottom)){
                    possiblePrefabs.Add(mazeCellPrefabs.Find(x => x.name == "Variant1_Spike"));
                }
                mazeCellPrefab = possiblePrefabs[UnityEngine.Random.Range(0, possiblePrefabs.Count)];
                mazeCellObject = Instantiate(mazeCellPrefab, new Vector3(i * scale, 0, j * scale), Quaternion.identity);                
                mazeCell = mazeCellObject.GetComponent<MazeCellObject>();
                mazeCell.InitializeWalls(top, bottom, left, right);   

            }
            
        }
        SpawnKey();
        SpawnChest();
        SpawnDragons();
        GameManager.Instance.UpdateGameState(GameState.Playing);
        Debug.Log("Game state is playing");
    }
 
    

    private void SpawnKey()
    {
        Instantiate(KeyPrefab, new Vector3(KeyPositionIndex.x * scale + 6, 0.5f, KeyPositionIndex.y * scale+ 6), Quaternion.identity);
    }

    private void SpawnChest()
    {   
        Instantiate(ChestPrefab, new Vector3(ChestPositionIndex.x * scale + 6, 0f, ChestPositionIndex.y * scale+ 6), Quaternion.Euler(0f, 180f, 0f));
    }

    private void SpawnDragons(){
        for (int i = 0; i < DragonPositionIndexes.Length; i++)
        {
            GameObject dragon = SpawnDragon(DragonPositionIndexes[i].x, DragonPositionIndexes[i].y, scale, DragonRotations[i]);
            Vector2[] indexes = new Vector2[2];
            Array.Copy(DragonPaths, i * 2, indexes, 0, 2);
            Transform[] path = new Transform[2];
            for (int j = 0; j < 2; j++)
            {
                GameObject pathObject = new GameObject();
                pathObject.transform.position = new Vector3(indexes[j].x * scale + 6, 0.5f, indexes[j].y * scale + 6);
                path[j] = pathObject.transform;
            }
            dragon.GetComponent<DragonController>().path = new List<Transform>(path);
        }
    }

    private GameObject SpawnDragon(float i, float j, float scale, Quaternion quaternion)
    {   
        
        GameObject dragon = Instantiate(DragonPrefab, new Vector3(i * scale + 6, -1f, j * scale+ 6), quaternion);

        dragon.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        return dragon;
    }


    protected override void Awake()
    {
        GameManager.OnGameStateChanged += SpawnMaze;
        GameManager.OnGameStateChanged += RestartAll;

    }



    protected override void OnDestroy()
    {
        GameManager.OnGameStateChanged -= SpawnMaze;
        GameManager.OnGameStateChanged -= RestartAll;
        base.OnDestroy();
    }

    public void RestartAll(GameState state){
        if (state != GameState.Restart) return;
        RestartKey();
        RestartChest();

        // //RestartDragons(state);
    }

    private void RestartKey ()
    {
    
        GameObject key = GameObject.FindWithTag("Key");
        if (key != null) Destroy(key);
        SpawnKey();
    }

    private void RestartChest ()
    {
    
        GameObject chest = GameObject.FindWithTag("Chest");
        if (chest != null) Destroy(chest);
        SpawnChest();
    }

    private void RestartDragons(){
        GameObject[] dragons = GameObject.FindGameObjectsWithTag("Dragon");
        for (int i = 0; i < dragons.Length; i++)
        {
            Destroy(dragons[i]);
        }
        SpawnDragons();
    }

    




}
