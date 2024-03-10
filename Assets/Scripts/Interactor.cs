using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    private INInteractable _cachedGameObject;
    public KeyCode activationKey = KeyCode.F;
    public float activationDistance = 2f;
    public LayerMask raycastLayerMask; 

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(activationKey))
        {
            float direction = Mathf.Clamp(transform.localScale.x, -1, 1);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right * direction, activationDistance, raycastLayerMask);
            Debug.DrawRay(transform.position, transform.right * direction * activationDistance, Color.red, 5);
            
            if (hit)
            {
                Debug.Log("Ray hit: " + hit.collider.gameObject.name);
                if (hit.collider.CompareTag("Interactable"))
                {
                    _cachedGameObject = hit.collider.gameObject.GetComponent<INInteractable>();
                    _cachedGameObject.FireInteraction();
                }
            }
        }
    }
}
