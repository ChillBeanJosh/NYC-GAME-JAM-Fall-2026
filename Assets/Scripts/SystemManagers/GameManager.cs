using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    public static GameManager Instance => instance;


    bool isGameActive; bool IsGameActive => isGameActive;
    bool isRoundActive; bool IsRoundActive => isRoundActive;

    int round;
    float roundTimer;

    public UnityEvent GameStartEvent { private set; get; }
    public UnityEvent GameEndEvent { private set; get; }

    public UnityEvent RoundStartEvent { private set; get; }
    public UnityEvent RoundEndEvent { private set; get; }

    public void Awake()
    {
        instance = this;
        
        GameStartEvent = new UnityEvent();
        GameEndEvent = new UnityEvent();
        RoundStartEvent = new UnityEvent();
        RoundEndEvent = new UnityEvent();
    }

    public void GameStart()
    {
        GameStartEvent.Invoke();
    }

    public void GameEnd()
    {
        GameEndEvent.Invoke();
    }

    public void RoundStart()
    {
        RoundStartEvent.Invoke();
    }

    public void RoundEnd()
    {
        RoundEndEvent.Invoke();
    }
}
