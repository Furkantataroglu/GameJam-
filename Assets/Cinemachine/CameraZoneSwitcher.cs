using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Diagnostics;

public class CameraZoneSwitcher : MonoBehaviour
{
    public string triggerTag;
    public CinemachineVirtualCamera primarycamera;
    public CinemachineVirtualCamera [] virtualCameras;
    // Start is called before the first frame update
    void Start()
    {
        SwitchToCamera(primarycamera);
    }
     void OnTriggerEnter2D(Collider2D other)
    {

        if(other.CompareTag(triggerTag))
        {
            CinemachineVirtualCamera targetCamera = other.GetComponentInChildren<CinemachineVirtualCamera>();
            SwitchToCamera(targetCamera);
        }
    }
     void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag(triggerTag))
        {
            SwitchToCamera(primarycamera);
        }
    }
    private void SwitchToCamera(CinemachineVirtualCamera targetCamera)
    {
        foreach ( CinemachineVirtualCamera camera in virtualCameras)
        {
            camera.enabled = camera ==targetCamera;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
