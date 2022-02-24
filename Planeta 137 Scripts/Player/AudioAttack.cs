using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioAttack : MonoBehaviour
{
    private AudioSource source;
    [SerializeField] private AudioClip airPunch;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void AirAttack()
    {
        StartCoroutine(Attack());
    }

    public IEnumerator Attack()
    {
        source.Play();
        yield return new WaitForSeconds(2);
    }
}
