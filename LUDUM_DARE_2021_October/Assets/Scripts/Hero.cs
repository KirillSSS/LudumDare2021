using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hero : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private int lives = 5, boilCand = 0, boilCandMax = 100;

    public int candy = 0, candyMax;

    [SerializeField] private GameObject Pause_menu, Score, ScoreBoiler, Boiler, space, torch, scoreTorch, win;
    //GameObject Score = GameObject.Find("Score_text");
    private Paues_end end;
    private bool inBoiler = false, inAdd = false;
    public Collider2D coll;
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
        if (inBoiler)
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
    private void addTourch()
    {
        if(inAdd==true && candy > 0)
        {
            Debug.Log("Kek");
            Debug.Log(coll.gameObject.GetComponent<AddCandy>().candy);
            coll.gameObject.GetComponent<AddCandy>().add();
            
            scoreTorch.GetComponent<Text>().text = coll.gameObject.GetComponent<AddCandy>().candy.ToString();
            candy--;
            Score.GetComponent<Text>().text = candy.ToString() + "/" + candyMax.ToString();
        }
    }

   

    private void Awake()
    {
        Time.timeScale = 1f;
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
    
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    /*if (collision.tag == "Pentagram")
    //    {
    //        giveBoiler();
    //    }*/

    //    if (collision.tag == "Add")
    //    {
    //        if (Input.GetKeyDown(KeyCode.Space) && candy > 0)
    //        {
    //            candy--;
    //            Score.GetComponent<Text>().text = candy.ToString() + "/" + candyMax.ToString();
    //            collision.gameObject.GetComponent<AddCandy>().add();
    //        }
    //    }

    //}

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Pentagram")
        {
            inBoiler = false;
            //space.SetActive(false);
        }

        if (collision.tag == "Add")
        {
            inAdd = false;
            //coll = collider;
            torch.SetActive(false);
            scoreTorch.SetActive(false);
            //space.SetActive(false);

        }



    }
    private void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.tag == "Pentagram")
        {
            inBoiler = true;
            space.SetActive(true);
        }

        //Debug.Log(collider.tag);
        if (collider.tag == "Candy")
        {
            //Debug.Log("Hello");
            if (candy < candyMax)
            {
                addCandy();
                Destroy(collider.gameObject);
            }
        }
        if (collider.tag == "Add")
        {
            inAdd = true;
            space.SetActive(true);
            coll = collider;
            torch.SetActive(true);
            scoreTorch.SetActive(true);
            //Debug.Log(collider.gameObject.GetComponent<AddCandy>().candy);
            scoreTorch.GetComponent<Text>().text = coll.gameObject.GetComponent<AddCandy>().candy.ToString();

        }


    }

    private void Update()
    {
        State = States.idle;
        Score.GetComponent<Text>().text = candy.ToString() + "/" + candyMax.ToString();
        string s = coll.gameObject.GetComponent<AddCandy>().candy.ToString();
        int n = s.IndexOf(",");
        if(n>0) s = s.Substring(0, n + 2);
        scoreTorch.GetComponent<Text>().text = s;
        //Debug.Log("kek");
        if (Input.GetButton("Vertical")) runVertical();
        if (Input.GetButton("Horizontal")) runHorizontal();
        if (Input.GetKeyDown(KeyCode.Space)) giveBoiler();
        if (Input.GetKeyDown(KeyCode.Space)) addTourch();

        if (boilCand == boilCandMax)
            win.SetActive(true);

        if (!inAdd && !inBoiler) space.SetActive(false);
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
