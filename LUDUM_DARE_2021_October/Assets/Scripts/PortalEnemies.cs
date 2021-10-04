using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalEnemies : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float health = 15, healthMax, x1;

    private Vector3 center;
    public Transform image;

    //private bool isInWrongPos = false;
    //private bool isAttacking = false;
    //private bool isStopped = true;

    //private Vector3 pointUntilStop;
    private Vector3 dir;
    private Vector2 move;
    private Vector2 startPosition;

    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] private SpriteRenderer sprite;


    private PortalSpawner waveSpawner;
    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        dir = center;
        startPosition = transform.position;
        
        //lives = health;
    }

    private void Awake()
    {
        healthMax = health;
        x1 = image.localScale.x;
        waveSpawner = GameObject.Find("Boiler").GetComponent<PortalSpawner>();
        center = GameObject.Find("Boiler").GetComponent<PortalSpawner>().transform.position;
    }

    private States State
    {
        get { return (States)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
    }

    private void Update()
    {
        dir = center - transform.position;

        //Debug.Log(dir);

        dir.Normalize();

        //Debug.Log("WOW = " + dir);
        move = dir;
    }

    private void FixedUpdate()
    {
        Move(move);
    }

    private void Move(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));

        if (dir.x < 0)
            sprite.flipX = false;
        else
            sprite.flipX = true;
    }

    private void OnDestroy()
    {
        int enemiesLeft = 0;
        enemiesLeft = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (enemiesLeft == 0)
            waveSpawner.LaunchWave();
    }

    public enum States
    {
        run
    }

    public void getDamaged()
    {
        health -= 0.03f;
        if (health <= 0)
        {
            Destroy(gameObject);
        }

        image.localScale = new Vector3(x1 * (health / healthMax), image.localScale.y, 1f);
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == "Minus")
        {
            Debug.Log("Minus");
            if (collider.gameObject.GetComponent<AddCandy>().candy > 0)
            {
                collider.gameObject.GetComponent<AddCandy>().minus();
                getDamaged();

            }
        }
    }
}
