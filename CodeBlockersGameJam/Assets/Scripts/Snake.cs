using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2 direction = Vector2.right;
    private List<Transform> segmentsList;
    public Transform segmentPrefab;
    public int tail;
    public Transform tail2;
    public GameObject enemy;

    private void Start()
    {
        segmentsList = new List<Transform>();
        segmentsList.Add(this.transform);
       
    }


    private void Update()
    {
        enemy.GetComponent<Pathfinding.AIDestinationSetter>().target = tail2;
        tail = segmentsList.Count;
        tail2 = segmentsList[tail - 1];
        
        if (direction.x != 0f) {
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
            tail2.tag = "Tail";
                
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

    private void ResetGame()
    {
        for (int i = 1; i < segmentsList.Count; i++)
        {
            Destroy(segmentsList[i].gameObject);
        }
            segmentsList.Clear();
            segmentsList.Add(this.transform);
            this.transform.position = Vector3.zero;
        
    }
}
