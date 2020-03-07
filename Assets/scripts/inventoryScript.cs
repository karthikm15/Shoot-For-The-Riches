using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inventoryScript : MonoBehaviour
{
    public int size;
    public GameObject[] slots;
    public slotScript[] slotscripts;
    public GameObject hand;
    public List<GameObject> guns;
    [HideInInspector] public int selected;

    private void Start()
    {
        slotscripts = new slotScript[slots.Length];
        for (int i = 0; i < slots.Length; i++)
            slotscripts[i] = slots[i].GetComponent<slotScript>();
        slotscripts[0].Highlight();
        guns[0].SetActive(true);
        
    }
    // Update is called once per frame
    void Update()
    {
        for (int i = 1; i <= 9; i++)
        {
            string s = i.ToString();
            if (Input.GetKeyDown(s))
            {
                Debug.Log(i - 1);
                foreach (GameObject gun in guns)
                {
                    gun.SetActive(false);
                    gun.GetComponent<Weapon>().text.gameObject.SetActive(false);
                }
                hand.SetActive(false);
                if (i <= guns.Count)
                {
                    guns[i - 1].GetComponent<Weapon>().text.gameObject.SetActive(true);
                    guns[i - 1].SetActive(true);
                    guns[i - 1].transform.position = hand.transform.position;
                }
                else
                {
                    hand.SetActive(true);
                }

                selected = i - 1; 

                foreach (slotScript ss in slotscripts)
                    ss.resetColor(); 
                slotscripts[i-1].Highlight();
                break;
            }
        }
    }
}
