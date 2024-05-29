using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header ("Movement Variable")]
    public float Movespeed = 5f;
    [HideInInspector] public Rigidbody2D rb;
    Vector2 movement;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        //Movement here
        rb.MovePosition(rb.position + movement * Movespeed * Time.fixedDeltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        //Input here
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if(movement != Vector2.zero){
            movement.Normalize();
        }
    }
    
}
