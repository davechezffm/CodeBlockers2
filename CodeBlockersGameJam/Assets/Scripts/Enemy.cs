using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   
    private Snake snake;
    public bool hit;
    public int destroy=4;
    public float destroyCounter;
    public GameObject tail;
    public bool first;
   
    private void Start()
    {
        snake = FindObjectOfType<Snake>();
        destroyCounter = destroy;
        first = true;
    }

    private void Update()
    {
        
            tail = snake.segmentsList[snake.segmentsList.Count - 1].transform.gameObject;
        
        if (hit == true) {
           

                {
                    gameObject.transform.position = snake.segmentsList[snake.segmentsList.Count - 1].transform.position;
                    destroyCounter -= Time.deltaTime;
                    GetComponent<Pathfinding.Seeker>().enabled = false;
                    GetComponent<Pathfinding.AIDestinationSetter>().enabled = false;
                }
            }

                    if (destroyCounter < 0)
                    {
                snake.segmentsList.RemoveAt(snake.segmentsList.Count - 1) ;

                  
                        Destroy(tail,0.2f);
                        destroyCounter = destroy;
                        tail = snake.segmentsList[snake.segmentsList.Count - 1].transform.gameObject;
                        transform.position= snake.segmentsList[snake.segmentsList.Count - 2].position;
                    }
                
            
        
    }


    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tail"))
        {
            hit = true;
            
        }
    }
}
