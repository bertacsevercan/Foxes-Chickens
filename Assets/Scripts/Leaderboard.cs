using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    public List<GameManager.SaveData> leaderboard = new List<GameManager.SaveData>();

    public Font font;

    private float textWidth = 300f;
    private float textHeight = 30f;

    private RectTransform rectTransform;

    private Vector2 position = new Vector2(0, 50);

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.LoadProgress();

        if (leaderboard.Count == 0)
        {
            string entry = "No one has beaten the game yet!";
            GameObject newObj = new GameObject("New Score");

            newObj.transform.SetParent(transform);

            Text text = newObj.AddComponent<Text>();

            rectTransform = newObj.GetComponent<RectTransform>();

            Vector2 offset = 50 * Vector2.down;
            rectTransform.anchoredPosition = position + offset;
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, textHeight);
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, textWidth);
            rectTransform.localScale = Vector3.one;
            text.font = font;
            text.resizeTextForBestFit = true;
            text.resizeTextMinSize = 14;
            text.text = entry;
        }
        else
        {
            int maxLimit = 5;
            for (int i = 0; i < maxLimit; i++)
            {
                string entry = $"-> Name: {leaderboard[i].playerName} - Score: {leaderboard[i].playerScore}";
                GameObject newObj = new GameObject("New Score");

                newObj.transform.SetParent(transform);

                Text text = newObj.AddComponent<Text>();

                rectTransform = newObj.GetComponent<RectTransform>();

                rectTransform.anchoredPosition = position;
                position += 30 * Vector2.down;
                rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, textHeight);
                rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, textWidth);
                rectTransform.localScale = Vector3.one;
                text.font = font;
                text.resizeTextForBestFit = true;
                text.resizeTextMinSize = 14;
                text.text = entry;
            }
        }
    }
}
