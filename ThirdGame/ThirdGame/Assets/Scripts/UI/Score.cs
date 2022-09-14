using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI score;

    public int currentScore;

    void Start()
    {
        currentScore = 0;
        score.text = "" + currentScore;
    }

    public void IncreaseScore()
    {
        currentScore += 2;
        score.text = "" + currentScore;
    }
}
