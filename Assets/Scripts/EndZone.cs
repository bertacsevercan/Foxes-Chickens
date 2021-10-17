using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndZone : Ground
{

    public static EndZone Instance { get; set; }

    private void Start()
    {
        if (Instance != null)
        {
            Destroy(Instance);
            return;
        }
        Instance = this;
    }


    public override void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.GetComponentInParent<Fox>())
        {
            foxCountOnEnd++;
        }
        else if (collision.gameObject.GetComponentInParent<Chicken>())
        {
            chickCountOnEnd++;
        }
        Win();
    }

    public override void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponentInParent<Fox>())
        {
            foxCountOnEnd--;
        }
        else if (collision.gameObject.GetComponentInParent<Chicken>())
        {
            chickCountOnEnd--;
        }
    }

    public override void Fail()
    {

        if (chickCountOnEnd > 0 && foxCountOnEnd > 0)
        {
            if (foxCountOnEnd > chickCountOnEnd)
            {
                GameMenu.Instance.ShowGameOverPanel();
                GameManager.Instance.isGameLost = true;
                GameManager.Instance.isGameOver = true;
            }
        }
    }

    public void Win()
    {
        if (chickCountOnEnd == 3 && foxCountOnEnd == 3)
        {
            GameMenu.Instance.ShowGameWonPanel();
            GameManager.Instance.isGameWon = true;
            GameManager.Instance.isGameOver = true;
        }

    }
}
