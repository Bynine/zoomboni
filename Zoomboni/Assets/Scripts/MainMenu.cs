using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI highScoreOneText;
    public TextMeshProUGUI highScoreTwoText;
    public TextMeshProUGUI highScoreThreeText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       highScoreOneText.text = PlayerPrefs.GetFloat("SCORE_1").ToString("0"); 
       highScoreTwoText.text = PlayerPrefs.GetFloat("SCORE_2").ToString("0"); 
       highScoreThreeText.text = PlayerPrefs.GetFloat("SCORE_3").ToString("0"); 
    }

    // Function to start the game when users press the start button
    public void BeginGame()
    {
        SceneManager.LoadScene(1);
    }

    //Function to view the controls
    public void OpenControls()
    {

    }

    // Function to exit the game.
    public void ExitGame()
    {
        Application.Quit();
    }
}
