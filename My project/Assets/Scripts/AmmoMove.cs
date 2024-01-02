using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoMove : MonoBehaviour
{
    float outBand = 70f; // sahneden uzaklaþan mermi olursa yok etmek için sýnýr mesafe
    public bool isPlayer;

    // Update is called once per frame
    void Update()
    {
        // mermi x ve z ekseninde fazla uzaklaþýp
        // oyun alanýndan çýkarsa yok etmek için
        Vector3 ammoPos = gameObject.transform.position;
        if (ammoPos.x > outBand || ammoPos.z > outBand || ammoPos.x < -outBand || ammoPos.z < -outBand)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Mermi player yada enemy ile karþýlaþýnca
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Girdi");
            other.gameObject.GetComponent<Enemy>().heal -= 20;//enemy objesindeki Enemy script içerisindeki heal deðerini eksiltir
        }
        else if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<CharSpecs>().Heal -= 20;// player için de ayný durum
        }
        else
        {
            if (!other.CompareTag("bar"))
            {
                Destroy(gameObject);
            }

        }
    }
}
