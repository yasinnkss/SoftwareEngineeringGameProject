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
        player = GameObject.FindWithTag("Player");// Sahnede player objesini bulup deðiþkenimize atýyoruz
    }

    // Update is called once per frame
    void Update()
    {
        // mermi ile temasa geçtiðinde diðer scriptten bu scriptteki heal deðeri eksiltiliyor
        // can 0 olduðunda objeyi siliyoruz.
        if (heal <= 0) { Destroy(gameObject); }

        // Enemy'nin oyuncuyu takip etmesi için
        // MoveTowards fonk. kullanýyorum
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);


        // Bu kýsým enemy nesnesini y deðerini eski haline getirmek için
        // playerýn y pozisyonuna geliyordu
        Vector3 pos = transform.position;
        pos.y = transform.position.y * 0 + 0.4f;
        transform.position = pos;
    }
}
