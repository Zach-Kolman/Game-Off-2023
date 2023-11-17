using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardIcon : MonoBehaviour
{
    private Transform target;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0);
    }

    private void Update()
    {
        transform.LookAt(target);
    }
}
