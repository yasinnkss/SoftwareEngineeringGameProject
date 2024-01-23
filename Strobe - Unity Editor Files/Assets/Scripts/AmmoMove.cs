using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoMove : MonoBehaviour
{
    GameManager gameManager;


    public bool isPlayer = false;
    public float speed = 2.1f;


    private void Start()
    {
        Destroy(gameObject, 1.2f);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (!isPlayer)
        {
            gameManager.bullets++;
            gameObject.transform.LookAt(GameObject.Find("Player").transform);
            gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * speed, ForceMode.VelocityChange);
            transform.forward *= speed;

        }
        if (isPlayer)
        {
            gameManager.bullets++;
            transform.rotation = GameObject.Find("bulletPoint").transform.rotation;
            gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * speed, ForceMode.VelocityChange);

        }
    }

    // Update is called once per frame

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Enemy") && !other.CompareTag("Player"))
        {
            Debug.Log("duvarda yok olan mermi:" + gameManager.score + "   total: " + gameManager.bullets);
        }
        // Mermi player yada enemy ile kar��la��nca
        if (other.CompareTag("Enemy") && isPlayer)
        {
            Debug.Log("Girdi");
            other.gameObject.GetComponent<EnemyAI>().health -= 20;//enemy objesindeki Enemy script i�erisindeki heal de�erini eksiltir
            other.GetComponent<Animator>().SetTrigger("hitting");
            Destroy(gameObject);
        }
        else if (other.CompareTag("Player") && !isPlayer)
        {

            other.gameObject.GetComponent<CharSpecs>().TakenDamage();// player i�in de ayn� durum
            Destroy(gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Enemy") && !other.CompareTag("Player"))
        {
            Debug.Log("world! 123");
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}