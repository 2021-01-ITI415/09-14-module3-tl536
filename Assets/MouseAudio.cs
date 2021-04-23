using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAudio : MonoBehaviour{
    public AudioSource AudioS;
    public void PlayClip(AudioClip clip)
    {
        AudioS.PlayOneShot(clip);
    }

}
