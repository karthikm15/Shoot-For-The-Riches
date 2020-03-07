using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour
{
    public static GameObject target;
    public float speed;
    public float damage;
    public float health = 20f;
    public Transform wheel;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        Physics.IgnoreCollision(GetComponent<BoxCollider>(), target.GetComponent<CapsuleCollider>());
    }
    void OnTriggerEnter(Collider coll)
    {
        if (coll.CompareTag("Player"))
        {
            Debug.Log("Damage");
            controller3d player = coll.gameObject.GetComponent<controller3d>();
            player.TakeDamage(damage);
            player.rb.AddForce(transform.forward * 3, ForceMode.Impulse);
        }
    }
    public void takeDamage(float d)
    {
        health -= d;
        if (health <= 0)
            Object.Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        wheel.Rotate(0, 0, speed);
        transform.LookAt(target.transform);
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * speed);
    }
}
