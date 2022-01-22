using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   
    private Snake snake;
    public bool hit;
    public bool works;
    public int destroy;
    public float destroyCounter;
   
    private void Start()
    {
        snake = FindObjectOfType<Snake>();
        destroyCounter = destroy;
    }

    private void Update()
    {
        if (hit)
        {
            gameObject.transform.position = snake.tail2.position;
            destroyCounter -=Time.deltaTime;
            if (destroyCounter < 0)
            { Destroy(snake.tail2.GetComponent<GameObject>());
                destroyCounter=destroy;
            }

            
        }
    }


    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tail"))
        {
            hit = true;
            works = true;
        }
    }
}
