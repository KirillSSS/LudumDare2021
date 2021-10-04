using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies_dop : MonoBehaviour
{
    Hero hero;
    /*Hero hero;
    private void Start()
    {
        var hero = GameObject.Find("Hero").GetComponent<Hero>();
    }*/
    /*private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log(collider.tag);
        if (collider.tag == "Boiler")
        {
            GameObject.Find("Hero").GetComponent<Hero>().minusBoiler();
            Destroy(gameObject);
        }


    }*/
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug.Log(collision.tag);
        if (collision.gameObject.tag == "Boiler")
        {
            GameObject.Find("Hero").GetComponent<Hero>().minusBoiler();
            Destroy(gameObject);
        }

    }

    /*private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Minus")
        {
            if (collider.gameObject.GetComponent<AddCandy>().candy > 0)
            {
                collider.gameObject.GetComponent<AddCandy>().minus();
                gameObject.GetComponent<Enemies>().getDamaged();
                
            }
        }
    }*/

}   