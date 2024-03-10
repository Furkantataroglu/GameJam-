using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;

public class StartCutscene : MonoBehaviour
{
    public static bool isCutsceneOn;
    [SerializeField] Animator camAnim;  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isCutsceneOn = true; // need to freeze the player
            camAnim.SetBool("cutscene1", true);
            Invoke(nameof(StopCutscene), 3f);
        }
    }

    void StopCutscene()
    {
        isCutsceneOn = false;
        camAnim.SetBool("cutscene1", false);
        Destroy(gameObject);
    }
}

