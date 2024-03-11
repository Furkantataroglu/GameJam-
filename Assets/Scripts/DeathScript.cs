using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScript : MonoBehaviour
{
  
    [SerializeField]public GameObject targetObject;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Obstacle"))
        {
            Die();
        }
    }
    void Die()
    {
        Respawn();
    }
    void Respawn()
    {
       transform.position = targetObject.transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
