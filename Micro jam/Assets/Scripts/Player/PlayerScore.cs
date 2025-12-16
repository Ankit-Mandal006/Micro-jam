using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScore : MonoBehaviour
{
    public int score=0;
    public TMP_Text scoreText;  

    public void UpdateScore(int s)
    {
        score+=s;
        scoreText.text="Score:"+score.ToString();
    }
}