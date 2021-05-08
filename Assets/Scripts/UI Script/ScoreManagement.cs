using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManagement : MonoBehaviour
{
    private int score;
    public Text score_board;

    void Start()
    {
        score = 0;
    }

    void Update()
    {
        score_board.text = score.ToString();
    }

    void update_score(int points) {
        score += points;
    }
}
