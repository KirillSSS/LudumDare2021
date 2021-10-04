using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Script : MonoBehaviour
{
    private float del = 5;
    private void Awake()
    {
        /*while (true)
        {
            Debug.Log(del);
            del *= -1;
            var pos = gameObject.GetComponent<RectTransform>().anchoredPosition3D;
            gameObject.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(pos.x, pos.y + del, pos.z);
            StartCoroutine(wait());
        }*/
    }

    private IEnumerator wait()
    {
        yield return new WaitForSeconds(2f);
    }
}
