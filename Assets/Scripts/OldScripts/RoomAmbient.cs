using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomAmbient : MonoBehaviour
{
    public bool ambientActivated;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            ambientActivated = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            ambientActivated = false;
    }
}
