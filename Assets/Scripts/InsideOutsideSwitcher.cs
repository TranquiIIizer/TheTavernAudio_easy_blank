using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;

public class InsideOutsideSwitcher : MonoBehaviour
{
    private FMOD.Studio.VCA outsideVCA;
    private FMOD.Studio.VCA insideVCA;
    float distanceToGround;
    private void Start()
    {
        distanceToGround = GetComponent<Collider>().bounds.extents.y;
        outsideVCA = FMODUnity.RuntimeManager.GetVCA("vca:/Outside");
        insideVCA = FMODUnity.RuntimeManager.GetVCA("vca:/Inside");
        insideVCA.setVolume(1f);
        StartCoroutine(CheckIfOutside());
    }

    IEnumerator CheckIfOutside()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            RaycastHit hit;
            
            if (Physics.Raycast(transform.position, Vector3.down, out hit, distanceToGround + 0.5f))
            {
                if (hit.collider.CompareTag("Outside"))
                {
                    Debug.Log("Outside");
                    outsideVCA.setVolume(0.25f);
                    insideVCA.setVolume(1f);
                }
                else
                {
                    Debug.Log("Inside");
                    outsideVCA.setVolume(1f);
                    insideVCA.setVolume(0f);
                }
            }
        }
    }
}
