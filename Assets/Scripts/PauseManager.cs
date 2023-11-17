using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using StarterAssets;

public class PauseManager : MonoBehaviour
{
    private GameObject player;

    public List<GameObject> enemies;

    [SerializeReference]
    public bool paused;

    public bool pauseStarted;

    private CharacterController controller;

    public GameObject characterMesh;

    public GameObject inventoryCanvas;

    private static PauseManager pauseManager;

    public StarterAssetsInputs inputs;

    public bool isPnC;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (pauseManager == null)
        {
            pauseManager = gameObject.GetComponent<PauseManager>();
        }
        else
        {
            Destroy(gameObject);
        }

        if (isPnC)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //inventoryCanvas = GameObject.FindGameObjectWithTag("InventoryCanvas");
        controller = player.GetComponent<CharacterController>();

        pauseStarted = false;

        foreach (GameObject enemy in enemies)
        {
            if (enemy == null) enemies.Remove(enemy);
        }
    }

    private void FixedUpdate()
    {
        if (inputs.pause)
        {
            if (!pauseStarted)
            {
                if (!paused)
                {
                    paused = true;
                    pauseStarted = true;
                    inputs.pause = false;
                    StartCoroutine(PauseStuff());
                }
                else
                {
                    paused = false;
                    pauseStarted = true;
                    inputs.pause = false;
                    StartCoroutine(UnpauseStuff());
                }
            }
            
        }
    }

    //private void PauseEnemies()
    //{
    //    pauseStarted = true;

    //    foreach (GameObject enemy in enemies)
    //    {
    //        if (enemy == null) enemies.Remove(enemy);
    //    }

    //    foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
    //    {
    //        enemies.Add(enemy);
    //    }

    //    foreach (GameObject enemy in enemies)
    //    {
    //        if (enemy.GetComponent<EnemyStates>().isDead) return;

    //        enemy.GetComponent<NavMeshAgent>().enabled = false;
    //        enemy.GetComponent<EnemyStates>().enabled = false;
    //        enemy.GetComponent<Animator>().enabled = false;
    //    }
    //}

    //private void UnpauseEnemies()
    //{
    //    pauseStarted = false;


    //    foreach (GameObject enemy in enemies)
    //    {
    //        if (enemy == null)
    //        {
    //            enemies.Remove(enemy);
    //            return;
    //        }

    //        if (enemy.GetComponent<EnemyStates>().isDead) return;

    //        enemy.GetComponent<NavMeshAgent>().enabled = true;
    //        enemy.GetComponent<EnemyStates>().enabled = true;
    //        enemy.GetComponent<Animator>().enabled = true;
    //    }

    //    foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
    //    {
    //        enemies.Remove(enemy);
    //    }
    //}

    private void PausePlayer()
    {
        //foreach (GameObject gam in GameObject.FindGameObjectsWithTag("InventorySlot"))
        //{
        //    gam.GetComponent<UpdateMainInventoryUI>().HideSubMenu();
        //}

        inventoryCanvas.SetActive(true);

        if (!isPnC)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            controller.enabled = false;
        }

        //characterMesh.SetActive(false);
    }

    private void UnpausePlayer()
    {
        //foreach (GameObject gam in GameObject.FindGameObjectsWithTag("InventorySlot"))
        //{
        //    gam.GetComponent<UpdateMainInventoryUI>().HideSubMenu();
        //}

        inventoryCanvas.SetActive(false);

        if (!isPnC)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            controller.enabled = true;
        }

        //characterMesh.SetActive(true);
    }

    IEnumerator PauseStuff()
    {
        yield return new WaitForEndOfFrame();

        //PauseEnemies();
        PausePlayer();
        pauseStarted = false;
    }

    IEnumerator UnpauseStuff()
    {
        yield return new WaitForEndOfFrame();

        //run the unpause method
        //UnpauseEnemies();
        UnpausePlayer();
        pauseStarted = false;
    }

    public void ManualUnpause()
    {
        StartCoroutine(UnpauseStuff());
    }
}