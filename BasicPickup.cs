using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using StarterAssets;

public class BasicPickup : MonoBehaviour
{
    public Item itemType;

    private TextMeshProUGUI alertText;

    private Collider coll;

    public GameObject itemMesh;
    private GameObject alertCanvas;
    public GameObject interactButton;
    private GameObject player;

    [HideInInspector]
    public List<GameObject> inventoryPanels;

    private string itemName;

    [HideInInspector]
    public bool pickedUp;
    [HideInInspector]
    public bool inRange;

    public Outline outlineScript;

    private AudioSource _audioSource;

    public AudioClip pickupSound;

    private StarterAssetsInputs inputs;
    private void Start()
    {
        itemName = itemType.itemName.ToString();

        _audioSource = GetComponent<AudioSource>();

        alertCanvas = GameObject.FindGameObjectWithTag("AlertCanvas");
        alertText = alertCanvas.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        coll = gameObject.GetComponent<Collider>();


        foreach (GameObject gam in GameObject.FindGameObjectsWithTag("InventorySlot"))
        {
            inventoryPanels.Add(gam);
        }

        player = GameObject.FindGameObjectWithTag("Player");

        inputs = player.GetComponent<StarterAssetsInputs>();

        //alertCanvas.SetActive(false);
    }

    private void Update()
    {
        if (!inRange) return;

        if (inputs.interact)
        {
            inputs.interact = false;
            GrabItem();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;

        interactButton.SetActive(true);
        inRange = true;
        player.GetComponent<PlayerInventory>().inInteractRange = true;
        outlineScript.enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player") return;

        interactButton.SetActive(false);
        inRange = false;
        player.GetComponent<PlayerInventory>().inInteractRange = false;
        outlineScript.enabled = false;
    }

    public void GrabItem()
    {
        player.GetComponent<PlayerInventory>().inventoryObjects.Add(itemType);

        foreach (GameObject gam in inventoryPanels)
        {
            if (gam.GetComponent<UpdateMainInventoryUI>().itemIndex == player.GetComponent<PlayerInventory>().inventoryObjects.Count - 1)
            {
                gam.GetComponent<UpdateMainInventoryUI>().itemPanelImage.sprite = itemType.itemImage;
                gam.GetComponent<UpdateMainInventoryUI>().itemPanelImage.color = Color.white * 1;
            }
        }
        StartCoroutine(SetCanvas());
        PlayAudioThenDestroy();
    }

    IEnumerator SetCanvas()
    {
        alertCanvas.SetActive(true);
        alertText.text = itemType.itemName.ToString() + " has been aquired";
        Destroy(coll);

        yield return new WaitForSeconds(3);

        alertCanvas.SetActive(false);

        Destroy(gameObject);
    }

    private void PlayAudioThenDestroy()
    {
        _audioSource.clip = pickupSound;
        _audioSource.Play();

        interactButton.SetActive(false);

        Destroy(itemMesh);
        player.GetComponent<PlayerInventory>().inInteractRange = false;
        inRange = false;
    }
}
