using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneSE : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip battleStart;

    [SerializeField]
    private AudioClip damage;

    [SerializeField]
    private AudioClip go;

    [SerializeField]
    private AudioClip push;

    [SerializeField]
    private AudioClip winGame;

    [SerializeField]
    private AudioClip winRound;



    public void BattleStart()
    {
        audioSource.PlayOneShot(battleStart);
    }
    public void Damage()
    {
        audioSource.PlayOneShot(damage);
    }
    public void Go()
    {
        audioSource.PlayOneShot(go);
    }
    public void Push()
    {
        audioSource.PlayOneShot(push);
    }
    public void WinGame()
    {
        audioSource.PlayOneShot(winGame);
    }
    public void WinRound()
    {
        audioSource.PlayOneShot(winRound);
    }
}
