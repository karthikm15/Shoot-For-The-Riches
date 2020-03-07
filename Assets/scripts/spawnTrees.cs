using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnTrees : MonoBehaviour
{
    public GameObject plane;
    public GameObject obj;
    public int min, max;
    // Start is called before the first frame update
    void Start()
    {
        System.Random rand = new System.Random();
        int randInt = rand.Next(min, max);
        for (int i = 0; i < randInt; i ++)
        {
            Instantiate(obj, new Vector3(rand.Next((int)plane.transform.position.x - 27, (int)plane.transform.position.x + 27),
            obj.transform.position.y, rand.Next((int)plane.transform.position.z - 27, (int)plane.transform.position.z + 27)), obj.transform.rotation);
        }
    }
}
