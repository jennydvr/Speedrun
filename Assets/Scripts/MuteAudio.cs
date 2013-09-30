using UnityEngine;
using System.Collections;

public class MuteAudio : MonoBehaviour
{
    public AudioSource audio;
    public bool soun = true;
    // Use this for initialization
    void Start()
    {
        audio = gameObject.GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
	
    }

    public void MuteDesmute(){
    
        if (soun)
        {
            audio.volume = 0.0f;
        }
        else
        {
            audio.volume = 1.0f;
        }
        soun = !soun;
    }
}

