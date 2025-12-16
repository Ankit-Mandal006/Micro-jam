using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FinalScoreDisplay : MonoBehaviour
{
    public PlayerScore ps;
    public TMP_Text score;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        score.text="Final Socre : "+ps.score.ToString();
    }
}
