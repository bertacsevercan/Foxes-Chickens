using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    public string playerName;

    public int playerScore = 0;

    public bool isGameOver;
    public bool isGameWon;
    public bool isGameLost;
    public bool isRestartKeyPressed;

    public Leaderboard Leaderboard;

    public List<SaveData> leaderboard = new List<SaveData>();

    public Text nameText;
    public Text scoreText;

    private string savePath = "";

    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
        isGameWon = false;
        isGameLost = false;
        isRestartKeyPressed = false;

        savePath = Application.persistentDataPath + "/savefile.json";

        if (Instance != null)
        {
            Destroy(Instance);
            return;
        }
        Instance = this;
        if (scoreText)
            UpdateScore(0);
    }

    // Update is called once per frame
    void Update()
    {
        StartOver();
    }

    public void UpdateScore(int scoreToAdd)
    {
        playerScore += scoreToAdd;
        scoreText.text = "Number Of Travels Made: " + playerScore;
    }

    public void StartOver()
    {
        if (isGameLost && isRestartKeyPressed)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    [System.Serializable]
    public class SaveData
    {
        public int playerScore;
        public string playerName;
    }

    public void SaveProgress()
    {
        playerName = nameText.text.Replace(' ', '_');

        SaveData saveData = new SaveData
        {
            playerScore = playerScore,
            playerName = playerName
        };


        string json = JsonUtility.ToJson(saveData);

        File.AppendAllText(savePath, json);
        File.AppendAllText(savePath, " ");

    }

    public void LoadProgress()
    {

        if (File.Exists(savePath))
        {
            string[] jsonArr = File.ReadAllText(savePath).Split(' ');

            foreach (string json in jsonArr)
            {
                SaveData data = JsonUtility.FromJson<SaveData>(json);

                if (data?.playerName != null)
                {
                    leaderboard.Add(data);
                }
            }

            leaderboard.Sort((u1, u2) => u1.playerScore.CompareTo(u2.playerScore));
            //leaderboard.Reverse();

            Leaderboard.leaderboard = leaderboard;
        }
    }

    public void DeleteSave()
    {
        File.Delete(savePath);
    }
}
