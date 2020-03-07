using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class car : MonoBehaviour
{
    public float speed;
    public float acceleration;
    public float turnspeed;
    //public float rotateSpeed;
    public float maxSpeed;
    public Camera[] cameras = new Camera[2];
    public GameObject[] wheels = new GameObject[4];
    private int cameraInd = 0;
    private Quaternion target;
    private int pos = 0;
    public GameObject player;
    [HideInInspector] public bool boarded;
    public GameObject spawner;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("enemy"))
        {
            collision.collider.GetComponent<enemyScript>().takeDamage(8);
        }
    }

    void Start()
    {
        for (int i = 0; i < cameras.Length; i++)
            cameras[i].gameObject.SetActive(false);
        cameras[0].gameObject.SetActive(true);
    }
    void Update()
    {
        bool moved = false;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            moved = true;
            transform.Translate(acceleration * Vector3.forward * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            moved = true;
            transform.Translate(acceleration * Vector3.back * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            pos--;
            target = Quaternion.Euler(0, pos * turnspeed, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 10);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            pos++;
            target = Quaternion.Euler(0, pos * turnspeed, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 10);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (GetComponent<car>().enabled)
            {
                player.GetComponent<CapsuleCollider>().isTrigger = false;
                player.transform.position = transform.position + new Vector3(2, 10, 0);
                GetComponent<car>().cameras[0].gameObject.SetActive(false);
                player.GetComponent<controller3d>().currentCam.SetActive(true);
                spawner.GetComponent<robotSpawner>().changeBackTargets();
                GetComponent<car>().enabled = false;
            }
        }
        if (!moved)
            acceleration = speed;
        else if (acceleration <= maxSpeed)
            acceleration += 0.1F;
    }
}
