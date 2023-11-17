using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class EnvironmentInteraction : MonoBehaviour
{
    private GameObject player;

    private StarterAssetsInputs inputs;

    private bool inRange;

    public GameObject interactButton;

    public Item neededItem;

    private PlayerInventory _playerInventory;

    public Animator animator;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        _playerInventory = player.GetComponent<PlayerInventory>();

        inputs = player.GetComponent<StarterAssetsInputs>();
    }

    private void Update()
    {
        RangeCheck();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;

        interactButton.SetActive(true);
        inRange = true;
        foreach (Item item in _playerInventory.inventoryObjects)
        {
            if (item != neededItem) continue;

            item.usable = true;
            print("Needed item " + item.name + " being usable is " + item.usable);
        }
        player.GetComponent<PlayerInventory>().inInteractRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player") return;

        interactButton.SetActive(false);
        inRange = false;
        foreach (Item item in _playerInventory.inventoryObjects)
        {
            if (item != neededItem) continue;

            item.usable = false;
            print("Needed item " + item.name + " being usable is " + item.usable);
        }
        player.GetComponent<PlayerInventory>().inInteractRange = false;
        //outlineScript.enabled = false;
    }

    private void RangeCheck()
    {
        if (!inRange) return;


        if (inputs.interact)
        {
            inputs.interact = false;
            foreach (Item item in _playerInventory.inventoryObjects)
            {
                if (item != neededItem) continue;

                animator.Play("Test Door Open");
            }
        }
    }
}
