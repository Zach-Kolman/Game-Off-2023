using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
    public UnityEvent onTriggerEnter;
    public UnityEvent onTriggerExit;

    public GameObject parent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;

        print("ahhhhhh");
        onTriggerEnter.Invoke();
        //parent.GetComponent<CamSwitcher>().SwitchCamera();
    }

    private void OnTriggerExit(Collider other)
    {
        onTriggerExit.Invoke();
    }
}
