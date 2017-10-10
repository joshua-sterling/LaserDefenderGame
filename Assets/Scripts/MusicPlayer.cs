using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

    static MusicPlayer instance = null;                //singleton - initialize as null

    public AudioClip startClip;
    public AudioClip gameClip;
    public AudioClip endClip;

    private AudioSource music;

    private void Awake()
    {
        if (instance != null && instance != this)                                   //if not null, we already have one so destroy this one
        {
            Destroy(gameObject);
            Debug.Log("DESTROY INSTANCE OF MUSIC PLAYER");
        }
        else
        {
            instance = this;                                    //instance was null, so make this gameobject be instance
            GameObject.DontDestroyOnLoad(gameObject);
            music = GetComponent<AudioSource>();
            music.clip = startClip;
            music.loop = true;
            music.Play();
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        Debug.Log("Music player: loaded level" +level);
        music.Stop();

        if (level == 0)
        {
            music.clip = startClip;           
        }
        if (level == 2)
        {
            music.clip = gameClip;            
        }
        if (level == 1)
        {
            music.clip = endClip;           
        }

        music.loop = true;
        music.Play();

    }

}
