using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MazeGenerator : MonoBehaviour
{   

    public int width, height;
    public int startX = 0, startY = 0;
    MazeCell[,] maze;
    Vector2Int currentCell;
 
    

    List<Direction> directions = new List<Direction> {
     Direction.Up, Direction.Down, Direction.Left, Direction.Right 
     
    };

    public MazeCell[,] getMaze(int width, int height){
        this.width = width;
        this.height = height;
        initializeMaze();
        generateMaze();
        return maze;
    }


    List<Direction>  GetRandomDirections(int x, int y){
        List<Direction> randomDirections = new List<Direction>();
        List<Direction> directionCopy = new List<Direction>(directions); 
        UnityEngine.Random.InitState(x * y + x + y + (int)Math.Pow(x+1, y+1));
        while (directionCopy.Count > 0){
            int index = UnityEngine.Random.Range(0, directionCopy.Count);
            randomDirections.Add(directionCopy[index]);
            directionCopy.RemoveAt(index);
        }
        return randomDirections;

    }

    bool IsCellValid(Vector2Int cell){
        if (cell.x < 0 || cell.x >= width || cell.y < 0 || cell.y >= height || maze[cell.x, cell.y].visited){
            return false;
        }
        return true;
    }

    

    private void initializeMaze(){
        maze = new MazeCell[width, height];

        for (int i = 0; i < width; i++){
            for (int j = 0; j < height; j++){
                maze[i, j] = new MazeCell(i, j);
            }
        }

    }

    private void generateMaze(){
        Stack<Vector2Int> stack = new Stack<Vector2Int>();
        currentCell = new Vector2Int(startX, startY);
        maze[currentCell.x, currentCell.y].visited = true;
        stack.Push(currentCell);

        while (stack.Count > 0){
            List<Direction> randomDirections = GetRandomDirections(currentCell.x, currentCell.y);
            bool hasValidCell = false;
            foreach (Direction direction in randomDirections){
                Vector2Int newCell = currentCell;
                switch (direction){
                    case Direction.Up:
                        newCell.y += 1;
                        break;
                    case Direction.Down:
                        newCell.y -= 1;
                        break;
                    case Direction.Left:
                        newCell.x -= 1;
                        break;
                    case Direction.Right:
                        newCell.x += 1;
                        break;
                }
                if (IsCellValid(newCell)){
                    hasValidCell = true;
                    stack.Push(currentCell);
                    RemoveWall(currentCell, newCell);
                    currentCell = newCell;
                    maze[currentCell.x, currentCell.y].visited = true;
                    break;
                }
            }
            if (!hasValidCell){
                currentCell = stack.Pop();
            }
        }

    }

    private void RemoveWall(Vector2Int currentCell, Vector2Int newCell){
        if (currentCell.x == newCell.x){
            if (currentCell.y < newCell.y){
               maze[currentCell.x, currentCell.y].wallTop = false;
               maze[newCell.x, newCell.y].wallBottom = false;
            } else {
                maze[newCell.x, newCell.y].wallTop = false;
                maze[currentCell.x, currentCell.y].wallBottom = false;
            }
        } else {
            if (currentCell.x > newCell.x){
                maze[currentCell.x, currentCell.y].wallLeft = false;
                maze[newCell.x, newCell.y].wallRight = false;
            } else {
                maze[newCell.x, newCell.y].wallLeft = false;
                maze[currentCell.x, currentCell.y].wallRight = false;
            }
        }
    }

  
}



public enum Direction {
    Up, Down, Left, Right
}


public class MazeCell {
    public bool visited;
    public int x, y; 

    public bool wallLeft, wallTop, wallRight, wallBottom;



    public Vector2Int position {
        get {
            return new Vector2Int(x, y);
        }
    }

    public MazeCell(int x, int y) {
        this.x = x;
        this.y = y;
        visited = false;
        wallLeft = true;
        wallTop = true;
        wallRight = false;
        wallBottom = false;
    }

}