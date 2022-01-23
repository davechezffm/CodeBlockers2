using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static int score;
    public int highScore = 0;
    public Text highScoreText;
    private static bool highscore;
    // Start is called before the first frame update
    void Start()
    {
        score = 1;
        if (highscore)
        {
            highScore = PlayerPrefs.GetInt("HighScore");
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.Save();
        GetComponent<UnityEngine.UI.Text>().text = "Score: " + score;
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            highscore = true;
        }

        highScoreText.text = "HighScore: " + highScore;
    }
}
