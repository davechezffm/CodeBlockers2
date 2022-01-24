using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Snake : MonoBehaviour
{
    private Vector2 direction = Vector2.right;
    public List<Transform> segmentsList;
    public Transform segmentPrefab;
    public int tail;
    public Food food;
    public bool canMove;
    private SpriteRenderer sprite;
    private Animator anim;
    public SFXManager sfxManager;

    public GameObject enemy;

    private void Start()
    {
        segmentsList = new List<Transform>();
        segmentsList.Add(this.transform);
        food = FindObjectOfType<Food>();
        canMove = true;
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        Grow();

    }


    private void Update()
    {
        for (int i = 1; i < segmentsList.Count; i++)
        {
            if (segmentsList[i].position.x > segmentsList[i - 1].position.x)
            {
                segmentsList[i].gameObject.transform.localScale = new Vector3(1, 1, 1);
                segmentsList[i].gameObject.GetComponent<Animator>().SetBool("Up", false);
                segmentsList[i].gameObject.GetComponent<Animator>().SetBool("Down", false);
            }
            if (segmentsList[i].position.x < segmentsList[i - 1].position.x)
            {
                segmentsList[i].gameObject.transform.localScale = new Vector3(-1, 1, 1);
                segmentsList[i].gameObject.GetComponent<Animator>().SetBool("Up", false);
                segmentsList[i].gameObject.GetComponent<Animator>().SetBool("Down", false);
            }

            if (segmentsList[i].position.y <  segmentsList[i - 1].position.y)
            {
                segmentsList[i].gameObject.GetComponent<Animator>().SetBool("Up", true);
            }
            if (segmentsList[i].position.y > segmentsList[i - 1].position.y)
            {
                segmentsList[i].gameObject.GetComponent<Animator>().SetBool("Down", true);
            }
        }
        if (enemy.GetComponent<Pathfinding.AIDestinationSetter>().enabled == true)
        {
            enemy.GetComponent<Pathfinding.AIDestinationSetter>().target = segmentsList[segmentsList.Count - 1].transform;
        }
       
      
        tail = segmentsList.Count;
        ;
        if (canMove)
        {
            if (direction.x != 0f)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    direction = Vector2.up;

                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    direction = Vector2.down;
                }
            }



            if (direction.y != 0f)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    direction = Vector2.left;
                    anim.Play("Player_Left");
                    anim.SetBool("Right", false);

                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    direction = Vector2.right;
                    anim.SetBool("Right", true);

                }
            }
        }
        
    }

    private void FixedUpdate()
    { for (int i = segmentsList.Count - 1; i > 0; i--)
        {
            segmentsList[i].position = segmentsList[i - 1].position;
        }
        transform.position = new Vector3(
         Mathf.Round(transform.position.x) + direction.x,
         Mathf.Round(transform.position.y) + direction.y
         , 0.0f);


        for (int i = segmentsList.Count - 1; i > 0; i--)
        {
            if (segmentsList[i].position != segmentsList[i - 1].position)
            {
                segmentsList[i].tag = "Obstacle";
                segmentsList[i].GetComponent<SpriteRenderer>().color =
               new Color(255, 255, 255, 255);

            }
            segmentsList[segmentsList.Count - 1].tag = "Tail";
            segmentsList[segmentsList.Count - 1].GetComponent<SpriteRenderer>().color =
                new Color(255, 255, 0, 255);


        }

       

    }

    private void Grow()
    {
        Transform segment = Instantiate(segmentPrefab);
        segment.position = segmentsList[segmentsList.Count - 1].position;

            segmentsList.Add(segment);
        Score.score++;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Food"))
        {
            Grow();
            sfxManager.grow.Play();
        }

        if (collision.CompareTag ("Obstacle"))
        {
            ResetGame();
            sfxManager.gameOver.Play();
        }

        if (enemy.GetComponent<Enemy>().hit == true)
        {
            if (collision.CompareTag("Pepper"))
            {
                enemy.GetComponent<Enemy>().MoveToWater();
                Destroy(collision.gameObject, 0.2f);
                
               
                
            }
        }

    }

    public void ResetGame()
    {
       /* for (int i = 1; i < segmentsList.Count; i++)
        {
            Destroy(segmentsList[i].gameObject);
        }
            segmentsList.Clear();
            segmentsList.Add(this.transform);
            this.transform.position = Vector3.zero;
        enemy.transform.position = new Vector2(4, -4);*/

        enemy.GetComponent<Enemy>().hit = false;
        canMove = false;
        sprite.color = new Color(0, 0, 0, 0);
        Invoke("LoadScene", 2f);
        direction = Vector2.zero;

        

    }

    private void LoadScene()
    {
        SceneManager.LoadScene(2);
    }
}
