using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    [SerializeField] private float lightIntensity;
    [SerializeField] private float lightPercent;
    public float candy;
    [SerializeField] private float candyMax;

    private Animator anim;

    private Light torch;

    PortalEnemies pe = new PortalEnemies();
    Enemies e = new Enemies();

    private state State
    {
        get { return (state)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
    }

    public void add()
    {
        candy++;
    }

    public void minus()
    {
        candy -= 0.001f;
        if (candy < 0) candy = 0;
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

        private void Start()
    {
        torch = GetComponentInChildren<Light>();
        torch.intensity = lightIntensity;

    }

    private void Update()
    {
        if (candy >= 2)
        {
            torch.intensity = lightIntensity;
            State = state.TorchLight;
        } else 
        {
            if (candy > 0) 
            {
                torch.intensity = lightIntensity * 0.75f * candy;
                State = state.TorchLight;
            } else
            {
                State = state.TorchIdle;
            }
        }
        //Debug.Log(candy);
    }
    /*private void OnTriggerStay2D(Collider2D collision)
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
    }*/
}

public enum state
{
    TorchIdle,
    TorchLight
}
