using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text score;

    public int currentScore;

    void Start()
    {
        currentScore = 0;
        score.text = " " + currentScore;
    }

    public void IncreaseScore()
    {
        Debug.Log("increased Score");
        currentScore += 2;

        score.text = " " + currentScore;
    }
}
