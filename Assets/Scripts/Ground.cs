using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ground : MonoBehaviour
{
    public int foxCountOnStart;
    public int chickCountOnStart;
    public int foxCountOnEnd;
    public int chickCountOnEnd;

    public AudioClip audioClipFail;
    public AudioClip audioClipWin;

    private AudioSource audioSource;

    public abstract void OnCollisionEnter(Collision collision);
    public abstract void OnCollisionExit(Collision collision);

    public abstract void Fail();

    protected void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    protected void PlayFailAudio()
    {
        audioSource.PlayOneShot(audioClipFail, 1.0f);
    }

    protected void PlayWinAudio()
    {
        audioSource.PlayOneShot(audioClipWin, 1.0f);
    }
}
