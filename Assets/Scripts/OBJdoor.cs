using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OBJdoor : MonoBehaviour, INInteractable
{
    private bool IsOpen = false;

    public void FireInteraction()
    {
        if (IsOpen)
        {
            CloseDoor();
        }
        else
        {
            OpenDoor();
        }

        IsOpen = !IsOpen;
    }

    private void OpenDoor()
    {
        Debug.Log("The door is opened");
    }
    
    private void CloseDoor()
    {
        Debug.Log("The door is closed");
    }
}
