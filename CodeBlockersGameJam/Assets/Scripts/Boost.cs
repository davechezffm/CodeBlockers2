using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{
    public BoxCollider2D gridArea;

    public int boostSpeedTime = 3;
    private float boostSpeedTimeCounter;
    private bool boost;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {//Here is the number to change how much faster the player goes.
            Time.fixedDeltaTime = 0.05f;
            boost = true;
            boostSpeedTimeCounter = boostSpeedTime;
            RandomizePosition();

        }
    }

    public void RandomizePosition()
    {
        Bounds bounds = this.gridArea.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);

    }
    private void Update()
    {
        if (boost)
        {
            boostSpeedTimeCounter -= Time.deltaTime;
            if (boostSpeedTimeCounter < 0)
            {
                boost = false;
                Time.fixedDeltaTime = 0.15f;


            }
        }
    }
}
