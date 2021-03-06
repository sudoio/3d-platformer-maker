﻿using UnityEngine;

public class Player : MonoBehaviour {

    public float speed = 5f;
    public float jumpSpeed = 10f;

    public LayerMask ground;

    private float distToGround;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        distToGround = GetComponent<Collider>().bounds.extents.y;
    }

	void Update()
    {
        if (State.isPlaying)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            float y = 0f;
            if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
            {
                y = jumpSpeed;
            }

            rb.velocity = new Vector3(x * speed, rb.velocity.y + y, z * speed);
        }
    }

    private bool IsGrounded(){
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f, ground);
    } 

}
