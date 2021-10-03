using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    [SerializeField] private float lightIntensity;
    [SerializeField] private float lightPercent;

    private Light torch;

    PortalEnemies pe = new PortalEnemies();
    Enemies e = new Enemies();

    private void Start()
    {
        torch = GetComponentInChildren<Light>();
        torch.intensity = lightIntensity;

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        //Place for Canvas with "E" button
        if (collision.tag == "Enemy")
        {
            if(torch.intensity != 0)
                //pe.GetDamage;
            torch.intensity -= torch.intensity * lightPercent;
        }
        else if (collision.tag == "Enemy1")
        {
            //e.GetDamage;
            torch.intensity -= torch.intensity * lightPercent;
        }
    }
}
