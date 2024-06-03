using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [HideInInspector] InputHandler inputHandler;
    void Start()
    {
        inputHandler = FindObjectOfType<InputHandler>();
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(inputHandler.SwordSkill)){
            Skill();
        }
        if(Input.GetKeyDown(inputHandler.SwordAttack)){
            Attack();
        }
        if(Input.GetKeyDown(inputHandler.SwordInteract)){
            interact();
        }
    }

    void Skill(){
    }
    void Attack(){
        
    }
    void interact(){
        
    }
}
