using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class robotSpawner : MonoBehaviour
{
    public GameObject[] possGuns;
    private GameObject[] spawners;
    public GameObject robot;
    private int wave = 0;
    private int spawns = 1;
    private int spawned = 0;
    public bool cheat;
    public GameObject inventoryCanvas;
    private inventoryScript inventory;
    private GameObject[] robots;
    public Text waveCounter;
    public Text moneyCounter;
    private float money = 0f;
    private bool first = true;
    public RawImage[] pngs;
    private void Start()
    {
        if (!cheat)
        {
            pngs[0].gameObject.SetActive(true);
            pngs[1].gameObject.SetActive(false);
            pngs[2].gameObject.SetActive(false);
        }
        else
        {
            foreach (RawImage img in pngs)
                img.gameObject.SetActive(true);
        }
        inventory = inventoryCanvas.GetComponent<inventoryScript>();
        if (cheat)
            for (int gun = 1; gun < possGuns.Length; gun++)
                inventory.guns.Add(possGuns[gun]);
    }
    void Update()
    {
        spawners = GameObject.FindGameObjectsWithTag("barrel");
        robots = GameObject.FindGameObjectsWithTag("enemy");
        if (robots.Length == 0)
            Spawn();
    }
    private void Spawn()
    {
        if (!first)
            money += 0.02f;
        else
            first = false;
        moneyCounter.text = "$" + money;
        if (++spawned >= spawns)
        {
            ++wave;
            waveCounter.text = "Wave " + wave;
            Debug.Log("Wave: " + wave);
            NextWave();
        }
        foreach (GameObject spawner in spawners)
        {
            GameObject s = Instantiate(robot, spawner.transform.position, robot.transform.rotation);
            System.Random random = new System.Random();
            s.GetComponent<enemyScript>().speed = (float) (random.NextDouble() + 3);
            s.SetActive(true);
        }
    }
    private void NextWave()
    {
        if (wave != 1)
        {
            //SceneManager.LoadScene(0);
            //Cursor.lockState = CursorLockMode.None;
            spawns++;
        }
        spawned = 0;
        if (wave == 2 && !cheat)
        {
            inventory.guns.Add(possGuns[1]);
            pngs[1].gameObject.SetActive(true);
        }
        else if (wave == 3 && !cheat)
        {
            inventory.guns.Add(possGuns[2]);
            pngs[2].gameObject.SetActive(true);
        }
    }
    public void changeTargets(GameObject newTarget)
    {
        enemyScript.target = newTarget;
    }
    public void changeBackTargets()
    {
        enemyScript.target = GameObject.FindGameObjectWithTag("Player");
    }
}
