using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuManager : MonoBehaviour
{
    public GameObject leaderboard;

    public AudioClip audioClip;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    public void StartGame()
    {
        PlayAudio();
        LevelLoader.Instance.LoadNextScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        PlayAudio();
        EditorApplication.isPlaying = false;
#else
        Application.Quit(); // original code to quit Unity player
#endif

    }

    public void ToggleLeaderboard()
    {
        PlayAudio();
        bool isActive = leaderboard.activeSelf;
        leaderboard.SetActive(!isActive);
    }

    public void ResetLeaderboard()
    {
        PlayAudio();
        GameManager.Instance.DeleteSave();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void PlayAudio()
    {
        audioSource.PlayOneShot(audioClip, 1.0f);
    }
}