using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static  SoundManager instance;
    [SerializeField] private AudioSource MusicSource,EffectSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip _soundName)
    {
        EffectSource.PlayOneShot(_soundName);
    }
    
    public void PlayBGM()
    {
        MusicSource.Play();
    }
    public void MuteBGM()
    {
        MusicSource.mute=true;
    }

    public void ChangeBGM(string _BGMname)
    {
        _BGMname = "BGM/" + _BGMname;
        AudioClip BGMFile = Resources.Load<AudioClip>(_BGMname);
        MusicSource.clip = BGMFile;
        instance.PlayBGM();
    }

}
