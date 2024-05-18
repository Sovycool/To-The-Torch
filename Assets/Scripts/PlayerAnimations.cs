using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore;

public class PlayerAnimations : MonoBehaviour
{
    public Sprite idleSprite;
    public Sprite jumpSprite;
    public Sprite fallSprite;
    public Sprite happySprite;
    private SpriteRenderer sprite;
    private Rigidbody rb;
    private PlayerMovement player;
    private GameObject face;
    // Start is called before the first frame update
    void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody>();
        player = GetComponent<PlayerMovement>();
        face = transform.GetChild(0).gameObject;
    }

    void MoveEyes()
    {
        if (player.grounded)
            face.transform.localPosition = new Vector3(0, -0.2f * (player.jumpForce / (player.jumpStats.max - player.jumpStats.min)), -1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        sprite.sprite = idleSprite;
        if (Input.GetKey(KeyCode.Space) && player.grounded)
            sprite.sprite = jumpSprite;
        if (Vector3.Magnitude(rb.velocity) > 10 && rb.velocity.y < 0)
            sprite.sprite = fallSprite;
        MoveEyes();
    }
}
