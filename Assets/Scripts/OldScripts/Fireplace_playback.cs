using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using STOP_MODE = FMOD.Studio.STOP_MODE;

public class Fireplace_playback : MonoBehaviour
{
    public EventInstance FireplaceInstance;
    public EventReference fireplaceEvent;
    private bool isPlaying = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isPlaying)
        {
            Debug.Log("setting parameter to 1");
            FireplaceInstance = RuntimeManager.CreateInstance(fireplaceEvent);
            FireplaceInstance.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject.transform));
            FireplaceInstance.start();
            isPlaying = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && FireplaceInstance.isValid())
        {
            FireplaceInstance.stop(STOP_MODE.ALLOWFADEOUT);
            FireplaceInstance.release();
            isPlaying = false;
        }
    }
}