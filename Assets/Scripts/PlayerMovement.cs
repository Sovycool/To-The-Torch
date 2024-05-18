using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [System.Serializable]
    public struct JumpStats
    {
        public JumpStats(float mn, float mx, float ttm)
        {
            min = mn;
            max = mx;
            timeToMax = ttm;
        }

        public float min;
        public float max;
        public float timeToMax;
    }

    public JumpStats jumpStats = new(0.0f, 10.0f, 50.0f);
    [HideInInspector]
    public float jumpForce;
    [HideInInspector]
    public bool grounded = false;
    private Rigidbody rb;
    private int dir = 0;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        jumpForce = jumpStats.min;
    }

    private void IsGrounded()
    {
        grounded = rb.velocity.y == 0;
    }

    private void SetDir()
    {
        dir = 0;
        if (Input.GetKey(KeyCode.RightArrow))
            dir = 1;
        if (Input.GetKey(KeyCode.LeftArrow))
            dir = Math.Max(-1, dir - 1);
    }

    private void BounceOnWalls()
    {
        if (Physics.Raycast(transform.position + new Vector3(0, 0.5f), Vector3.right, 0.51f) ||
            Physics.Raycast(transform.position - new Vector3(0, 0.5f), Vector3.right, 0.51f) ||
            Physics.Raycast(transform.position, Vector3.right, 0.51f) ||
            Physics.Raycast(transform.position + new Vector3(0, 0.5f), Vector3.left, 0.51f) ||
            Physics.Raycast(transform.position - new Vector3(0, 0.5f), Vector3.left, 0.51f) ||
            Physics.Raycast(transform.position, Vector3.left, 0.51f)) {
            rb.velocity = new Vector3(rb.velocity.x * -1, rb.velocity.y);
        }
    }

    private void BounceOnCeiling()
    {
        if ((Physics.Raycast(transform.position + new Vector3(0.5f, 0), Vector3.up, 0.51f) ||
            Physics.Raycast(transform.position - new Vector3(0.5f, 0), Vector3.up, 0.51f) ||
            Physics.Raycast(transform.position, Vector3.up, 0.51f)) && rb.velocity.y > 0) {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y * -1);
        }
    }

    void FixedUpdate()
    {
        BounceOnWalls();
        BounceOnCeiling();
        IsGrounded();
        SetDir();
        if (Input.GetKey(KeyCode.Space) && grounded)
            jumpForce = Math.Min(jumpForce + (jumpStats.max / jumpStats.timeToMax), jumpStats.max);
        else if (jumpForce > jumpStats.min) {
            rb.velocity = new Vector3(0.5f * jumpForce * dir, 1.2f * jumpForce);
            if (jumpForce + 1f > jumpStats.min)
                GetComponent<AudioSource>().Play();
            jumpForce = jumpStats.min;
        }
    }
}
