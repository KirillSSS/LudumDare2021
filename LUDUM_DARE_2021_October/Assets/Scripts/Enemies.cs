using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float walkRange = 5f;
    [SerializeField] private float walkDistance = 2f;
    public float health = 5, healthMax;
    private bool inDam;

    private GameObject player;

    /* public GameObject HPbarPrefab;
     public Transform CanvasTransform;
     public Transform HPbarAbchor;
     private EnemiesHpBar _hpBar;*/
    public Transform image;

    private bool isInWrongPos = false;
    private bool isAttacking = false;
    private bool isStopped = true;

    private Vector3 pointUntilStop;
    private Vector3 dir;
    private Vector2 move;
    private Vector2 startPosition;

    private Rigidbody2D rb;
    private float progress;
    private Animator anim;
    private float x1;

    [SerializeField] private GameObject character;

    [SerializeField] private SpriteRenderer sprite;

    public void getDamaged()
    {
        health -= 0.01f;
        if (health <= 0f)
        {
            //Destroy(_hpBar);
            Destroy(gameObject);
        }
        //Debug.Log(image.localScale.x * (health / healthMax));
        image.localScale = new Vector3(x1 * (health / healthMax), image.localScale.y, 1f);
        //image.localScale.Set(image.localScale.x * (health / healthMax), 1f, 1f);
        //_hpBar.UpdateBar();
        
    }

    private void Awake()
    {
        healthMax = health;
        x1 = image.localScale.x;
        /*CanvasTransform = GameObject.Find("Canvas").GetComponent<Transform>();
        healthMax = health;
        var bar = Instantiate(HPbarPrefab, CanvasTransform);
        _hpBar = bar.GetComponent<EnemiesHpBar>();
        _hpBar.Initialize(HPbarAbchor, this);*/
    }

    private void Start()
    {
        
        rb = this.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        player = GameObject.FindGameObjectsWithTag("Hero")[0];

        dir = transform.position;
        startPosition = transform.position;
        
        //lives = health;
    }
    private States State
    {
        get { return (States)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
    }

    private void Update()
    {
        
        if (Vector2.Distance(player.transform.position, transform.position) <= walkRange)
        {
            dir = player.transform.position - transform.position;

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

                StartCoroutine(Wait());

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

                //print("|||||||||||||||" + pointUntilStop + "|||||||||||||||" + startPosition);
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

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == "Minus")
        {
            if (collider.gameObject.GetComponent<AddCandy>().candy > 0)
            {
                collider.gameObject.GetComponent<AddCandy>().minus();
                getDamaged();

            }
        }
    }

    private void OnDestroy()
    {
        print("AAAAAAAAAAAAAAAAAAAAAAAAA");
        StartCoroutine(Died());
    }

    private void Move(Vector2 direction)
    {
        //print("-------" + pointUntilStop + " ---------- " + startPosition);

        if (Vector2.Distance(startPosition, pointUntilStop) <= walkRange)
        {
            rb.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
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

        if (dir.x < 0)
            sprite.flipX = false;
        else
            sprite.flipX = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(startPosition, walkRange);
    }


    private IEnumerator Died()
    {
        print("BBBBBBBBBBBBBBBBBBBBBBBBBBBB");
        yield return new WaitForSeconds(5f);
        Instantiate(character, startPosition, Quaternion.identity);
    }
    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(50f);
    }

    public enum States
    {
        run
    }
}
