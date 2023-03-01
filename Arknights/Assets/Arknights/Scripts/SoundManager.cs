using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip click;
    public AudioClip battleStart;
    public AudioSource audioSource;
    public static SoundManager instance;
    private void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public static void Click()
    {
        instance.audioSource.PlayOneShot(instance.click);
    }

    public static void BattleStart()
    {
        instance.audioSource.PlayOneShot(instance.battleStart);
    }
}
