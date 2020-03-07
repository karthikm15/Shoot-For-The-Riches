using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startCar : MonoBehaviour
{
    public void startthecar() 
    {
        Debug.Log("Starting car");
        GetComponent<CapsuleCollider>().enabled = true;
    }
    public void stopcar()
    {
        GetComponent<car>().enabled = false;
    }
}
