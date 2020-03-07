using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
[System.Serializable]
public class Weapon : MonoBehaviour
{
    private AudioSource source;
    public int maxCap;
    [HideInInspector] public int hand;
    public int ammo = 60;
    public float damage;
    public float reloadSpeed;
    public float shotIntervals;
    private float startShot;
    private float startReload;
    public Text text;
    private int counter;
    public GameObject muzzle;

    void Start()
    {
        counter = 0;
        source = GetComponent<AudioSource>();
        hand = maxCap;
        if (text != null)
            text.text = hand + "/" + ammo;
        startShot = Time.time;
        startReload = Time.time;
    }
    public bool fire()
    {
        muzzle.SetActive(true);
        if (hand > 0 && Time.time - startShot >= shotIntervals) 
        {
            if (maxCap >= 30)
            {
                if (counter++ >= 5)
                {
                    source.Play();
                    counter = 0;
                }
            }
            else source.Play();
            if (text != null)
                text.text = --hand + "/" + --ammo;
            startShot = Time.time;
            return true;
        }
        muzzle.SetActive(false);
        return false;
    }
    public bool reload()
    {
        if (ammo >= maxCap)
        {
            ammo -= maxCap - hand;
            hand = maxCap;
            if (text != null)
                text.text = hand + "/" + ammo;
            return true;
        }
        return false;
    }
    public void increaseMaxCap(int inc)
    {
        maxCap += inc;
    }
    public void addAmmo(int num)
    {
        ammo += num;
    }
}
