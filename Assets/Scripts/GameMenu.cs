using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameMenu : MonoBehaviour
{
    public static GameMenu Instance { get; set; }

    public Camera mainCamera;
    public Camera boatCamera;
    public AudioListener mainListener;
    public AudioListener boatListener;

    public GameObject menu;
    public GameObject rules;
    public GameObject gameOver;
    public GameObject gameWon;

    public Button startBoatButton;

    public AudioClip audioClip;

    private AudioSource audioSource;

    private void Start()
    {
        if (Instance != null)
        {
            Destroy(Instance);
            return;
        }
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Boat.Instance.IsBoatReady())
        {
            startBoatButton.interactable = true;
        }
        else
        {
            startBoatButton.interactable = false;
        }
    }

    private void PlayAudio()
    {
        audioSource.PlayOneShot(audioClip, 1.0f);
    }

    public void ToggleMenu()
    {
        bool isActive = menu.activeSelf;
        menu.SetActive(!isActive);
        PlayAudio();

        if (!isActive)
            PauseGame();
        else
            ResumeGame();

    }

    public void ToggleRules()
    {
        bool isActive = rules.activeSelf;
        menu.SetActive(isActive);
        rules.SetActive(!isActive);
        PlayAudio();

    }

    public void ShowGameOverPanel()
    {
        gameOver.SetActive(true);
    }

    public void ShowGameWonPanel()
    {
        gameWon.SetActive(true);
    }

    public void RestartGame()
    {
        GameManager.Instance.isRestartKeyPressed = true;
    }

    public void ExitToMenu()
    {
        PlayAudio();
        LevelLoader.Instance.LoadNextScene(0);
        ResumeGame();
    }

    public void ResumeGame()
    {
        PlayAudio();

        Time.timeScale = 1;
    }

    public void PauseGame()
    {
        PlayAudio();

        Time.timeScale = 0;
    }

    public void StartBoat()
    {
        PlayAudio();

        Boat.Instance.isStartKeyPressed = true;
    }

    public void SaveAndExit()
    {
        PlayAudio();

        GameManager.Instance.SaveProgress();
        ExitToMenu();
    }

    public void ShowBoatView()
    {
        PlayAudio();

        mainCamera.enabled = false;
        mainListener.enabled = false;
        boatCamera.enabled = true;
        boatListener.enabled = true;
    }

    public void ShowMainView()
    {
        PlayAudio();

        mainCamera.enabled = true;
        mainListener.enabled = true;
        boatCamera.enabled = false;
        boatListener.enabled = false;
    }
}
