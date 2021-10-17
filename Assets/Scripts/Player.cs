using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Camera mainCamera;
    public Camera boatCamera;

    private Animal m_Selected;

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.isGameOver && !Boat.Instance.isMoving && Input.GetMouseButtonDown(0))
        {
            HandleSelection();
        }
    }

    void HandleSelection()
    {
        var mainRay = mainCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(mainRay, out hit))
        {
            var animal = hit.collider.GetComponentInParent<Animal>();
            m_Selected = animal;

            if (m_Selected != null)
            {
                m_Selected.Talk();
                m_Selected.GoTo();
            }
        }

        var boatRay = boatCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(boatRay, out hit))
        {
            var animal = hit.collider.GetComponentInParent<Animal>();
            m_Selected = animal;

            if (m_Selected != null)
            {
                m_Selected.Talk();
                m_Selected.GoTo();
            }
        }
    }
}
