using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;

public class InsideOutsideSwitcher : MonoBehaviour
{
    private FMOD.Studio.VCA outsideVCA;
    float distanceToGround;
    private void Start()
    {
        distanceToGround = GetComponent<Collider>().bounds.extents.y;
        outsideVCA = FMODUnity.RuntimeManager.GetVCA("vca:/Outside");
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
                }
                else
                {
                    Debug.Log("Inside");
                    outsideVCA.setVolume(1f);
                }
            }
        }
    }
}
