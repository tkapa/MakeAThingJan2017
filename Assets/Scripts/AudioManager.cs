using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles listening to events and playing soundclips in correct channel
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource SFXChannel, BGMChannel;

    public float pitchVariance = .07f;

    // Listen to events
    void Awake()
    {
        // Setup instance or destroy if instance already exists in scene
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            DestroyImmediate(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Play menu music on start
    private void Start()
    {
        //BGMChannel.Stop();
        //BGMChannel.clip = menuMusic;
        //BGMChannel.Play();
    }

    // Take in an sfx clip and add pitch variation before playing
    public void PlaySFX(AudioClip _clip)
    {
        print("Playing Audio");
        SFXChannel.pitch = Random.Range(1f + pitchVariance, 1f - pitchVariance);
        SFXChannel.PlayOneShot(_clip);
    }
}
