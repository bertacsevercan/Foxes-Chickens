using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Animal : MonoBehaviour
{
    private AudioSource audioSource;
    private Animator animator;

    private Vector3 startPos;

    public Transform boatPos;
    public Transform groundPos;

    public bool isOnGround;
    public bool isOnBoat;
    public bool isOnStartZone;
    public bool isOnEndZone;

    public static Animal Instance { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        isOnStartZone = true;
        isOnEndZone = false;
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        getStartingPos();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Talk(AudioClip audio)
    {

        audioSource.PlayOneShot(audio, 1.0f);
    }

    public void Die()
    {
        animator.SetBool("death", true);
    }

    protected abstract void Interact();

    public virtual void GoTo()
    {
        Vector3 offset = new Vector3(4, 3, 1);
        if (isOnGround)
            gameObject.transform.position = boatPos.position + offset;
        else if (isOnBoat && isOnStartZone)
            gameObject.transform.position = startPos;
        else if (isOnBoat && isOnEndZone)
            gameObject.transform.position = -startPos;

    }

    private void OnCollisionEnter(Collision collision)
    {
        var ground = collision.gameObject.GetComponent<Ground>();
        var boat = collision.gameObject.GetComponent<Boat>();

        if (ground)
        {
            isOnGround = true;
            isOnBoat = false;
        }
        else if (boat)
        {
            isOnBoat = true;
            isOnGround = false;
        }

    }

    void getStartingPos()
    {
        startPos = gameObject.transform.position;
    }


}
