using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    [SerializeField] TMP_Text roundCounterText;
    [SerializeField] TMP_Text roundTimerText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateRound(int currentRound)
    {
        roundCounterText.text = "Round " + currentRound.ToString();
    }

    public void UpdateTimer(float currentTime)
    {
        roundTimerText.text = "";
    }
}
