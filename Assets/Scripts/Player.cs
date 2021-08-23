using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Camera gameCamera;

    private Animal m_Selected;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleSelection();
        }
    }

    void HandleSelection()
    {
        var ray = gameCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var animal = hit.collider.GetComponent<Animal>();
            m_Selected = animal;

            if (m_Selected != null)
            {
                m_Selected.GoTo();

            }

            print(m_Selected);
        }
    }
}
