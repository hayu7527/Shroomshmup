using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scorescript : MonoBehaviour {
    public Text leftScore;
    public Text rightScore;

    public static int scoreLeft;
    public static int scoreRight;
    
    public float gameRestartDelay = 2f;
    public static scorescript S;

    public void Awake() {
        scoreLeft = 0;
        scoreRight = 0;
        DisplayScore();
        S = this;
    }

    public void UpdateScore(int scorer) {
        if (scorer <= 5) {
            scoreRight++;
        }
        else
        {
            Reset3();
        }

        DisplayScore();
    }
    
    public void UpdateScore2(int scorer2) {
        if (scorer2 <= 5) {
            scoreLeft++;
        }
        else
        {
            Reset2();
        }

        DisplayScore();
    }

    private void DisplayScore() {
        leftScore.text = scoreLeft.ToString();
        rightScore.text = scoreRight.ToString();

    }
void Reset2()
    {

        Main.S.RestartGreen();
        
    }
    public void Reset3()
    {

        Main.S.RestartRed();
        
    }
}