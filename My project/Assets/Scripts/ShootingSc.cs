using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShootingSc : MonoBehaviour
{
    [SerializeField]
    bool isPlayer = false;
    float cldown; // cooldown
    Vector3 playerPos;
    [SerializeField]
    private GameObject ammoPrefab; // mermi
    public float ammoSpeed = 1f;
    Vector3 positionWorldCoordinates;
    Vector3 ammoPlayerBulletPos;

    private void Start()
    {
        if (gameObject.tag == "Player")
        {
            isPlayer = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // mouse pozisyonunu cekmek
        if (Input.GetMouseButtonDown(0) && isPlayer)
        {// ate�leyen e�er player nesnesi ise mouse
         // butonu ile istedi�i zaman ate� edebilecek
         // bu k�sma da cooldown ekleyece�im
            shooting(MousePos());
        }

        if (!isPlayer) // e�er ate�leyen enemy nesnelerimiz ise 
                       // ate�leme s�resi koyuyoruz
        {
            cldown += Time.deltaTime;
            if (cldown > 6)
            {
                cldown = 0f;
                playerPos = GameObject.Find("Player").transform.position; // player'�n pozisyonuna ate� etmelerini sa�layaca��z
                shooting(playerPos); // fonksiyonumuza pozisyon g�nderiyoruz
            }
        }
    }

    // merminin g�nderilece�i pozisyonu input alan ve o y�ne mermi g�nderen fonksiyon
    void shooting(Vector3 pos)
    {
        //Vector3 shootingPoint = gameObject.transform.position + ammoPrefab.transform.position; // at���n yap�ld��� nokta
        //shootingPoint.y = ammoPrefab.transform.position.y; // merminin yerden y�ksekli�ini sabitledim
        ammoPlayerBulletPos = GameObject.Find("bulletPoint").transform.position;
        ammoPlayerBulletPos.y = 3.578f;
        Vector3 fireSide = (pos - ammoPlayerBulletPos).normalized; // mousepos => GameObject.Find("Player").transform.position
        GameObject yeniMermi = Instantiate(ammoPrefab, ammoPlayerBulletPos, Quaternion.identity);
        yeniMermi.GetComponent<AmmoMove>().isPlayer = isPlayer; // Mermiyi ate�leyen ki�inin player olup olmad���n� kontrol
                                                                // ediyoruz e�er player ate�lediyse enemy'e hasar verecek
                                                                // ate�leyen enemy ise enemy objeleri birbirine hasar veremeyecek
        yeniMermi.GetComponent<Rigidbody>().AddForce(fireSide * ammoSpeed, ForceMode.Impulse); // mermiyi hareket ettiriyoruz
    }

    // Bu fonksiyon mouse pozsiyonunu almak i�in
    // daha sonra al�nan pozisyona mermi g�ndermek i�in
    public Vector3 MousePos()
    {

        // mouse pozisyonunu cekmek
        Ray lazer = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(lazer, out hitInfo)) // raycasti a��kcas� tam olarak
                                                 // anlayamad�m ancak kullanma amac�m mouse pozisyonunu
                                                 // almak ve o pozisyona do�ru mermi g�ndermekti
                                                 // raycast'i forumlarda ara�t�rarak buldum
        {
            positionWorldCoordinates = hitInfo.point; // mouse pozisyonunu de�i�kene
                                                      // at�yoruz
            positionWorldCoordinates.y = 3.578f;        // mouse pozisyonunun yerden y�ksekli�ini artt�rmam
                                                        // gerekti
        }

        return positionWorldCoordinates;
    }
}
