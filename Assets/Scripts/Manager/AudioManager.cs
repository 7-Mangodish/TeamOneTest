using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance { get { return instance; } }

    [SerializeField] private Sound[] listSound;

    private void Awake() {
        if(instance == null) {
            instance = this;
        }
        else {
            Destroy(this.gameObject);
        }

        foreach(Sound s in listSound) {
            s.audioSource = this.AddComponent<AudioSource>();

            s.audioSource.name = s.name;
            s.audioSource.clip = s.audioClip;
            s.audioSource.volume = s.volume;
            s.audioSource.pitch = s.pitch;
            s.audioSource.playOnAwake = s.playOnAwake;
        }
    }


    public void Play(string soundName) {
        AudioSource res = null;
        foreach(Sound s in listSound) {
            if(s.name == soundName) {
                res = s.audioSource; break;
            }
        }

        if(res != null) {
            res.Play();
        }
        else {
            Debug.Log("SoundName can find: " + soundName);
        }
    }
}
