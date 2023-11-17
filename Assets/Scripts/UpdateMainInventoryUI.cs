using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UpdateMainInventoryUI : MonoBehaviour, IDragHandler
{
    public delegate void UsedAction();
    public static event UsedAction onUsed;

    [HideInInspector]
    //the image attached to this game object
    public Image itemPanelImage;

    //the image the panel displays
    public Image mainPanelImage;

    //the descriptive text the panel displays
    public TextMeshProUGUI mainPanelText;

    //the name text the panel displays
    public TextMeshProUGUI mainPanelName;

    public int itemIndex;
    private Item slottedItem;
    private GameObject player;
    private GameObject subMenu;
    public Button inspectButton;
    public Button useButton;
    public Button dropButton;
    private Transform itemPrefab;
    public GameObject inspectWindow;
    public Camera inspectCamera;
    public RenderTexture renderTexture;
    public bool used;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        itemPanelImage = transform.GetChild(1).GetComponent<Image>();

        subMenu = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        if (itemIndex > player.GetComponent<PlayerInventory>().inventoryObjects.Count - 1) return;
        inspectButton.interactable = player.GetComponent<PlayerInventory>().inventoryObjects[itemIndex].inspectable;
        useButton.interactable = player.GetComponent<PlayerInventory>().inventoryObjects[itemIndex].usable;
        dropButton.interactable = player.GetComponent<PlayerInventory>().inventoryObjects[itemIndex].droppable;
    }

    public void SetMainPanelInfo()
    {
        if (itemIndex > player.GetComponent<PlayerInventory>().inventoryObjects.Count - 1) return;
        mainPanelImage.color = Color.white;
        mainPanelImage.sprite = player.GetComponent<PlayerInventory>().inventoryObjects[itemIndex].itemImage;
        mainPanelText.text = player.GetComponent<PlayerInventory>().inventoryObjects[itemIndex].description;
        mainPanelName.text = player.GetComponent<PlayerInventory>().inventoryObjects[itemIndex].itemName;
    }

    public void UnsetMainPanelInfo()
    {
        mainPanelImage.color = Color.clear;
        mainPanelText.text = "";
        mainPanelName.text = "";
    }

    public void ShowSubMenu()
    {
        if (itemIndex > player.GetComponent<PlayerInventory>().inventoryObjects.Count - 1) return;

        subMenu.SetActive(true);

        subMenu.GetComponent<Animator>().Play("Inventory_Submenu_Open");
    }

    public void HideSubMenu()
    {
        if (itemIndex > player.GetComponent<PlayerInventory>().inventoryObjects.Count - 1) return;

        StartCoroutine(CloseSubMenu());
    }

    private IEnumerator CloseSubMenu()
    {
        subMenu.GetComponent<Animator>().Play("Inventory_Submenu_Close");

        yield return new WaitForSeconds(0.167f);

        subMenu.SetActive(false);
    }

    public void Inspect()
    {
        //if(player.GetComponent<PlayerInventory>().inventoryObjects[itemIndex].inspectable)
        //{
        //    print("Inspecting dat shit");

        //    itemPrefab = Instantiate(player.GetComponent<PlayerInventory>().inventoryObjects[itemIndex].prefabModel, new Vector3(1000, 1000, 1000), Quaternion.identity);

        //    inspectWindow.SetActive(true);

        //    itemPrefab.gameObject.layer = LayerMask.NameToLayer("InspectLayer");

        //    inspectCamera.clearFlags = CameraClearFlags.Depth;
        //}
    }

    public void Use()
    {
        if (!player.GetComponent<PlayerInventory>().inventoryObjects[itemIndex].usable) return;

        if (onUsed != null)
            onUsed();

        switch (player.GetComponent<PlayerInventory>().inventoryObjects[itemIndex].itemName)
        {
            case "Handgun Ammo":
                HideSubMenu();
                UnsetMainPanelInfo();
                print("Dropped dat shit");
                player.GetComponent<PlayerInventory>().inventoryObjects.Remove(player.GetComponent<PlayerInventory>().inventoryObjects[itemIndex]);
                itemPanelImage.sprite = null;
                break;

        }


    }

    public void Drop()
    {
        if (player.GetComponent<PlayerInventory>().inventoryObjects[itemIndex].droppable)
        {
            HideSubMenu();
            UnsetMainPanelInfo();
            print("Dropped dat shit");
            player.GetComponent<PlayerInventory>().inventoryObjects.Remove(player.GetComponent<PlayerInventory>().inventoryObjects[itemIndex]);
            itemPanelImage.sprite = null;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        itemPrefab.eulerAngles += new Vector3(eventData.delta.y, -eventData.delta.x);
    }


}
