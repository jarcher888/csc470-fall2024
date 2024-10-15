using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;

public class GameScript : MonoBehaviour
{

    public GameObject cubePrefab;
    public CubeScript[,] grid;

    public TMP_Text currentMode;
    public TMP_Text timerText;
    public TMP_Text aliveText;
    public TMP_Text alivePercent;
    public TMP_Text endText;

    float spacing = 1.1f;

    float simulationTimer;
    float simulationRate = .1f;

    public int xSize = 20;
    public int zSize = 20;

    bool started = false;
    bool hasWon = false;
    public string mode;

    float timer = 10f;

    int aliveCount = 0;
    float alivePercentage = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        endText.text = "";
        alivePercent.text = "Percent alive: " + alivePercentage + "%";
        aliveText.text = "Alive: " + aliveCount;
        timerText.text = timer + "s";
        mode = "glider";

        simulationTimer = simulationRate;

        grid = new CubeScript[xSize,zSize];
        for (int i = 0; i < xSize; i++){
            for (int j = 0; j < zSize; j++){
                Vector3 pos = transform.position;
                pos.x += i * spacing;
                pos.z += j * spacing;
                GameObject cube = Instantiate(cubePrefab, pos, Quaternion.identity);

                grid[i,j] = cube.GetComponent<CubeScript>();

                grid[i,j].alive = false;
                grid[i,j].xIndex = i;
                grid[i,j].yIndex = j;
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasWon){
            aliveCount = 0;
            for (int i = 0; i < xSize; i++){
                for (int j = 0; j < zSize; j++){
                    if (grid[i,j].alive){
                        aliveCount++;
                    }
                }
            }
            alivePercentage = (float)aliveCount / (xSize * zSize) * 100f;
            
            // Debug.Log(alivePercentage);
            
            // aliveText.text = "Alive: " + aliveCount;
            alivePercent.text = "Percent alive: " + alivePercentage + "%";


            currentMode.text = "Current mode: " + mode;
            if (Input.GetKeyDown(KeyCode.Space)){
                started = !started;
            }else if (Input.GetKeyDown(KeyCode.Q)){
                mode = "glider";
            }else if (Input.GetKeyDown(KeyCode.W)){
                mode = "smiley";
            }else if (Input.GetKeyDown(KeyCode.E)){
                mode = "zigzag";
            }else if (Input.GetKeyDown(KeyCode.R)){
                mode = "plus";
            }else if (Input.GetKeyDown(KeyCode.T)){
                mode = "flower";
            }
            
            simulationTimer -= Time.deltaTime;

            if (started){
                timer -= Time.deltaTime;
                timerText.text = Mathf.Round(timer) + "s";
                if (simulationTimer < 0){
                    Simulate();
                    simulationTimer = simulationRate;
                    aliveText.text = "Alive: " + aliveCount;
                    alivePercent.text = "Percent alive: " + alivePercentage + "%";
                }
            }else{
                timer = 10f;
            }
            if (timer < 0){
                hasWon = true;
            }
        }else if (alivePercentage >= 15){
            endText.text = "Congratulations, you win!";
        }else{
            endText.text = "Oops! You lose";
        }
    }

    void Simulate(){
        // Debug.Log("inside simulate");
        bool[,] nextAlive = new bool[xSize,zSize];

        for (int x = 0; x < xSize; x++){
            for (int y = 0; y < zSize; y++){
                int neighbors = CountNeighbors(x,y);


                if (neighbors < 2 && grid[x,y].alive){
                    nextAlive[x,y] = false;
                }else if ((neighbors == 2 || neighbors == 3) && grid[x,y].alive){
                    nextAlive[x,y] = true;
                }else if (neighbors > 3 && grid[x,y].alive){
                    nextAlive[x,y] = false;
                }else if (!grid[x,y].alive && neighbors == 3){
                    nextAlive[x,y] = true;
                }else {
                    nextAlive[x,y] = grid[x,y].alive;
                }
            }
        }

        for (int i = 0; i < xSize; i++){
            for (int j = 0; j < zSize; j++){
                grid[i,j].alive = nextAlive[i,j];
                grid[i,j].ChangeColor();
            }
            
        }

        
    }

    public int CountNeighbors(int xIndex, int yIndex){
        int numNeighbors = 0;

        for (int x = xIndex - 1; x <= xIndex + 1; x++){
            for (int y = yIndex - 1; y <= yIndex + 1; y++){
                if (x >= 0 && x < xSize && y >= 0 && y < zSize){
                    if (!(x == xIndex && y == yIndex)){
                        if (grid[x,y].alive){
                            numNeighbors++;
                        }
                    }
                }
            }
        }

        return numNeighbors;
    }

}
