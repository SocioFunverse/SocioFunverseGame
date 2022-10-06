using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager insta;


    [SerializeField] AudioSource bgSource;
    [SerializeField] AudioSource soundSource;

    [SerializeField] AudioClip[] audioClips;

    private void Awake()
    {
        if (insta == null)
        {
            insta = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
            return;
        }
    }

    public void playSound(int _no, float _vol = 1f) {
        //if (soundSource.isPlaying) soundSource.Stop();
        soundSource.PlayOneShot(audioClips[_no], _vol);
    }
   
}
