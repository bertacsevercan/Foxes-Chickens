using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader Instance { get; set; }

    public Animator transition;

    private float transitionTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null)
        {
            Destroy(Instance);
            return;
        }
        Instance = this;
    }

    private void PlayAnimation()
    {
        transition.SetTrigger("Start");
    }

    public void LoadNextScene(int sceneIndex)
    {
        StartCoroutine(LoadScene(sceneIndex));
    }

    IEnumerator LoadScene(int sceneIndex)
    {
        PlayAnimation();

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneIndex);
    }
}
