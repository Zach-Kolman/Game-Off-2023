using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamSwitcher : MonoBehaviour
{
    public CinemachineVirtualCamera _camera;

    public List<GameObject> vcams;

    public int basePriority = 10;

    private void Start()
    {
        foreach(GameObject cam in GameObject.FindGameObjectsWithTag("SwitchingCamera"))
        {
            vcams.Add(cam);
        }
    }

    public void SwitchCamera()
    {
        foreach (GameObject cam in GameObject.FindGameObjectsWithTag("SwitchingCamera"))
        {
            cam.GetComponent<CinemachineVirtualCamera>().Priority = basePriority;
        }

        _camera.Priority = basePriority + 6;
    }
}