using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timeWave : MonoBehaviour
{
    [SerializeField] private GameObject timer, wave;

    private float time, sec, minutes, milisec;
    private string s, ms, m;
    private int k;
    public void startTimer(float sec1)
    {
        timer.SetActive(true);
        wave.SetActive(false);
        time = sec1;
        LaunchWave();
    }
    void Start()
    {
        timer.SetActive(false);
        wave.SetActive(false);

    }

    private IEnumerator timeWait()
    {
        if (time > 0)
        {
            yield return new WaitForSeconds(0);
            //S ms = (time % 1).ToString().Substring(2, 4);
            minutes = time / 60;
            m = minutes.ToString().Substring(0, 1);
            sec = (time % 60) / 1;
            s = sec.ToString();
            k = s.IndexOf(',');
            Debug.Log(k + " k " + s);
            if (k>1)s = s.Substring(0, k); 
            if(k==1) s = "0" + s.Substring(0, k);
            milisec = time % 1;
            //ms = milisec.ToString().Substring(0, 2); ;
            ms = milisec.ToString();
            k = ms.IndexOf(',');
            if (k >= 0) ms = ms.Substring(k + 1, 2); else ms = "00";
            //ms = ms.Substring(s.IndexOf(",") + 1, s.IndexOf(",") + 2);
            timer.GetComponent<Text>().text = "Time to next wave: 0" + m + ":" + s + ":" + ms;
            time-= 0.01f;
            
        
            LaunchWave();
        } else
        {
            timer.SetActive(false);
            wave.SetActive(true);
        }
    }

    public void LaunchWave() => StartCoroutine(timeWait());
}
