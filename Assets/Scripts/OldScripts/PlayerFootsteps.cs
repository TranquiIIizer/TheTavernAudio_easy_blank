using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class PlayerFootsteps : MonoBehaviour
{
    private EventInstance footstepsInstance;
    private EventInstance jumpInstance;

    [SerializeField] private float raycastDistance;
    [SerializeField] private string footstepsParameterName;
    [SerializeField] private string footstepsStone;
    [SerializeField] private string footstepsWood;

    [field: SerializeField] private EventReference footstepsEvent;
    [field: SerializeField] private EventReference jumpEvent;

    private void Start()
    {
        footstepsInstance = RuntimeManager.CreateInstance(footstepsEvent);
        footstepsInstance.set3DAttributes(RuntimeUtils.To3DAttributes(transform.position));

        jumpInstance = RuntimeManager.CreateInstance(jumpEvent);

    }

    private void Update()
    {
        PlayFootsteps();
    }

    private void PlayFootsteps()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, raycastDistance))
        {
            Debug.Log(hit.collider.tag);
        }
    }
}
