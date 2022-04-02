using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // PARAMETERS - for tuning, typically set in the editor

    // CACHE - e.g. references for readability

    // STATE - private instance (member) variables
    
    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float mainRotation = 200f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem engineParticles;
    [SerializeField] ParticleSystem rightSideParticles;
    [SerializeField] ParticleSystem leftSideParticles;

    Rigidbody rb;
    AudioSource myAudio;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        myAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }

    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotation();
        }
    }

    void StopRotation()
    {
        rightSideParticles.Stop();
        leftSideParticles.Stop();
    }

    void RotateRight()
    {
        ApplyRotation(-mainRotation);
        if (!leftSideParticles.isPlaying)
        {
            leftSideParticles.Play();
        }
    }

    void RotateLeft()
    {
        ApplyRotation(mainRotation);
        if (!rightSideParticles.isPlaying)
        {
            rightSideParticles.Play();
        }
    }

    void StopThrusting()
    {
        myAudio.Stop();
        engineParticles.Stop();
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!myAudio.isPlaying)
        {
            myAudio.PlayOneShot(mainEngine);
        }
        if (!engineParticles.isPlaying)
        {
            engineParticles.Play();
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // unfreezing rotation so physics can take over
    }
}