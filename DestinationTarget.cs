using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationTarget : MonoBehaviour
{
    private GameObject _player;

    private Renderer _renderer;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");

        _renderer = GetComponent<Renderer>();
    }
    public void CheckRange()
    {
        float distance = Vector3.Distance(_player.transform.position, transform.position);

        if (distance <= 0.65f)
        {
            _renderer.enabled = false;
            return;
        }

        _renderer.enabled = true;
    }

    private void Update()
    {
        print(Vector3.Distance(_player.transform.position, transform.position));

        CheckRange();
    }
}
