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
    public GameObject backButton;

    public Button startButtonB;
    public Button controlsButtonB;
    public Button exitButtonB;
    public Button backButtonB;
    
    public RawImage startSelectedBear;
    public RawImage controlsSelectedBear;
    public RawImage exitSelectedBear;
    public RawImage backSelectedBear;

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
       backButtonB.enabled=false;
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
       startButtonB.enabled=false;
       controlsButtonB.enabled=false;
       exitButtonB.enabled=false;
       backButtonB.enabled=true;
       backButtonB.Select();
    }

    // Function to exit the game.
    public void ExitGame()
    {
        Application.Quit();
    }

    // Return to the main menu from the controls menu
    public void CloseControls()
    {
       mainMenuCanvas.enabled=true;
       controlsCanvas.enabled=false;
       startButtonB.enabled=true;
       controlsButtonB.enabled=true;
       exitButtonB.enabled=true;
       backButtonB.enabled=false;
       controlsButtonB.Select();
    }

    // Hide and show cursor images depending on which button is selected
    public void UpdateMenuCursor()
    {
        if(EventSystem.current.currentSelectedGameObject == startButton){
            startSelectedBear.enabled=true;
            controlsSelectedBear.enabled=false;
            exitSelectedBear.enabled=false;
        } else if(EventSystem.current.currentSelectedGameObject == controlsButton){
            startSelectedBear.enabled=false;
            controlsSelectedBear.enabled=true;
            exitSelectedBear.enabled=false;
        } else if(EventSystem.current.currentSelectedGameObject == exitButton){
            startSelectedBear.enabled=false;
            controlsSelectedBear.enabled=false;
            exitSelectedBear.enabled=true;
        }
        if(EventSystem.current.currentSelectedGameObject == backButton){
            backSelectedBear.enabled=true;
        } else {
            backSelectedBear.enabled=false;
        }
            
    }

    // Update cursor on mouse over
    public void StartButtonMouseOver()
    {
        Debug.Log("Start Mouse Over");
        startSelectedBear.enabled=true;
        controlsSelectedBear.enabled=false;
        exitSelectedBear.enabled=false;
        startButtonB.Select();
    }
    
    // Update cursor on mouse over
    public void ControlsButtonMouseOver()
    {
        Debug.Log("Controls Mouse Over");
        startSelectedBear.enabled=false;
        controlsSelectedBear.enabled=true;
        exitSelectedBear.enabled=false;
        controlsButtonB.Select();
    }

    // Update cursor on mouse over
    public void ExitButtonMouseOver()
    {
        Debug.Log("Exit Mouse Over");
        startSelectedBear.enabled=false;
        controlsSelectedBear.enabled=false;
        exitSelectedBear.enabled=true;
        exitButtonB.Select();
    }
}
