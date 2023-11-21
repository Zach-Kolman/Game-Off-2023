using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnvironmentInteractionPnC : MonoBehaviour
{
    private GameObject player;

    private StarterAssetsInputs _inputs;

    private bool inRange;

    private bool movingToInteract;

    private bool isOpened;

    public Item neededItem;

    private PlayerInventory _playerInventory;

    public LayerMask _usableLayerMask;

    public List<Outline> _outlineScript;

    public Transform debugSphere;

    public NavMeshAgent _agent;

    public UnityEvent eventToExecute;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        _playerInventory = player.GetComponent<PlayerInventory>();

        _inputs = player.GetComponent<StarterAssetsInputs>();
    }

    private void Update()
    {
        if (isOpened) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (movingToInteract && IsInRange())
        {
            movingToInteract = false;

            //do thing +++++++++++++++++++++++++++++++++++++++++++++++++++++
            ExecuteEvent();


            return;
        }

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if ((_usableLayerMask & (1 << hit.collider.gameObject.layer)) != 0)
            {
                foreach(Outline outline in _outlineScript)
                {
                    outline.enabled = true;
                }

                //We hit a part of the floor
                Vector3 targetPoint = hit.point;
                debugSphere.position = targetPoint;

                if (_inputs.leftMouse)
                {
                    _inputs.leftMouse = false;

                    if (!IsInRange())
                    {
                        //Move the character to that new point
                        Vector3 moveDirection = targetPoint;
                        _agent.destination = moveDirection;
                        movingToInteract = true;
                    }
                    else
                    {
                        movingToInteract = false;

                        //do thing +++++++++++++++++++++++++++++++++++++++++
                        ExecuteEvent();

                        return;
                    }
                }
            }
            else
            {
                if (isOpened) return;
                foreach (Outline outline in _outlineScript)
                {
                    outline.enabled = false;
                }
            }
        }
    }

    //private void RangeCheck()
    //{
    //    if (!inRange) return;


    //    if (inputs.interact)
    //    {
    //        inputs.interact = false;
    //        foreach (Item item in _playerInventory.inventoryObjects)
    //        {
    //            if (item != neededItem) continue;

    //            animator.Play("Test Door Open");
    //        }
    //    }
    //}

    private bool IsInRange()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance <= 1f) return true;

        return false;
    }

    public void ExecuteEvent()
    {
        foreach (Item item in _playerInventory.inventoryObjects)
        {
            if (item != neededItem) continue;

            item.usable = true;
            print("Needed item " + item.name + " being usable is " + item.usable);

            eventToExecute.Invoke();
        }
    }
}
