using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDrop : MonoBehaviour
{
    public AudioClip clip;
    private void OnTriggerEnter(Collider other)
    {
        AudioManager.instance.PlayAudioClip(clip);
    }
}
