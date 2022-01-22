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
   
    public GameObject enemy;

    private void Start()
    {
        segmentsList = new List<Transform>();
        segmentsList.Add(this.transform);
        food = FindObjectOfType<Food>();
        canMove = true;
        sprite = GetComponent<SpriteRenderer>();
       
    }


    private void Update()
    {
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

                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    direction = Vector2.right;

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

            }
            segmentsList[segmentsList.Count - 1].tag = "Tail";
                
        }

            }

    private void Grow()
    {
        Transform segment = Instantiate(segmentPrefab);
        segment.position = segmentsList[segmentsList.Count - 1].position;

            segmentsList.Add(segment);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Food"))
        {
            Grow();
        }

        if (collision.CompareTag ("Obstacle"))
        {
            ResetGame();
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
