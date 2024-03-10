using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumpGirlVista : MonoBehaviour
{
    public static bool isCutsceneOn;
    [SerializeField] GameObject OutroCutscene; 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isCutsceneOn = true; // need to freeze the player
            OutroCutscene.SetActive(true);
            Invoke(nameof(StopCutscene), 5f);
        }
    }

    void StopCutscene()
    {
        isCutsceneOn = false;
        Destroy(gameObject);
        
    }
}
