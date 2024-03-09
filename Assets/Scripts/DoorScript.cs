using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{

    // Start is called before the first frame update
    public bool locked;
    public bool keyPickedUp;
    [SerializeField]
    private Collider2D doorCollider;
    void Start()
    {
        locked = true;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D (Collider2D other)
    {
        if(other.gameObject.CompareTag("Player") && keyPickedUp)
        {
            locked = false;
            doorCollider.enabled = false;

        }
    }
    
}
