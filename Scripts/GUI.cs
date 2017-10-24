using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GUI : MonoBehaviour {

    public RawImage InstructionScreen_01;
    public RawImage BackLogo;


    public Button playButton;
    public Button instButton;
    public Button backButton;

	// Use this for initialization
	void Start ()
    {
        PlayerPrefs.SetInt("Games_Won", 0);

        Button bttn;
        bttn = playButton.GetComponent<Button>();
        bttn.onClick.AddListener(PlayGame);

        bttn = instButton.GetComponent<Button>();
        bttn.onClick.AddListener(ShowInstScreen_01);

        bttn = backButton.GetComponent<Button>();
        bttn.onClick.AddListener(GoToMenuScreen);


        InstructionScreen_01.enabled = false;
        BackLogo.enabled = false;
        backButton.gameObject.SetActive(false);


    }
	
    public void PlayGame()
    {
        SceneManager.LoadScene("main_scene");
    }

    public void ShowInstScreen_01()
    {
        //MenuScreen.enabled = false;
        InstructionScreen_01.enabled = true;
        BackLogo.enabled = true;
        backButton.gameObject.SetActive(true);
    }

    
    public void GoToMenuScreen()
    {
        
        backButton.gameObject.SetActive(false);
        InstructionScreen_01.enabled = false;
        BackLogo.enabled = false;
    }

}
