using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private int lives = 5;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;

    public static Hero Instance { get; set; }

    private States State
    {
        get { return (States)anim.GetInteger("State"); }
        set { anim.SetInteger("State", (int)value); }
    }

    private void runHorizontal()
    {
        Debug.Log("LOL");

        Vector3 dir = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);

        sprite.flipX = dir.x < 0.0f;

        State = States.runHor;

        //if (Input.GetButton("Vertical")) runVertical(2);

        

    }

    private void runVertical()
    {
        Vector3 dir = transform.up * Input.GetAxis("Vertical"); 
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);

        if (dir.y > 0.0f) State = States.runUp;
        if (dir.y < 0.0f) State = States.runDown;

    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {

        State = States.idle;
        if (Input.GetButton("Vertical")) runVertical();
        if (Input.GetButton("Horizontal")) runHorizontal();

    }

    private void FixedUpdate()
    {
        if (Input.GetButton("Horizontal")) State = States.runHor;
    }

}

public enum States
{
    idle,
    runHor,
    runUp,
    runDown
}
