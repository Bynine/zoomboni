using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI highScoreOneText;
    public TextMeshProUGUI highScoreTwoText;
    public TextMeshProUGUI highScoreThreeText;

    public GameObject startButton;
    public GameObject controlsButton;
    public GameObject exitButton;

    public RawImage startSelectedBear;
    public RawImage controlsSelectedBear;
    public RawImage exitSelectedBear;

    public Canvas mainMenuCanvas;
    public Canvas controlsCanvas;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       highScoreOneText.text = PlayerPrefs.GetFloat("SCORE_1").ToString("0"); 
       highScoreTwoText.text = PlayerPrefs.GetFloat("SCORE_2").ToString("0"); 
       highScoreThreeText.text = PlayerPrefs.GetFloat("SCORE_3").ToString("0"); 
   
       UpdateMenuCursor();
       
       mainMenuCanvas.enabled=true;
       controlsCanvas.enabled=false;
    }

    void Update()
    {
       UpdateMenuCursor();
    }

    // Function to start the game when users press the start button
    public void BeginGame()
    {
        SceneManager.LoadScene(1);
    }

    //Function to view the controls
    public void OpenControls()
    {
       mainMenuCanvas.enabled=false;
       controlsCanvas.enabled=true;
    }

    // Function to exit the game.
    public void ExitGame()
    {
        Application.Quit();
    }

    public void CloseControls()
    {
       mainMenuCanvas.enabled=true;
       controlsCanvas.enabled=false;
    }

    private void UpdateMenuCursor()
    {
        if(EventSystem.current.currentSelectedGameObject == startButton){
            startSelectedBear.enabled=true;
            controlsSelectedBear.enabled=false;
            exitSelectedBear.enabled=false;
        }
        if(EventSystem.current.currentSelectedGameObject == controlsButton){
            startSelectedBear.enabled=false;
            controlsSelectedBear.enabled=true;
            exitSelectedBear.enabled=false;
        }
        if(EventSystem.current.currentSelectedGameObject == exitButton){
            startSelectedBear.enabled=false;
            controlsSelectedBear.enabled=false;
            exitSelectedBear.enabled=true;
        }
    }
}
