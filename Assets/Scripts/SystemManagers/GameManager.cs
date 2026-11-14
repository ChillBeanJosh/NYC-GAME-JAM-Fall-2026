using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    public static GameManager Instance => instance;

    [SerializeField] float roundDuration;
    [SerializeField] int maxPlayerLives;

    [SerializeField] GameUI ui;
    [SerializeField] PlayerMovement player;

    bool isGameActive; bool IsGameActive => isGameActive;
    bool isRoundActive; bool IsRoundActive => isRoundActive;

    int round; public int Round => round;
    float roundTimer; public float RoundTimer => roundTimer;
    CameraPosition targetPosition;

    int playerLives; public int PlayerLives => playerLives;
    float inbetweenRoundTimer;



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

    private void Start()
    {
        GameStart();
    }

    private void FixedUpdate()
    {
        if (isGameActive)
        {
            if (isRoundActive)
            {
                roundTimer -= Time.fixedDeltaTime;
                ui.UpdateTimer(roundTimer);
                if (roundTimer <= 0)
                {
                    RoundEnd();
                }
            }
            else
            {
                inbetweenRoundTimer -= Time.deltaTime;
                if (inbetweenRoundTimer <= 0)
                {
                    RoundStart();
                }
            }
        }
    }

    public void GameStart()
    {
        isGameActive = true;
        playerLives = maxPlayerLives;
        round = 0;

        ui.UpdateRound(round + 1);
        
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
        ui.UpdateTimer(roundTimer);

        targetPosition = CameraPosition.StreetTopRight;

        RoundStartEvent.Invoke();
    }

    public void RoundEnd()
    {
        isRoundActive = false;

        if (targetPosition != TransitionManager.Instance.GetCurrentCameraPosition)
        {
            LoseLife();
        }

        inbetweenRoundTimer = 2f;

        RoundEndEvent.Invoke();
    }

    public void LoseLife()
    {
        playerLives--;
        ui.UpdateLives(playerLives);
        if (playerLives <= 0)
        {
            GameEnd();
        }
    }
}
