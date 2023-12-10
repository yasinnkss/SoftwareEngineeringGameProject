using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float heal = 100;
    private GameObject player;
    public float speed = 4f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");// Sahnede player objesini bulup de�i�kenimize at�yoruz
    }

    // Update is called once per frame
    void Update()
    {
        // mermi ile temasa ge�ti�inde di�er scriptten bu scriptteki heal de�eri eksiltiliyor
        // can 0 oldu�unda objeyi siliyoruz.
        if (heal <= 0) { Destroy(gameObject); }

        // Enemy'nin oyuncuyu takip etmesi i�in
        // MoveTowards fonk. kullan�yorum
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);


        // Bu k�s�m enemy nesnesini y de�erini eski haline getirmek i�in
        // player�n y pozisyonuna geliyordu
        Vector3 pos = transform.position;
        pos.y = transform.position.y * 0 + 0.4f;
        transform.position = pos;
    }
}
