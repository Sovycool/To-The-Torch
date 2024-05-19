using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class PlayerLight : MonoBehaviour
{
    public GameObject l;
    public float maxHeight;
    private float angle;

    void FixedUpdate()
    {
        angle = transform.position.y / maxHeight * 80f;
        l.transform.rotation = Quaternion.AngleAxis(-90f + angle, Vector3.right);
    }
}
