using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverComponent : MonoBehaviour, INInteractable
{
    public GameObject CachedGameObject = null;
    private bool IsActivated = false;

    public void FireInteraction()
    {
        IsActivated = !IsActivated;

        INInteractable interactable = CachedGameObject.GetComponent<INInteractable>();
        interactable?.FireInteraction();
    }
}
