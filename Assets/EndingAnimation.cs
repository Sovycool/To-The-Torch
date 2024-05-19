using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class EndingAnimation : MonoBehaviour
{
    public GameObject particles;

    public void LightTorch()
    {
        particles.SetActive(true);
    }
}
