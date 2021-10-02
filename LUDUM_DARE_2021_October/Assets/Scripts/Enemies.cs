using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float walkRange = 5f;
    [SerializeField] private float walkDistance = 2f;
    [SerializeField] private int health = 5;

    public Transform player;

    private Vector3 dir;
    private Vector2 move;
    private Rigidbody2D rb;
    private bool isStopped = true;
    private Vector3 pointUntilStop;
    private bool isAttacking = false;
    private Vector2 startPosition;
    private bool isInWrongPos = false;
    private float progress;

    [SerializeField] private SpriteRenderer sprite;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        dir = transform.position;
        startPosition = transform.position;
        
        //lives = health;
    }

    private void Update()
    {   
        if (Vector2.Distance(player.position, transform.position) <= walkRange)
        {
            dir = player.position - transform.position;

            //Debug.Log(dir);

            dir.Normalize();

            //Debug.Log("WOW = " + dir);
            move = dir;

            isAttacking = true;
        }
        else
        {
            if (Vector2.Distance(startPosition, transform.position) >= walkRange)
            {
                print("ii");
                isInWrongPos = true;
            }

            if (isAttacking)
            {
                isStopped = true;
            }

            isAttacking = false;
            
            if (isStopped && !isInWrongPos)
            {
                //Debug.Log("WHY  " + dir + " and " + transform.position);
                
                int r = Random.Range(-1, 1);
                dir.x = r * walkDistance;

                r = Random.Range(-1, 1);
                dir.y = r * walkDistance;

                move = dir;

                pointUntilStop = transform.position + dir;
                //print("ddoodd");
            }
            else if (isStopped && isInWrongPos)
            {
                dir.x = startPosition.x;
                dir.y = startPosition.y;
                
                move = dir;
                pointUntilStop = transform.position + dir;

                print("|||||||||||||||" + pointUntilStop + "|||||||||||||||" + startPosition);
                //print("pp");
            }

            if (((int)(pointUntilStop.x * 1000)) / 1000 == ((int)(transform.position.x * 1000)) / 1000 && (((int)(pointUntilStop.y * 1000)) / 1000 == ((int)(transform.position.y * 1000)) / 1000))
                isStopped = true;
            else
                isStopped = false;
        }

        //Debug.Log(isAttacking + " and " + isStopped + " and " + dir + " and " + pointUntilStop + " and " + transform.position + " and " + startPosition);
    }

    private void FixedUpdate()
    {
        Move(move);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("fuck you");

        if (!isAttacking)
        {
            walkDistance *= -1;
            isStopped = true;
        }
    }
    private void Move(Vector2 direction)
    {
        //print("-------" + pointUntilStop + " ---------- " + startPosition);

        if (Vector2.Distance(startPosition, pointUntilStop) <= walkRange)
        {
            rb.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
            //print(transform.position);
        }
        else if ((Vector2)transform.position != startPosition && isInWrongPos)
        {
            transform.position = Vector2.Lerp(transform.position, startPosition, progress * Time.deltaTime);
            progress += 1;
        }
        else
        {
            walkDistance *= -1;
            isStopped = true;
            isInWrongPos = false;
        }
        //print(isStopped+ " and "+ dir + "------------" + transform.position);

        if (dir.x > player.position.x)
            sprite.flipX = false;
        else
            sprite.flipX = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(startPosition, walkRange);
    }
}
