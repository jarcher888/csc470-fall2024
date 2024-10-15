using System.Collections;
using System.Collections.Generic;
// using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class CubeScript : MonoBehaviour
{

    public Renderer cubeRenderer;
    public Color aliveColor;
    public Color deadColor;

    public bool alive;
    public int xIndex;
    public int yIndex;

    bool[,] glider = new bool[3,3];
    bool[,] smiley = new bool[4,5];
    bool[,] zigzag = new bool[2,3];
    bool[,] plus = new bool[3,3];
    bool[,] flower = new bool[5,5];

    GameScript gameManager;


    // Start is called before the first frame update
    void Start()
    {
        InitializePatterns();


        ChangeColor();

        GameObject gmObj = GameObject.Find("GameManagerObject");
        gameManager = gmObj.GetComponent<GameScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeColor(){
        if (alive){
           cubeRenderer.material.color = deadColor;
        }else{
            cubeRenderer.material.color = aliveColor;
        }
    }

    void OnMouseDown(){
        //int neighbors = gameManager.CountNeighbors(xIndex, yIndex);
        // Debug.Log("clicked: " + xIndex + " " + yIndex);

        bool[,] nextAlive = new bool[gameManager.xSize,gameManager.zSize];


        if (gameManager.mode == "glider" && xIndex < gameManager.xSize - 2 && yIndex < gameManager.zSize - 2){
            int xCount = 0;
            int yCount = 0;
            for (int x = xIndex; x < xIndex + 3; x++){
                for (int y = yIndex; y < yIndex + 3; y++){
                    nextAlive[x,y] = glider[xCount,yCount];
                    xCount++;
                }
                yCount++;
                xCount = 0;
            }


            for (int x = xIndex; x < xIndex + 3; x++){
                for (int y = yIndex; y < yIndex + 3; y++){
                    gameManager.grid[x,y].alive = nextAlive[x,y];
                    gameManager.grid[x,y].ChangeColor();
                }
            }
        }else if (gameManager.mode == "smiley" && xIndex < gameManager.xSize - 4 && yIndex < gameManager.zSize - 3){
            int xCount = 0;
            int yCount = 0;
            for (int x = xIndex; x < xIndex + 5; x++){
                for (int y = yIndex; y < yIndex + 4; y++){
                    nextAlive[x,y] = smiley[xCount,yCount];
                    xCount++;
                }
                yCount++;
                xCount = 0;
            }


            for (int x = xIndex; x < xIndex + 5; x++){
                for (int y = yIndex; y < yIndex + 4; y++){
                    gameManager.grid[x,y].alive = nextAlive[x,y];
                    gameManager.grid[x,y].ChangeColor();
                }
            }
        }else if (gameManager.mode == "zigzag" && xIndex < gameManager.xSize - 2 && yIndex < gameManager.zSize - 1){
            int xCount = 0;
            int yCount = 0;
            for (int x = xIndex; x < xIndex + 3; x++){
                for (int y = yIndex; y < yIndex + 2; y++){
                    nextAlive[x,y] = zigzag[xCount,yCount];
                    xCount++;
                }
                yCount++;
                xCount = 0;
            }


            for (int x = xIndex; x < xIndex + 3; x++){
                for (int y = yIndex; y < yIndex + 2; y++){
                    gameManager.grid[x,y].alive = nextAlive[x,y];
                    gameManager.grid[x,y].ChangeColor();
                }
            }
        }else if (gameManager.mode == "plus" && xIndex < gameManager.xSize - 2 && yIndex < gameManager.zSize - 2){
            int xCount = 0;
            int yCount = 0;
            for (int x = xIndex; x < xIndex + 3; x++){
                for (int y = yIndex; y < yIndex + 3; y++){
                    nextAlive[x,y] = plus[xCount,yCount];
                    xCount++;
                }
                yCount++;
                xCount = 0;
            }


            for (int x = xIndex; x < xIndex + 3; x++){
                for (int y = yIndex; y < yIndex + 3; y++){
                    gameManager.grid[x,y].alive = nextAlive[x,y];
                    gameManager.grid[x,y].ChangeColor();
                }
            }
        }else if (gameManager.mode == "flower" && xIndex < gameManager.xSize - 4 && yIndex < gameManager.zSize - 4){
            int xCount = 0;
            int yCount = 0;
            for (int x = xIndex; x < xIndex + 5; x++){
                for (int y = yIndex; y < yIndex + 5; y++){
                    nextAlive[x,y] = flower[xCount,yCount];
                    xCount++;
                }
                yCount++;
                xCount = 0;
            }


            for (int x = xIndex; x < xIndex + 5; x++){
                for (int y = yIndex; y < yIndex + 5; y++){
                    gameManager.grid[x,y].alive = nextAlive[x,y];
                    gameManager.grid[x,y].ChangeColor();
                }
            }
        }

        // alive = !alive;
        // ChangeColor();
    }

    void InitializePatterns(){
        //Glider
        glider[0,0] = true;
        glider[1,0] = false;
        glider[2,0] = false;
        glider[0,1] = true;
        glider[1,1] = false;
        glider[2,1] = true;
        glider[0,2] = true;
        glider[1,2] = true;
        glider[2,2] = false;

        //Smiley
        smiley[0,0] = false;
        smiley[1,0] = true;
        smiley[2,0] = false;
        smiley[3,0] = false;
        smiley[0,1] = true;
        smiley[1,1] = false;
        smiley[2,1] = false;
        smiley[3,1] = true;
        smiley[0,2] = true;
        smiley[1,2] = false;
        smiley[2,2] = false;
        smiley[3,2] = false;
        smiley[0,3] = true;
        smiley[1,3] = false;
        smiley[2,3] = false;
        smiley[3,3] = true;
        smiley[0,4] = false;
        smiley[1,4] = true;
        smiley[2,4] = false;
        smiley[3,4] = false;

        //Zigzag
        zigzag[0,0] = true;
        zigzag[1,0] = false;
        zigzag[0,1] = true;
        zigzag[1,1] = true;
        zigzag[0,2] = false;
        zigzag[1,2] = true;

        //Plus
        plus[0,0] = false;
        plus[1,0] = true;
        plus[2,0] = false;
        plus[0,1] = true;
        plus[1,1] = true;
        plus[2,1] = true;
        plus[0,2] = false;
        plus[1,2] = true;
        plus[2,2] = false;

        //Flower
        flower[0,0] = false;
        flower[1,0] = false;
        flower[2,0] = true;
        flower[3,0] = false;
        flower[4,0] = false;
        flower[0,1] = false;
        flower[1,1] = true;
        flower[2,1] = false;
        flower[3,1] = true;
        flower[4,1] = false;
        flower[0,2] = true;
        flower[1,2] = false;
        flower[2,2] = true;
        flower[3,2] = false;
        flower[4,2] = true;
        flower[0,3] = false;
        flower[1,3] = true;
        flower[2,3] = false;
        flower[3,3] = true;
        flower[4,3] = false;
        flower[0,4] = false;
        flower[1,4] = false;
        flower[2,4] = true;
        flower[3,4] = false;
        flower[4,4] = false;

    }
}
