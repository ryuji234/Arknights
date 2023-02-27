using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exusiaianimationcontrol : MonoBehaviour
{
    public Exusiai exusiai;
    public List<AudioClip> clipList;

    public AudioSource exusiaiAudio;

    private void Start()
    {
        exusiaiAudio= GetComponent<AudioSource>();
    }
    private void Hit()
    {
        if(Exusiai.skillready)
        {
            exusiaiAudio.clip = clipList[1];
        }
        else
        {
            exusiaiAudio.clip = clipList[0];
        }
        exusiaiAudio.Play();
        exusiai.attackenemy();
    }
    private void Die()
    {
        exusiaiAudio.clip = clipList[2];
        exusiaiAudio.Play();
        exusiai.Die();
    }
}
