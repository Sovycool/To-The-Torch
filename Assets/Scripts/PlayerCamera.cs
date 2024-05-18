using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Camera cam;

    void FixedUpdate()
    {
        cam.transform.position = new Vector3(0f, 5f + (10.0f * MathF.Floor(transform.position.y / 10.0f)), -10f);
    }
}
