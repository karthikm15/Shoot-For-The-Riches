using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class controller3d : MonoBehaviour
{
    [HideInInspector] public float xForce, yForce;
    [HideInInspector] public Rigidbody rb;
    float distToGround;
    private inventoryScript inventory;
    [HideInInspector] public GameObject currentCam;

    [Header("Camera")]
    public GameObject fpcam;
    public GameObject tpcam;
    [Header("Movement")]
    public float speed;
    public float jumpForce;
    [Header("Shooting")]
    public KeyCode shoot;
    public float damage = 10f;
    public float range = 100f;
    public GameObject hand;
    [Header("Inventory")]
    public GameObject Hotbar;
    [Header("Misc")]
    public Slider health;
    public GameObject car;
    public Slider reload;
    public GameObject spawner;

    void Start()
    {
        reload.value = reload.minValue;
        car.GetComponent<car>().enabled = false;
        inventory = Hotbar.GetComponent<inventoryScript>();
        distToGround = GetComponent<CapsuleCollider>().bounds.extents.y;
        rb = GetComponent<Rigidbody>();
        tpcam.SetActive(false);
        fpcam.SetActive(true);
        currentCam = fpcam;
        health.value = health.maxValue;
    }
    bool isGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }
    void Update()
    {
        Weapon gun = hand.GetComponent<Weapon>();
        if (inventory.selected < inventory.guns.Count)
             gun = inventory.guns[inventory.selected].GetComponent<Weapon>();
        if (Input.GetKey(shoot))
        {
            RaycastHit hit;
            if (gun.fire())
            {
                if (Physics.Raycast(currentCam.transform.position, currentCam.transform.forward, out hit, range)) {
                    if (hit.transform.CompareTag("enemy"))
                        hit.transform.gameObject.GetComponent<enemyScript>().takeDamage(gun.damage);
                }
            }
        }
        if (Input.GetKeyDown("c"))
        {
            if (currentCam.Equals(tpcam))
            {
                currentCam = fpcam;
                tpcam.SetActive(false);
                fpcam.SetActive(true);
            }
            else
            {
                currentCam = tpcam;
                fpcam.SetActive(false);
                tpcam.SetActive(true);
            }
        }
        if (Input.GetKeyDown("e") && Mathf.Abs(transform.position.z - car.transform.position.z + (transform.position.x - car.transform.position.x) + (transform.position.y - car.transform.position.y)) <= 2)
        {
            car.GetComponent<car>().enabled = true;
            currentCam.SetActive(false);
            spawner.GetComponent<robotSpawner>().changeTargets(car);
            GetComponent<CapsuleCollider>().isTrigger = true;
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
        }
        if (Input.GetKey(KeyCode.R))
        {
            reload.gameObject.SetActive(true);
            if (reload.value + gun.reloadSpeed/100 < reload.maxValue)
                reload.value += gun.reloadSpeed / 100;
            else
                gun.reload();
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            reload.gameObject.SetActive(false);
            reload.value = reload.minValue;
        }
        transform.rotation = Quaternion.Euler(0, currentCam.GetComponent<cameraScript>().currentY, 0);
        xForce = Input.GetAxis("Horizontal") * speed;
        yForce = Input.GetAxis("Vertical") * speed;
        transform.Translate(new Vector3(xForce, 0, yForce));
        if (Input.GetKeyUp("w") || Input.GetKeyUp("s"))
            rb.velocity = new Vector3(0, 0, 0);
    }
    public void TakeDamage(float amount)
    {
        if (health.value > health.minValue + amount) health.value -= amount;
        else Application.Quit();
    }
}
