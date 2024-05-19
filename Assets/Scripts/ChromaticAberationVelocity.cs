using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class ChromaticAberationVelocity : MonoBehaviour
{
    public GameObject player;
    private PostProcessVolume profile;
    // Start is called before the first frame update
    void Awake()
    {
        profile = GetComponent<PostProcessVolume>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (profile.profile.TryGetSettings(out ChromaticAberration chromatic))
            if (!player.GetComponent<PlayerMovement>().finished)
                chromatic.intensity.value = Mathf.Lerp(chromatic.intensity.value, 0.05f * Vector3.Magnitude(player.GetComponent<Rigidbody>().velocity), 0.1f);
            else if (profile.profile.TryGetSettings(out Bloom bloom)) {
                chromatic.intensity.value = Mathf.Lerp(chromatic.intensity.value, 2f * Vector3.Magnitude(player.GetComponent<Rigidbody>().velocity), 0.1f);
                bloom.intensity.value = Mathf.Lerp(bloom.intensity.value, 10f + Vector3.Magnitude(player.GetComponent<Rigidbody>().velocity), 0.1f);
            }
        if (player.transform.position.y > 300)
            profile.enabled = false;
    }
}
