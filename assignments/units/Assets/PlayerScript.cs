using System;
using UnityEngine;
using UnityEngine.AI;

public class PlayerScript : MonoBehaviour
{
    public Renderer rend;
    public Color normalColor;
    public Color selectedColor;
    public bool selected = false;

    public string unitName;
    public string bio;
    public string stats;

    public NavMeshAgent nma;

    // public TreeScript activeTree;
    public static Action TreeCollected;


    void OnEnable(){
        GameManager.UnitClicked += Clicked;
    }

    void OnDisable(){
        GameManager.UnitClicked -= Clicked;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.units.Add(this);
    }

    void OnDestroy(){
        GameManager.instance.units.Remove(this);
    }

    // Update is called once per frame
    void Update()
    {
        // if (activeTree != null){
        //     activeTree.lifeRemaining--;
        // }
        // Debug.Log(activeTree.lifeRemaining);
    }

    void OnMouseDown(){
        GameManager.instance.SelectUnit(this);
    }

    public void OnTriggerEnter(Collider other){
        if (other.CompareTag("tree")){
            GameManager.instance.Tree(other.gameObject.GetComponent<TreeScript>());
        }else if (other.CompareTag("stone")){
            GameManager.instance.Stone(other.gameObject.GetComponent<StoneScript>());
        }
    }

    void Clicked(PlayerScript p){
        if (p == this){
            selected = true;
            rend.material.color = selectedColor;
        }else{
            selected = false;
            rend.material.color = normalColor;
        }
    }
}
