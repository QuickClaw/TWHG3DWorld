using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLight : MonoBehaviour
{
    public bool stopped;
    public Follow Follow;

    void Start()
    {

    }

    void Update()
    {
        if (stopped)
            Follow.enabled = false;
        else
            Follow.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag is "PlayerLight")
        {
            stopped = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag is "PlayerLight")
        {
            stopped = false;
        }
    }
}
