using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class VistaCollide : MonoBehaviour
{


    [SerializeField] Rigidbody2D vistaRb;
    [SerializeField] CinemachineVirtualCamera cinemachine;
    [SerializeField] Transform lookAt;

    public Animator camAnim;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (vistaRb.tag == "Vista")
        {
            camAnim.SetBool("cutscene1", true);
            Invoke(nameof(StopCutscene), 3f);
        }
    }

    void StopCutscene()
    {
        camAnim.SetBool("cutscene1", false);
    }

}
