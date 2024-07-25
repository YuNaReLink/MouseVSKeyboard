using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySE : MonoBehaviour
{
    [SerializeField]
    private AudioClip soundEffect;

    [SerializeField]
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        
    }

    public void GoSE()
    {
        audioSource.PlayOneShot(soundEffect);
    }
}
