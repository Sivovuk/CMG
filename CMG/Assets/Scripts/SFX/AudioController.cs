using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [field:SerializeField] public AudioSource BackgroundMusic { get; private set; }
    [field:SerializeField] public AudioSource Flip  { get; private set; }
    [field:SerializeField] public AudioSource GameEnd  { get; private set; }
    [field:SerializeField] public AudioSource Match  { get; private set; }
    [field:SerializeField] public AudioSource Mismatch  { get; private set; }

    public static AudioController Instance { get; private set; }

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
            Destroy(gameObject);
    }

    public void PlayAudio(AudioSource audio) 
    {
        if (audio != null) 
            audio.PlayOneShot(audio.clip);
    }

    public void StopAudio(AudioSource audio)
    {
        if (audio != null)
            audio.Stop();
    }
}
