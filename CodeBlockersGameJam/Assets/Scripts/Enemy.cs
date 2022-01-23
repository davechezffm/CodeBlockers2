using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
   
    private Snake snake;
    public bool hit;
    public int destroy=4;
    public float destroyCounter;
    public GameObject tail;
    public bool first;
    AIPath path;
    public Transform target;
    
    
   
   
    private void Start()
    {
        snake = FindObjectOfType<Snake>();
        destroyCounter = destroy;
        first = true;
        path = GetComponent<AIPath>();
        
       
    }

    
    
    

    private void Update()
    {
        if (path.destination.x <= -0.1)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (path.destination.x >= -0.1)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        target = snake.segmentsList[snake.segmentsList.Count - 1].transform;
        if (snake.canMove == true)
            tail = snake.segmentsList[snake.segmentsList.Count - 1].transform.gameObject;

        if (hit == true)
        {
            

                gameObject.transform.position = snake.segmentsList[snake.segmentsList.Count - 1].transform.position;
            destroyCounter -= Time.deltaTime;
            GetComponent<Pathfinding.Seeker>().enabled = false;
            GetComponent<Pathfinding.AIDestinationSetter>().enabled = false;



            if (destroyCounter < 0)
            {
                snake.segmentsList.RemoveAt(snake.segmentsList.Count - 1);



                Destroy(tail, 0.2f);
                if (snake.segmentsList.Count < 2)
                {
                    snake.ResetGame();
                    //GetComponent<Pathfinding.Seeker>().enabled = true;
                    //GetComponent<Pathfinding.AIDestinationSetter>().enabled = true;
                }
                else
                {
                    destroyCounter = destroy;
                    tail = snake.segmentsList[snake.segmentsList.Count - 1].transform.gameObject;
                    transform.position = snake.segmentsList[snake.segmentsList.Count - 2].position;
                }
            }
        }
            
        
    }


    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tail"))
        {
            hit = true;
            
        }

        if (hit == true)
        {
            if (collision.CompareTag("Player"))
            {
                transform.position = new Vector2(4, -4);
                hit = false;
                GetComponent<Pathfinding.Seeker>().enabled = true;
                GetComponent<Pathfinding.AIDestinationSetter>().enabled = true;
            }
        }
    }
}
