using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCandy : MonoBehaviour
{
    //[SerializeField] private GameObject torch;
    public float candy;
    public void add()
    {
        //torch.GetComponent<Torch>().add();
        gameObject.GetComponent<Torch>().add();
        Debug.Log("Add");
    }
    public void minus()
    {
        //torch.GetComponent<Torch>().minus();
        gameObject.GetComponent<Torch>().minus();
        Debug.Log("Minus");
    }

    public void Update()
    {
        //candy = torch.GetComponent<Torch>().candy;
        candy = gameObject.GetComponent<Torch>().candy;
        Debug.Log(candy);
    }
}
