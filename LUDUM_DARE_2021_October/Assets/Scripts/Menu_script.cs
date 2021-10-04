using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Menu_script : MonoBehaviour
{

    public AudioMixer audioMixer;

    public void SetVolume(float volume)
    {
        float f;
        audioMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
        audioMixer.GetFloat("volume", out f);
        Debug.Log(volume);
        Debug.Log(Mathf.Log10(volume) * 20);
    }

    public void SetQuality(int qualityIndex)
    {
        Time.timeScale = 1f;
        QualitySettings.SetQualityLevel(qualityIndex);
    }

}
