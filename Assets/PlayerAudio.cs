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
    public AudioMixerSnapshot ambAuxinSnapshot;

    public LayerMask enemyMask;

    bool enemyNear;
    public void Update()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, 5f, transform.forward, 0f, enemyMask);
        if(hits.Length > 0)
        {
            if (!enemyNear)
            {

                auxinSnapshot.TransitionTo(0.5f);
                enemyNear = true;
            }
        }
        else
        {
            if (enemyNear)
            {
                idleSnapshot.TransitionTo(0.5f);
                enemyNear = false;
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
    }
}
