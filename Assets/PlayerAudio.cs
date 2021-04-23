using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerAudio : MonoBehaviour{
    public AudioClip splashSound;

    public AudioSource AudioS;

    public AudioMixerSnapshot idleSnapshot;
    public AudioMixerSnapshot auxinSnapshot;
    public AudioMixerSnapshot ambIdleSnapshot;
    public AudioMixerSnapshot ambInSnapshot;

    public LayerMask enemyMask;

    bool enemyNear;
    public void Update()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, 5f, transform.forward, 0f, enemyMask);


        if (hits.Length > 0)
        {
            enemyNear = true;

        }
        else
        {
           
            enemyNear = false;

        }

        if (!AudioManager.manager.eventRunning)
        {
            if (enemyNear)
            {
                if (!AudioManager.manager.auxIn)
                {
                    auxinSnapshot.TransitionTo(0.5f);
                    AudioManager.manager.currentAudioMixerSnapshot = auxinSnapshot;
                    AudioManager.manager.auxIn = true;
                }
                else
                {
                    if (AudioManager.manager.currentAudioMixerSnapshot == AudioManager.manager.eventSnap)
                    {
                        auxinSnapshot.TransitionTo(0.5f);
                        AudioManager.manager.currentAudioMixerSnapshot = auxinSnapshot;
                        AudioManager.manager.auxIn = true;
                    }
                }

            }
            else
            {
                if (!AudioManager.manager.auxIn)
                {
                    idleSnapshot.TransitionTo(0.5f);
                    AudioManager.manager.currentAudioMixerSnapshot = idleSnapshot;
                    AudioManager.manager.auxIn = true;
                }
                else
                {
                    if (AudioManager.manager.currentAudioMixerSnapshot == AudioManager.manager.eventSnap)
                    {
                        idleSnapshot.TransitionTo(0.5f);
                        AudioManager.manager.currentAudioMixerSnapshot = idleSnapshot;
                        AudioManager.manager.auxIn = true;
                    }
                }


            }
        }

           

    }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            AudioS.PlayOneShot(splashSound);
        }
        if (other.CompareTag("EnemyZone"))
        {
            auxinSnapshot.TransitionTo(0.5f);
        }
        if (other.CompareTag("Ambiance"))
        {
            ambInSnapshot.TransitionTo(0.5f);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            AudioS.PlayOneShot(splashSound);
        }
        if (other.CompareTag("EnemyZone"))
        {
            idleSnapshot.TransitionTo(0.5f);
        }
        if (other.CompareTag("Ambiance"))
        {
            ambIdleSnapshot.TransitionTo(0.5f);
        }
    }
}
