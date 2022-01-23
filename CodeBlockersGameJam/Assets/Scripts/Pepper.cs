using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Pepper : MonoBehaviour
{
    public GameObject gridArea;
    // Start is called before the first frame update
    void Start()
    {

        gridArea = GameObject.Find("GridArea");
            Bounds bounds = gridArea.GetComponent<BoxCollider2D>().bounds;
            float x = Random.Range(bounds.min.x, bounds.max.x);
            float y = Random.Range(bounds.min.y, bounds.max.y);
            this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            Bounds bounds = gridArea.GetComponent<BoxCollider2D>().bounds;
            float x = Random.Range(bounds.min.x, bounds.max.x);
            float y = Random.Range(bounds.min.y, bounds.max.y);
            this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
        }
    }



}
