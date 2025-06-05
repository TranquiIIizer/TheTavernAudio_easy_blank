using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class Footsteps : MonoBehaviour
{
    FMOD.Studio.EventInstance FootstepsSound;
    FMOD.Studio.EventInstance JumpSound;
    FMOD.Studio.EventInstance LandSound;

    public EventReference footstepsEvent;
    public EventReference jumpEvent;
    public EventReference landEvent;

    private float lastFootstepTime = 0f;
    private float distToGround;
    private bool isGrounded = true;
    private bool isJumping = false;
    
    private void Start()
    {
        distToGround = GetComponent<Collider>().bounds.extents.y;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(IsGrounded());
            PlayJump();
        }        
    }
    void FixedUpdate()
    {
        // Footsteps
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            if (IsGrounded() && Time.time - lastFootstepTime > 0.5f)
            {
                lastFootstepTime = Time.time;
                PlayFootsteps();
            }
        }         
        
        // Running
        if((Input.GetKey(KeyCode.LeftShift) && Input.GetAxisRaw("Horizontal") != 0) || (Input.GetKey(KeyCode.LeftShift) && Input.GetAxisRaw("Vertical") != 0))
        {
            if (IsGrounded() && Time.time - lastFootstepTime > 0.25f)
            {
                lastFootstepTime = Time.time;
                PlayFootsteps();
            }
        }
    }

    void PlayFootsteps()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, distToGround + 0.5f))
        {
            if (hit.collider.CompareTag("Stone"))
            {
                FootstepsSound = FMODUnity.RuntimeManager.CreateInstance(footstepsEvent);
                FootstepsSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
                FootstepsSound.setParameterByNameWithLabel("footSwitcher", "stone");
                FootstepsSound.start();
                FootstepsSound.release();
                Debug.Log("Stone");
            }
            else if (hit.collider.CompareTag("Wood"))
            {
                FootstepsSound = FMODUnity.RuntimeManager.CreateInstance(footstepsEvent);
                FootstepsSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
                FootstepsSound.setParameterByNameWithLabel("footSwitcher", "wood");
                FootstepsSound.start();
                FootstepsSound.release();
                Debug.Log("Wood");
            }
            else if (hit.collider.CompareTag("Outside"))
            {
                FootstepsSound = FMODUnity.RuntimeManager.CreateInstance(footstepsEvent);
                FootstepsSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
                FootstepsSound.setParameterByNameWithLabel("footSwitcher", "stone");
                FootstepsSound.start();
                FootstepsSound.release();
            }
            else if (hit.collider.CompareTag("Inside_stone"))
            {
                FootstepsSound = FMODUnity.RuntimeManager.CreateInstance(footstepsEvent);
                FootstepsSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
                FootstepsSound.setParameterByNameWithLabel("footSwitcher", "stone");
                FootstepsSound.start();
                FootstepsSound.release();
            }
            else if (hit.collider.CompareTag("Inside_wood"))
            {
                FootstepsSound = FMODUnity.RuntimeManager.CreateInstance(footstepsEvent);
                FootstepsSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
                FootstepsSound.setParameterByNameWithLabel("footSwitcher", "wood");
                FootstepsSound.start();
                FootstepsSound.release();
            }
            else
            {
                FootstepsSound = FMODUnity.RuntimeManager.CreateInstance(footstepsEvent);
                FootstepsSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject.transform));
                FootstepsSound.setParameterByNameWithLabel("footSwitcher", "stone");
                FootstepsSound.start();
                FootstepsSound.release();
            }
        }       
    }

    private void PlayJump()
    {
        //if (!JumpSound.isValid())
        if(IsGrounded())
        {
            JumpSound = FMODUnity.RuntimeManager.CreateInstance(jumpEvent); // "event:/Footsteps"
 
            if (IsGrounded())
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, Vector3.down, out hit, distToGround + 0.5f))
                {
                    //Debug.Log(hit.collider.tag);
                    if (hit.collider.CompareTag("Stone"))
                    {
                        JumpSound.setParameterByNameWithLabel("footSwitcher", "stone");
                        JumpSound.start();
                    }
                    else if (hit.collider.CompareTag("Wood"))
                    {
                        JumpSound.setParameterByNameWithLabel("footSwitcher", "wood");
                        JumpSound.start();
                    }
                    else if (hit.collider.CompareTag("Inside_stone"))
                    {
                        LandSound.setParameterByNameWithLabel("footSwitcher", "stone");
                        JumpSound.start();
                    }
                    else if (hit.collider.CompareTag("Inside_wood"))
                    {
                        LandSound.setParameterByNameWithLabel("footSwitcher", "wood");
                        JumpSound.start();
                    }
                    else if (hit.collider.CompareTag("Bed"))
                    {
                        JumpSound.setParameterByNameWithLabel("footSwitcher", "bed");
                        JumpSound.start();
                    }
                    else
                    {
                        JumpSound.setParameterByNameWithLabel("footSwitcher", "stone");
                        JumpSound.start();
                    }
                }
            }

            JumpSound.release();
            isGrounded = false;
            isJumping = true;
        }
    }

    // Play landing sound
    private void OnCollisionEnter(Collision col)
    {
        if (IsGrounded() && isGrounded == false)
        {
            PlayLanding();
        }
    }

    private void PlayLanding()
    {
        if (IsGrounded() && isGrounded == false)
        {
            if (isJumping == true)
            {
                LandSound = FMODUnity.RuntimeManager.CreateInstance(landEvent);
                RaycastHit hit;

                if (Physics.Raycast(transform.position, Vector3.down, out hit, distToGround + 0.5f))
                {
                    //Debug.Log("Hit object tag: " + hit.collider.tag);
                    if (hit.collider.CompareTag("Stone"))
                    {
                        LandSound.setParameterByNameWithLabel("footSwitcher", "stone");
                        LandSound.start();
                    }
                    else if (hit.collider.CompareTag("Wood"))
                    {
                        LandSound.setParameterByNameWithLabel("footSwitcher", "wood");
                        LandSound.start();
                    }
                    else if (hit.collider.CompareTag("Inside_stone"))
                    {
                        LandSound.setParameterByNameWithLabel("footSwitcher", "stone");
                        LandSound.start();
                    }
                    else if (hit.collider.CompareTag("Inside_wood"))
                    {
                        LandSound.setParameterByNameWithLabel("footSwitcher", "wood");
                        LandSound.start();
                    }
                    else if (hit.collider.CompareTag("Bed"))
                    {
                        LandSound.setParameterByNameWithLabel("footSwitcher", "bed");
                        LandSound.start();
                    }
                    else
                    {
                        LandSound.setParameterByNameWithLabel("footSwitcher", "stone");
                        LandSound.start();
                    }
                }

                LandSound.release();
                isGrounded = true;
                isJumping = false;                
            }           
        }
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, distToGround + 0.5f);
    }  
}
