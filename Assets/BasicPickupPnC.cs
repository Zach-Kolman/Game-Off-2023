using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using StarterAssets;
using UnityEngine.AI;

public class BasicPickupPnC : MonoBehaviour
{
    public Item itemType;

    private TextMeshProUGUI alertText;

    private Collider coll;

    public GameObject itemMesh;
    private GameObject alertCanvas;
    public GameObject interactButton;
    private GameObject player;

    public List<GameObject> inventoryPanels;

    private string itemName;

    public bool pickedUp;
    public bool inRange;

    public Outline outlineScript;

    private AudioSource _audioSource;

    public AudioClip pickupSound;

    private StarterAssetsInputs _inputs;

    private NavMeshAgent _agent;

    public LayerMask _itemLayerMask;

    private bool movingToPickup;

    public Transform bawl;
    private void Start()
    {
        outlineScript.enabled = false;

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

        _inputs = player.GetComponent<StarterAssetsInputs>();

        _agent = player.GetComponent<NavMeshAgent>();

        //alertCanvas.SetActive(false);
    }

    private void Update()
    {
        if (pickedUp) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (movingToPickup && IsInRange())
        {
            movingToPickup = false;
            GrabItem();
            return;
        }

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if ((_itemLayerMask & (1 << hit.collider.gameObject.layer)) != 0)
            {
                outlineScript.enabled = true;

                //We hit a part of the floor
                Vector3 targetPoint = hit.point;
                bawl.position = targetPoint;

                if (_inputs.leftMouse)
                {
                    _inputs.leftMouse = false;

                    if (!IsInRange())
                    {
                        //Move the character to that new point
                        Vector3 moveDirection = targetPoint;
                        _agent.destination = moveDirection;
                        movingToPickup = true;
                    }
                    else
                    {
                        movingToPickup = false;
                        GrabItem();
                        return;
                    }
                }
            }
            else
            {
                if (itemMesh == null) return; 
                outlineScript.enabled = false;
            }
        }
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

        yield return new WaitForSeconds(3);

        alertCanvas.SetActive(false);

        Destroy(gameObject);
    }

    private void PlayAudioThenDestroy()
    {
        _audioSource.clip = pickupSound;
        _audioSource.Play();

        pickedUp = true;
        Destroy(itemMesh);
    }

    private bool IsInRange()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance <= 1f) return true;

        return false;
    }
}
