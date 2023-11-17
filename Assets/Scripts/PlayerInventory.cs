using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class PlayerInventory : MonoBehaviour
{
    public List<Item> inventoryObjects;

    private StarterAssetsInputs inputs;

    public bool inInteractRange;

    private void Start()
    {
        inputs = GetComponent<StarterAssetsInputs>();
    }

    private void Update()
    {
        if (!inInteractRange) inputs.interact = false;
    }
}
