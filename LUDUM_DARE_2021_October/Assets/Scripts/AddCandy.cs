using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCandy : MonoBehaviour
{
    //[SerializeField] private GameObject torch;
    public float candy;
    public GameObject torch;
    public void add()
    {
        //torch.GetComponent<Torch>().add();
        torch.GetComponent<Torch>().add();
        Debug.Log("Add");
    }
    public void minus()
    {
        //torch.GetComponent<Torch>().minus();
        torch.GetComponent<Torch>().minus();
        Debug.Log("Minus");
    }

    public void Update()
    {
        //candy = torch.GetComponent<Torch>().candy;
        candy = torch.GetComponent<Torch>().candy;
        //Debug.Log(candy);
    }
}
