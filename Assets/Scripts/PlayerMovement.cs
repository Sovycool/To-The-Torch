using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [System.Serializable]
    public struct JumpStats
    {
        public JumpStats(float mn, float mx, float ttm, float rs)
        {
            min = mn;
            max = mx;
            timeToMax = ttm;
            rotationSpeed = rs;
        }

        public float min;
        public float max;
        public float timeToMax;
        public float rotationSpeed;
    }

    public JumpStats jumpStats = new(0.0f, 10.0f, 50.0f, 1.0f);
    public GameObject arrow;
    public GameObject end;
    [HideInInspector]
    public float jumpForce;
    [HideInInspector]
    public bool grounded = false;
    [HideInInspector]
    public bool finished = false;
    [SerializeField]
    private AudioClip endClip;
    private Rigidbody rb;
    private float angle = 45f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        jumpForce = jumpStats.min;
    }

    private void IsGrounded()
    {
        grounded = rb.velocity.y == 0;
    }

    private void FadeOutArrow()
    {
            arrow.GetComponentInChildren<SpriteRenderer>().color = new Color(255f, 255f, 255f, Mathf.Lerp(arrow.GetComponentInChildren<SpriteRenderer>().color.a, 0f, 0.1f));
    }

    private void MoveAngle()
    {
        if (finished) {
            angle = 45f;
            FadeOutArrow();
            return;
        }
        if (Input.GetKey(KeyCode.RightArrow))
            angle = Math.Min(angle + jumpStats.rotationSpeed, 90);
        if (Input.GetKey(KeyCode.LeftArrow))
            angle = Math.Max(angle - jumpStats.rotationSpeed, 0);
        arrow.transform.rotation = Quaternion.AngleAxis(45f - angle, Vector3.forward);
    }

    private void StickOnGround()
    {
        if (Physics.Raycast(transform.position + new Vector3(0.49f, 0f), Vector3.down, 0.52f, LayerMask.GetMask("Default")) ||
            Physics.Raycast(transform.position - new Vector3(0.49f, 0f), Vector3.down, 0.52f, LayerMask.GetMask("Default")) ||
            Physics.Raycast(transform.position, Vector3.down, 0.52f, LayerMask.GetMask("Default"))) {
            rb.velocity = new Vector3(0f, 0f);
            }
    }

    void FixedUpdate()
    {
        StickOnGround();
        IsGrounded();
        MoveAngle();
        if (Input.GetKey(KeyCode.Space) && grounded)
            jumpForce = Math.Min(jumpForce + (jumpStats.max / jumpStats.timeToMax), jumpStats.max);
        else if (jumpForce > jumpStats.min && grounded) {
            rb.velocity = Quaternion.AngleAxis(45f - angle, Vector3.forward) * new Vector3(0f, jumpForce);
            if (jumpForce + 1f > jumpStats.min)
                GetComponent<AudioSource>().Play();
            jumpForce = jumpStats.min;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("End") && grounded) {
            finished = true;
            GetComponent<PlayerLight>().enabled = false;
            GetComponent<AudioSource>().clip = endClip;
            jumpStats.max = 100f;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("End")) {
            StartCoroutine(DelayAction(5f));
        }
    }

    IEnumerator DelayAction(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        end.GetComponent<EndingAnimation>().LightTorch();
    }
}
