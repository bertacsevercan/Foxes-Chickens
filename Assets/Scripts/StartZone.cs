using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartZone : Ground
{
    public static StartZone Instance { get; set; }

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
            foxCountOnStart++;
        }
        else if (collision.gameObject.GetComponentInParent<Chicken>())
        {
            chickCountOnStart++;
        }
    }

    public override void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponentInParent<Fox>())
        {
            foxCountOnStart--;
        }
        else if (collision.gameObject.GetComponentInParent<Chicken>())
        {
            chickCountOnStart--;
        }

    }

    public override void Fail()
    {
        if (chickCountOnStart > 0 && foxCountOnStart > 0)
        {
            if (foxCountOnStart > chickCountOnStart)
            {
                GameMenu.Instance.ShowGameOverPanel();
                GameManager.Instance.isGameLost = true;
                GameManager.Instance.isGameOver = true;
            }
        }
    }
}
