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
    public AudioSource sfxClean;

    public PlayerInput playerInput;

    private int score = 0;

    private InputAction
            inputReset,
            inputEscape;

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

        scoreText.text = "Score: " + score;
        finishText.text = "";
        timerLevelDuration.Reset();
        inputReset = playerInput.actions.FindAction("Reset");
        inputEscape = playerInput.actions.FindAction("Escape");
    }
    public void AddPoints(int points)
    {
        score += points;
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
            print("Resetting scene");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (inputEscape.WasPressedThisFrame())
        {
            print("So long, fair well");
            Application.Quit();
        }
    }

    private void UpdateUI()
    {
        if (timerLevelDuration.JustDeactivated())
        {
            scoreText.text = "";
            timeText.text = "";
            finishText.text = "Time's up! Score : " + score + ". Press R to reset!";
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

            timeText.text = "Time: " + timeRemaining;

            scoreText.text = "Score: " + score;
        }
    }

}
