using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [Header ("SkillObject")]
    public GameObject AirBarrier;
    [Header ("SkillSide")]
    public GameObject Up;
    public GameObject Down;
    public GameObject Left;
    public GameObject Right;
    private GameObject skillside;
    [HideInInspector] InputHandler inputHandler;
    [HideInInspector] PlayerMovement playerMovement;
    private float lastside;
    void Start()
    {
        inputHandler = FindObjectOfType<InputHandler>();
    }
    // Update is called once per frame
    void Update()
    {
        SkillSide();
        if(Input.GetKeyDown(inputHandler.ShieldSkill)){
            Skill();
        }
        if(Input.GetKeyDown(inputHandler.ShieldAttack)){
            Attack();
        }
        if(Input.GetKeyDown(inputHandler.ShieldInteract)){
            interact();
        }
        
    }

    void Skill(){
        Instantiate(AirBarrier, skillside.transform.position , quaternion.identity);
    }
    void Attack(){
        
    }
    void interact(){
        
    }
    void SkillSide(){
        if(playerMovement.isMain){
            lastside = playerMovement.GetLastSideWASD();
        }if(!playerMovement.isMain){
            lastside = playerMovement.GetLastSideAlt();
        }
        if(lastside == 1 || lastside == 5){
            skillside = Up;
        }else if(lastside == 2){
            skillside = Down;
        }else if(lastside == 3){
            skillside = Left;
        }else if(lastside == 4){
            skillside = Right;
        }
    }
}
