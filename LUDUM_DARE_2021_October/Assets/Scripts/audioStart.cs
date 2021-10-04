using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class audioStart : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioMixer audioMixer;
    void Awake()
    {
        float f;
        audioMixer.GetFloat("volume", out f);
        Debug.Log(f);
        Debug.Log(-1f / (f / 2.9957f));
        if (f == 0f)
        {
            gameObject.GetComponent<Slider>().value = 1;
        }
        else
        {
            gameObject.GetComponent<Slider>().value = -1f / (f / 3f);
        }
    }

    
}
