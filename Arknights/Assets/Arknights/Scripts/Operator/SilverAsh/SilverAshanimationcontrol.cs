using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilverAshanimationcontrol : MonoBehaviour
{
    public SilverAsh silverash;
    public List<AudioClip> clipList;
    public AudioSource silverashAudio;
    private void Start()
    {
        silverashAudio= GetComponent<AudioSource>();
    }
    private void Hit()
    {
        silverashAudio.clip = clipList[0];
        silverashAudio.Play();
        silverash.attackenemy();
    }
    private void Die()
    {
        silverash.Die();
    }
}
