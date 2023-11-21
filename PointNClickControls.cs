using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using StarterAssets;
using UnityEngine.InputSystem;

public class PointNClickControls : MonoBehaviour
{
    public LayerMask _layerMask;

    private NavMeshAgent _agent;

    public StarterAssetsInputs _inputs;

    private Animator _animator;

    public float moveSpeed = 2f;
    public float rotateSpeed = 10f;

    public Transform bawl;

    public GameObject _playerInventory;

    public Transform destinationTarget;

    private void Start()
    {
        _playerInventory.SetActive(false);

        _agent = GetComponent<NavMeshAgent>();

        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(Vector3.Distance(_agent.destination, transform.position) <= 0.5f)
        {
            _animator.SetFloat("Speed", 0f);
        }
        else
        {
            _animator.SetFloat("Speed", 6f);
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if((_layerMask & (1 << hit.collider.gameObject.layer)) != 0)
            {
                //We hit a part of the floor
                Vector3 targetPoint = hit.point;

                bawl.position = targetPoint;

                if (_inputs.leftMouse)
                {
                    _inputs.leftMouse = false;
                    //Move the character to that new point
                    Vector3 moveDirection = targetPoint;
                    bawl.position = moveDirection;
                    destinationTarget.position = targetPoint;
                    _agent.destination = moveDirection;
                }
            }
        }
    }

    public void OnFootstep()
    {
        
    }
}
