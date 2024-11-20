using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public GameObject popUpWindow;

    public TMP_Text nameText;
    public TMP_Text bioText;
    public TMP_Text statsText;
    public TMP_Text woodText;
    public TMP_Text stoneText;

    public GameObject infoButton;
    public GameObject castleButton;
    public GameObject cabinButton;

    public List<PlayerScript> units = new List<PlayerScript>();
    public PlayerScript selectedUnit;
    public TreeScript activeTree;
    int totalWood = 0;
    int totalStone = 0;

    LayerMask layerMask;
    public Camera mainCamera;

    public static Action<PlayerScript> UnitClicked;

    public GameObject castle;
    public GameObject cabin;

    bool cabinBuilt = false;
    bool castleBuilt = false;


    

    void OnEnable(){
        if (GameManager.instance == null){
            GameManager.instance = this;
        }else{
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        layerMask = LayerMask.GetMask("ground", "player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            Ray mousePositionRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(mousePositionRay, out hitInfo, Mathf.Infinity, layerMask)){
                if (hitInfo.collider.CompareTag("ground")){
                    if (selectedUnit != null){
                        selectedUnit.nma.SetDestination(hitInfo.point);
                    }
                }else if (hitInfo.collider.CompareTag("player")){
                    SelectUnit(hitInfo.collider.gameObject.GetComponent<PlayerScript>());
                }
            }
        }

        if (!castleBuilt){
            if (totalWood >= 5 && totalStone >= 10){
                castleButton.SetActive(true);
            }
        }else {
            castleButton.SetActive(false);
        }

        if (!cabinBuilt){
            if (totalWood >= 15){
                cabinButton.SetActive(true);
            }
        }else{
            cabinButton.SetActive(false);
        }

    }

    public void SelectUnit(PlayerScript p){

        UnitClicked?.Invoke(p);
        selectedUnit = p;

    }

    public void Tree(TreeScript tree){
        tree.lifeRemaining--;
        totalWood++;
        woodText.text = "Wood: " + totalWood;
    }

    public void Stone(StoneScript stone){
        stone.lifeRemaining--;
        totalStone++;
        stoneText.text = "Stone: " + totalStone;
    }

    public void ClosePopUp(){
        popUpWindow.SetActive(false);
        infoButton.SetActive(true);
        woodText.text = "Wood: " + totalWood;
        stoneText.text = "Stone: " + totalStone;
    }
    public void OpenPopUp(){
        if (selectedUnit == null){
            return;
        }

        nameText.text = selectedUnit.unitName;
        bioText.text = selectedUnit.bio;
        statsText.text = selectedUnit.stats;

        woodText.text = "";
        stoneText.text = "";
        popUpWindow.SetActive(true);
        infoButton.SetActive(false);
    }

    public void buildCastle(){
        castle.SetActive(true);
        castleBuilt = true;
        totalWood -= 5;
        totalStone -= 10;

        woodText.text = "Wood: " + totalWood;
        stoneText.text = "Stone: " + totalStone;

    }

    public void buildCabin(){
        cabin.SetActive(true);
        cabinBuilt = true;
        totalWood -= 15;

        woodText.text = "Wood: " + totalWood;

    }

}

