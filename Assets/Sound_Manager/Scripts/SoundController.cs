using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{

    public AudioClip[] audioPlayer;

    public AudioClip[] audioEnemy;

    public AudioSource audioSource;

    public void Shot()
    {
        audioSource.PlayOneShot(audioPlayer[0]);
    }

    public void EnemyDeath()
    {
        audioSource.PlayOneShot(audioEnemy[0]);
    }
}
