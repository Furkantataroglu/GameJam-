using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalScene : MonoBehaviour
{
    [SerializeField] GameObject square1;
    [SerializeField] GameObject square2;
    [SerializeField] GameObject timeLine;
    public static bool isCutsceneOn;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isCutsceneOn = true; 
            timeLine.SetActive(true);
        }
    }
}
