using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI finishText;
    public TextMeshProUGUI timeText;
    public Timer timerLevelDuration;
    public AudioSource music;
    public AudioSource sfxClean;

    public float PITCH_MUSIC_NORMAL = 1.0f;
    public float PITCH_MUSIC_URGENT = 1.2f;
    public float TIME_BONUS = 10.0f;

    public PlayerInput playerInput;

    private int points = 0;

    private InputAction
            inputReset,
            inputEscape;

    private float MAX_POINTS = 0;
    private bool ended = false;

    /** SINGLETON **/
    private static LevelManager instance;
    public static LevelManager GetInstance()
    {
        if (instance == null)
        {
            Debug.LogError("Tried to access LevelManager before its instantiation");
        }
        return instance;
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;

        finishText.text = "";
        timerLevelDuration.Reset();
        inputReset = playerInput.actions.FindAction("Reset");
        inputEscape = playerInput.actions.FindAction("Escape");

        MAX_POINTS = 0;
        Collectable[] collectables = FindObjectsByType<Collectable>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
        foreach(Collectable collectable in collectables)
        {
            MAX_POINTS += collectable.GetScore();
        }
        ended = false;

        float SCORE_1 = GetScore("1");
        if (SCORE_1 == 0){ // Set initial scores
            Debug.Log("Setting initial scores");
            PlayerPrefs.SetFloat("SCORE_1", 400);
            PlayerPrefs.SetFloat("SCORE_2", 250);
            PlayerPrefs.SetFloat("SCORE_3", 100);
            PlayerPrefs.Save();
        }
    }
    public void AddPoints(int points)
    {
        this.points += points;
        sfxClean.Play();
    }

    public void Update()
    {
        UpdateInputs();
        UpdateUI();
    }

    private void UpdateInputs()
    {
        if (inputReset.WasPressedThisFrame())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (ended && inputEscape.WasPressedThisFrame())
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    private void UpdateUI()
    {
        if (points == MAX_POINTS)
        {
            End();
        }
        else if (timerLevelDuration.JustDeactivated())
        {
            End();
        }
        else if (timerLevelDuration.IsActive())
        {
            float timeRemaining =
                Mathf.Round(
                    timerLevelDuration.GetMax()
                     * (1.0f - timerLevelDuration.GetPercent())
                    / 60f
                );
            timeRemaining = Mathf.Clamp(timeRemaining, 0, int.MaxValue);

            if (timerLevelDuration.GetPercent() >= (5.0f/6.0f))
            {
                timeText.color = Color.red;
                music.pitch = PITCH_MUSIC_URGENT;
            }

            timeText.text = "Time: " + timeRemaining;

            scoreText.text = "Points: " + points + "/" + MAX_POINTS;
        }
    }

    private void End()
    {
        if (ended) return;

        ended = true;
        scoreText.text = "";
        timeText.text = "";
        float SCORE_N = points;
        SCORE_N += (1.0f - timerLevelDuration.GetPercent()) * TIME_BONUS * timerLevelDuration.GetMax();
        SCORE_N = Mathf.Round(SCORE_N);
        music.pitch = PITCH_MUSIC_NORMAL;

        {
            timerLevelDuration.End();
            finishText.text = "Wow! You got a score of " + SCORE_N + "!\n Press ESC to return to main menu or R to reset!";

            float SCORE_1 = GetScore("1");
            float SCORE_2 = GetScore("2");
            float SCORE_3 = GetScore("3");

            if (SCORE_N > SCORE_1)
            {
                SCORE_3 = SCORE_2;
                SCORE_2 = SCORE_1;
                SCORE_1 = SCORE_N;
            }
            else if (SCORE_N > SCORE_2)
            {
                SCORE_3 = SCORE_2;
                SCORE_2 = SCORE_N;
            }
            else if (SCORE_N > SCORE_3)
            {
                SCORE_3 = SCORE_N;
            }

            Debug.Log("Best Scores: 1: " + SCORE_1 + ", 2: " + SCORE_2 + ", 3: " + SCORE_3);
            PlayerPrefs.SetFloat("SCORE_1", SCORE_1 == float.MaxValue ? 0 : SCORE_1);
            PlayerPrefs.SetFloat("SCORE_2", SCORE_2 == float.MaxValue ? 0 : SCORE_2);
            PlayerPrefs.SetFloat("SCORE_3", SCORE_3 == float.MaxValue ? 0 : SCORE_3);
            PlayerPrefs.Save();
        }
        
    }

    private float GetScore(string n)
    {
        float score = PlayerPrefs.GetFloat("SCORE_" + n);
        return score;
    }

}
