using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    public static GameManager Instance => instance;

    [SerializeField] float roundDuration;
    [SerializeField] int maxPlayerLives;


    bool isGameActive; bool IsGameActive => isGameActive;
    bool isRoundActive; bool IsRoundActive => isRoundActive;

    int round; public int Round => round;
    float roundTimer; public float RoundTimer => roundTimer;


    int playerLives; public int PlayerLives => playerLives;


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

    private void FixedUpdate()
    {
        if (isGameActive)
        {
            if (isRoundActive)
            {
                roundTimer -= Time.fixedDeltaTime;
                if (roundTimer <= 0)
                {
                    RoundEnd();
                }
            }
        }
    }

    public void GameStart()
    {
        isGameActive = true;
        playerLives = maxPlayerLives;
        round = 0;

        GameStartEvent.Invoke();
    }

    public void GameEnd()
    {
        isGameActive = false;

        GameEndEvent.Invoke();
    }

    public void RoundStart()
    {
        isRoundActive = true;
        roundTimer = roundDuration;

        RoundStartEvent.Invoke();
    }

    public void RoundEnd()
    {
        isRoundActive = false;

        RoundEndEvent.Invoke();
    }

    public void LoseLife()
    {
        playerLives--;
        if (playerLives <= 0)
        {
            GameEnd();
        }
    }
}
