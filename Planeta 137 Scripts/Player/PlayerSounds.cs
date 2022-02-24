using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    #region Variables
    public static PlayerSounds instance;
    private AudioSource source;

    [Header("Move Sounds")]
    public AudioClip walk;
    public AudioClip run;
    public AudioClip jump;

    [Header("Attacks Sounds")]
    public AudioClip punchAndkick;
    public AudioClip explosion;
    public AudioClip shoot;
    public AudioClip shield;

    [Header("Others")]
    public AudioClip damage;
    public AudioClip usePotion;
    #endregion

    void Start()
    {
        source = GetComponent<AudioSource>();

        if (instance == null && instance != this)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }

    public void PlayeClipOnShoot(AudioClip clip)
    {
        source.volume = 0.3f;
        source.PlayOneShot(clip);
    }

    public void PlayClip()
    {
        source.clip = walk;
        source.volume = Random.Range(0.9f, 1);
        source.pitch = Random.Range(0.8f, 1.1f);
        source.Play();
    }
}
