using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    // this class should be a singleton and static
    public static Boat Instance { get; set; }

    public bool isOnStartZone;
    public bool isOnEndZone;
    public bool isMoving;
    public bool isStartKeyPressed;

    public int animalCount = 0;

    public Transform startZone;
    public Transform endZone;

    public List<Animal> animals;

    public AudioClip audioClip;

    private AudioSource audioSource;

    private readonly int maxAnimalOnBoat = 2;

    private readonly float speed = 15.0f;


    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null)
        {
            Destroy(Instance);
            return;
        }
        Instance = this;
        audioSource = GetComponent<AudioSource>();
        isMoving = false;
        isOnStartZone = true;
        isOnEndZone = false;
        isStartKeyPressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsBoatReady() && isStartKeyPressed)
        {
            PlayAudio();
            isMoving = true;
            isStartKeyPressed = false;
            GameManager.Instance.UpdateScore(1);
        }

        if (!GameManager.Instance.isGameOver)
        {
            Move();
        }

        if (isMoving)
        {
            StartZone.Instance.Fail();
            EndZone.Instance.Fail();
            EndZone.Instance.Win();
        }
    }

    private void Move()
    {
        if (isMoving && isOnStartZone)
            transform.Translate(speed * Time.deltaTime * Vector3.forward);

        else if (isMoving && isOnEndZone)
            transform.Translate(speed * Time.deltaTime * Vector3.back);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Start Zone")
        {
            isOnStartZone = true;
            isOnEndZone = false;
        }
        else if (other.name == "End Zone")
        {
            isOnStartZone = false;
            isOnEndZone = true;
        }

        isMoving = false;

        animals.ForEach(GetOff);

        StartCoroutine(CheckGame());
    }

    IEnumerator CheckGame()
    {
        yield return new WaitForSeconds(0.1f);

        StartZone.Instance.Fail();
        EndZone.Instance.Fail();
        EndZone.Instance.Win();

    }

    private void OnCollisionEnter(Collision collision)
    {
        var animal = collision.collider.GetComponentInParent<Animal>();

        if (animal)
        {
            animal.transform.SetParent(transform);
            animalCount++;
            animals.Add(animal);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        var animal = collision.collider.GetComponentInParent<Animal>();

        if (animal)
        {
            animal.transform.SetParent(null);
            animalCount--;
            animals.Remove(animal);
        }
    }

    public bool IsBoatFull()
    {
        if (animalCount == maxAnimalOnBoat)
            return true;
        return false;
    }

    public bool IsBoatReady()
    {
        if (animalCount > 0 && !isMoving)
            return true;
        return false;
    }

    private void GetOff(Animal animal)
    {
        animal.GoTo();
    }

    private void PlayAudio()
    {
        audioSource.PlayOneShot(audioClip, 1.0f);
    }
}
