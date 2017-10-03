using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {

    static MusicPlayer instance = null;                //singleton - initialize as null

    private void Awake()
    {
        if (instance != null)                                   //if not null, we already have one so destroy this one
        {
            Destroy(gameObject);
            Debug.Log("DESTROY INSTANCE OF MUSIC PLAYER");
        }
        else
        {
            instance = this;                                    //instance was null, so make this gameobject be instance
            GameObject.DontDestroyOnLoad(gameObject);
        }
    }
     
}
