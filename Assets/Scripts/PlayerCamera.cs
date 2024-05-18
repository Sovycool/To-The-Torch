using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Camera cam;

    void FixedUpdate()
    {
        cam.transform.position = new Vector3(0f, transform.position.y + 1f, -10f);
    }
}
