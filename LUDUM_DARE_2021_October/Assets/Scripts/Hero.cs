using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hero : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private int lives = 5, boilCand = 0, boilCandMax = 100;

    public int candy = 0, candyMax;

    [SerializeField] private GameObject Pause_menu, Score, ScoreBoiler, Boiler;
    //GameObject Score = GameObject.Find("Score_text");
    private Paues_end end;
    //private Score_te score;



    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;

    public static Hero Instance { get; set; }

    private States State
    {
        get { return (States)anim.GetInteger("State"); }
        set { anim.SetInteger("State", (int)value); }
    }

    public void addCandy()
    {
        candy++;
        //string s = candy.ToString() + "/" + candyMax.ToString();
        //score.addScore(s);
        Score.GetComponent<Text>().text = candy.ToString() + "/" + candyMax.ToString();
    }

    private void runHorizontal()
    {


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

    public void minusBoiler()
    {
        if (boilCand > 0)
        {
            boilCand--;
            ScoreBoiler.GetComponent<Text>().text = boilCand.ToString() + "/" + boilCandMax.ToString();
        }
        else
        {
            end.endOfGameBoi();
        }
    }

    private void giveBoiler()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (candy > 0)
            {
                candy--;
                Score.GetComponent<Text>().text = candy.ToString() + "/" + candyMax.ToString();
                boilCand++;
                ScoreBoiler.GetComponent<Text>().text = boilCand.ToString() + "/" + boilCandMax.ToString();
            }
        }
    }

   

    private void Awake()
    {
        Debug.Log("lol");
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        end = Pause_menu.GetComponent<Paues_end>();
        //score = Score.GetComponent<Score_te>();
        //string s = candy.ToString() + "/" + candyMax.ToString();
        //score.addScore(s);
        Score.GetComponent<Text>().text = candy.ToString() + "/" + candyMax.ToString();
        ScoreBoiler.GetComponent<Text>().text = boilCand.ToString() + "/" + boilCandMax.ToString();

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Enemy1")
        {
            if (candy > 0)
            {
                Destroy(collision.gameObject);
                candy--;
                //string s = candy.ToString() + "/" + candyMax.ToString();
                //score.addScore(s);
                Score.GetComponent<Text>().text = candy.ToString() + "/" + candyMax.ToString();
            }
            else
            {
                end.endOfGamePer();
            }

        }
        
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log(collider.tag);
        if (collider.tag == "Candy")
        {
            //Debug.Log("Hello");
            if (candy < candyMax)
            {
                addCandy();
                Destroy(collider.gameObject);
            }
        }
        if (collider.tag == "Pentagram")
        {
            giveBoiler();
        }

        if (collider.tag == "Add")
        {
            if (Input.GetKeyDown(KeyCode.Space) && candy > 0)
            {
                candy--;
                Score.GetComponent<Text>().text = candy.ToString() + "/" + candyMax.ToString();
                collider.gameObject.GetComponent<AddCandy>().add();
            }
        }

    }

    private void Update()
    {
        State = States.idle;
        //Debug.Log("kek");
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
