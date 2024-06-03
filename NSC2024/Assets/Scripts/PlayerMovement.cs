using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header ("Input Type")]
    public bool isMain = true;

    [Header ("Movement Variable")]
    public float Movespeed = 5f;
    [HideInInspector] public Rigidbody2D rb;
    Vector2 movement;
    private int LastSideWASD;
    private int LastSideAlt;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        //Movement here
        rb.MovePosition(rb.position + movement * Movespeed * Time.fixedDeltaTime);
    }

    void Update()
    {
        if(isMain == true)
        {
            GetInputWASD();
        }
        else
        {
            GetInputAlt();
        }
    }

    // Update is called once per frame
    void GetInputWASD()
    {
        movement = Vector2.zero; // Reset movement

        //Input here
        if(Input.GetKey(KeyCode.W))
        {
            movement.y = 1f;
            LastSideWASD = 1;
        }
        if(Input.GetKey(KeyCode.S))
        {
            movement.y = -1f;
            LastSideWASD = 2;
        }
        if(Input.GetKey(KeyCode.A))
        {
            movement.x = -1f;
            LastSideWASD = 3;
        }
        if(Input.GetKey(KeyCode.D))
        {
            movement.x = 1f;
            LastSideWASD = 4;
        }
        if(movement != Vector2.zero)
        {
            movement.Normalize();
        }
    }

    void GetInputAlt()
    {
        movement = Vector2.zero; // Reset movement

        //Input here
        if(Input.GetKey(KeyCode.UpArrow))
        {
            movement.y = 1f;
            LastSideAlt = 1;
        }
        if(Input.GetKey(KeyCode.DownArrow))
        {
            movement.y = -1f;
            LastSideAlt = 2;
        }
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            movement.x = -1f;
            LastSideAlt = 3;
        }
        if(Input.GetKey(KeyCode.RightArrow))
        {
            movement.x = 1f;
            LastSideAlt = 4;
        }
        if(movement != Vector2.zero)
        {
            movement.Normalize();
        }
    }

    public int GetLastSideWASD(){
        return LastSideWASD;
    }
    public int GetLastSideAlt(){
        return LastSideAlt;
    }
}