using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Camera cam;
    private PlayerMovement state;

    void Awake()
    {
        state = GetComponent<PlayerMovement>();
    }

    void FixedUpdate()
    {
        if (!state.finished)
            cam.transform.position = new Vector3(0f, 5f + (10.0f * MathF.Floor(transform.position.y / 10.0f)), cam.transform.position.z);
        else
            if (transform.position.y > 300f)
                cam.transform.position = new Vector3(0f, cam.transform.position.y, -100f);
    }
}
