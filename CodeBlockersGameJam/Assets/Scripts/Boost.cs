using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{
  
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

        }
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
