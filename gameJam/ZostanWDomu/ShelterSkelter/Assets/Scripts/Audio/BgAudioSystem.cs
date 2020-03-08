using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgAudioSystem : MonoBehaviour
{
    private static BgAudioSystem instance;
    private AudioClip current;
    private AudioSource source;
    public float rate;
    public float max;
    // Start is called before the first frame update
    void Awake()
    {
        source = GetComponent<AudioSource>();
        instance = this;
    }
    private void FadeOut()
    {
        source.volume -= 0.01f;
        if (source.volume < 0.02)
        {
            CancelInvoke("FadeOut");
            source.Pause();
            source.clip = current;
            source.Play();
            InvokeRepeating("FadeIn", 0, rate);
        }
    }
    private void FadeIn()
    {
        source.volume += 0.01f;
        if (source.volume > max)
        {
            CancelInvoke("FadeIn");
        }
    }
    public void ChangeBg(AudioClip clip)
    {
        if(source.isPlaying)
        {
            CancelInvoke("FadeIn");
            CancelInvoke("FadeOut");
            current = clip;
            InvokeRepeating("FadeOut", 0, rate);
        }
        else
        {
            CancelInvoke("FadeIn");
            CancelInvoke("FadeOut");
            current = clip;
            source.clip = current;
            source.volume = 0;
            source.Play();
            InvokeRepeating("FadeIn", 0, rate);
        }
    }
    public static BgAudioSystem Instance()
    {
        if(instance == null)
        {
            Debug.Log("Missing bgAudioSystem");
        }
        return instance;
    }
}
