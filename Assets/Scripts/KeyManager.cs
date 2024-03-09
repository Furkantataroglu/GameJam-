using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject door;
    public bool isPickedup;
    private Vector2 vel;
    public float smoothTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(isPickedup)
       {
        gameObject.SetActive(false);
    
       }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //trigger with player
        if(other.gameObject.CompareTag("Player") && !isPickedup)
        {
            isPickedup=true;
            door.GetComponent<DoorScript>().keyPickedUp = true;
        }
        
    }
}
