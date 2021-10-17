using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Animal : MonoBehaviour
{
    private AudioSource audioSource;

    private Vector3 startPos;
    private Vector3 offset;

    private bool isOnBoat;

    public AudioClip audioClip;

    public bool isOnStartGround;
    public bool isOnEndGround;

    // Start is called before the first frame update
    void Start()
    {
        isOnStartGround = true;
        isOnEndGround = false;
        audioSource = GetComponent<AudioSource>();
        GetStartingPos();
    }

    public void Talk()
    {
        audioSource.PlayOneShot(audioClip, 1.0f);
    }

    public void GoTo()
    {
        offset = new Vector3(4, 3, 1);
        Vector3 boatPos = Boat.Instance.transform.position;

        if (Boat.Instance.animalCount == 1)
        {
            float animalPosZ = Mathf.Round(GetAnimalOnBoatPosition().z);
            float movePosZ = Mathf.Round((boatPos + offset).z);

            if (animalPosZ == movePosZ)
            {
                offset = 2 * (Vector3.back);
            }
            else
            {
                offset = 2 * (Vector3.forward);

            }
            boatPos = GetAnimalOnBoatPosition();
        }

        if (Boat.Instance.isOnEndZone && isOnEndGround && !isOnBoat && !Boat.Instance.IsBoatFull())
        {
            transform.position = boatPos + offset;
        }
        else if (Boat.Instance.isOnStartZone && isOnStartGround && !isOnBoat && !Boat.Instance.IsBoatFull())
        {
            transform.position = boatPos + offset;
        }
        else if (isOnBoat && Boat.Instance.isOnStartZone)
        {
            transform.position = startPos;

        }
        else if (isOnBoat && Boat.Instance.isOnEndZone)
        {
            transform.position = -startPos;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        var ground = collision.gameObject.GetComponent<Ground>();
        var boat = collision.gameObject.GetComponent<Boat>();
        var startGround = collision.gameObject.CompareTag("Start");
        var endGround = collision.gameObject.CompareTag("End");

        if (ground)
        {
            isOnBoat = false;
            if (startGround)
            {
                isOnStartGround = true;
                isOnEndGround = false;
            }

            else if (endGround)
            {
                isOnEndGround = true;
                isOnStartGround = false;
            }
        }
        else if (boat)
        {
            isOnBoat = true;
        }

    }

    private void GetStartingPos()
    {
        startPos = transform.position;
    }

    private Vector3 GetAnimalOnBoatPosition()
    {
        return Boat.Instance.animals[0].transform.position;
    }


}
