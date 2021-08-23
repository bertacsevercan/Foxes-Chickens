using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    private float speed = 5.0f;
    private bool isMoving;
    private bool isOnMid;
    // Start is called before the first frame update
    void Start()
    {
        isMoving = false;
        isOnMid = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving && Input.GetButtonDown("Jump"))
        {
            isMoving = true;
            StartCoroutine("Stop");
        }
        Move();
    }

    private void Move()
    {
        if (isMoving && Animal.Instance.isOnStartZone)
            gameObject.transform.Translate(Vector3.forward * speed * Time.deltaTime);

        else if (isMoving && Animal.Instance.isOnEndZone)
            gameObject.transform.Translate(Vector3.back * speed * Time.deltaTime);

    }

    IEnumerator Stop()
    {
        yield return new WaitForSeconds(12.0f);
        isMoving = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Start Zone")
        {
            Animal.Instance.isOnStartZone = true;
            Animal.Instance.isOnEndZone = false;
        }
        else if (other.name == "End Zone")
        {
            Animal.Instance.isOnStartZone = false;
            Animal.Instance.isOnEndZone = true;

        }
        else if (other.name == "Mid Zone")
        {
            isOnMid = true;
        }
        print(other.name);
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.GetComponentInParent<Animal>())
        {
            collision.collider.transform.SetParent(transform);
        }

    }

    private void OnCollisionExit(Collision collision)
    {

        if (collision.gameObject.GetComponentInParent<Animal>())
        {
            collision.collider.transform.SetParent(null);
        }
    }
}
