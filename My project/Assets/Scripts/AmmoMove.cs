using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoMove : MonoBehaviour
{
    float outBand = 70f; // sahneden uzakla�an mermi olursa yok etmek i�in s�n�r mesafe
    public bool isPlayer;

    // Update is called once per frame
    void Update()
    {
        // mermi x ve z ekseninde fazla uzakla��p
        // oyun alan�ndan ��karsa yok etmek i�in
        Vector3 ammoPos = gameObject.transform.position;
        if (ammoPos.x > outBand || ammoPos.z > outBand || ammoPos.x < -outBand || ammoPos.z < -outBand)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Mermi player yada enemy ile kar��la��nca
        if (other.CompareTag("Enemy") && isPlayer)
        {
            other.gameObject.GetComponent<Enemy>().heal -= 20;//enemy objesindeki Enemy script i�erisindeki heal de�erini eksiltir
        }
        else if (other.CompareTag("Player") && !isPlayer)
        {
            other.gameObject.GetComponent<CharSpecs>().Heal -= 20;// player i�in de ayn� durum
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject); // Merminin �arpt��� herhangi bir kat� cisimde
                             // yok olmas� i�in ekledim buray� ancak �al��mad� bu fonksiyon
    }

}
