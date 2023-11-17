using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySubMenu : MonoBehaviour
{
    public Button useButton;
    public Button inspectButton;
    public Button dropButton;

    private bool canUse = false;
    private bool canInspect;
    private bool canDrop = false;

    private PlayerInventory _playerInventory;

    public string context;

    private void Start()
    {
        useButton.interactable = false;
        inspectButton.interactable = false;
        dropButton.interactable = false;

        _playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
    }
    private void Update()
    {
        SetUseButtonUsability();
        SetDropButtonUsability();
    }

    public void SetUseButtonUsability()
    {
        if (!canUse) return;

        useButton.interactable = true;
    }

    public void SetDropButtonUsability()
    {
        if (!canDrop) return;

        dropButton.interactable = true;
    }
}
