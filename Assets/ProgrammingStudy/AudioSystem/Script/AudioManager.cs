using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioSystem; // namespace 사용 예

namespace AudioSystem
{
    public class AudioManager : MonoBehaviour
    {
        public AudioSource audioSourceA;
        public AudioSource audioSourceB;
        public AudioSource audioSourceC;
        [SerializeField] List<AudioSource> audioSources; // attribute
        public AudioClip audioClipA;
        public AudioClip audioClipB;
        public AudioClip audioClipC;

        // Start is called before the first frame update
        void Start()
        {
            audioSourceA.volume = 1.0f;
            audioSourceA.clip = audioClipA;
            audioSourceA.Play();

            AudioSystem.AudioManager audioManager; // namespace 사용 예
        }
    }
}

