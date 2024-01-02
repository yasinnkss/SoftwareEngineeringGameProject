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
        {// ateþleyen eðer player nesnesi ise mouse
         // butonu ile istediði zaman ateþ edebilecek
         // bu kýsma da cooldown ekleyeceðim
            shooting(MousePos());
        }

        if (!isPlayer) // eðer ateþleyen enemy nesnelerimiz ise 
                       // ateþleme süresi koyuyoruz
        {
            cldown += Time.deltaTime;
            if (cldown > 6)
            {
                cldown = 0f;
                playerPos = GameObject.Find("Player").transform.position; // player'ýn pozisyonuna ateþ etmelerini saðlayacaðýz
                shooting(playerPos); // fonksiyonumuza pozisyon gönderiyoruz
            }
        }
    }

    // merminin gönderileceði pozisyonu input alan ve o yöne mermi gönderen fonksiyon
    void shooting(Vector3 pos)
    {
        //Vector3 shootingPoint = gameObject.transform.position + ammoPrefab.transform.position; // atýþýn yapýldýðý nokta
        //shootingPoint.y = ammoPrefab.transform.position.y; // merminin yerden yüksekliðini sabitledim
        ammoPlayerBulletPos = GameObject.Find("bulletPoint").transform.position;
        ammoPlayerBulletPos.y = 3.578f;
        Vector3 fireSide = (pos - ammoPlayerBulletPos).normalized; // mousepos => GameObject.Find("Player").transform.position
        GameObject yeniMermi = Instantiate(ammoPrefab, ammoPlayerBulletPos, Quaternion.identity);
        yeniMermi.GetComponent<AmmoMove>().isPlayer = isPlayer; // Mermiyi ateþleyen kiþinin player olup olmadýðýný kontrol
                                                                // ediyoruz eðer player ateþlediyse enemy'e hasar verecek
                                                                // ateþleyen enemy ise enemy objeleri birbirine hasar veremeyecek
        yeniMermi.GetComponent<Rigidbody>().AddForce(fireSide * ammoSpeed, ForceMode.Impulse); // mermiyi hareket ettiriyoruz
    }

    // Bu fonksiyon mouse pozsiyonunu almak için
    // daha sonra alýnan pozisyona mermi göndermek için
    public Vector3 MousePos()
    {

        // mouse pozisyonunu cekmek
        Ray lazer = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(lazer, out hitInfo)) // raycasti açýkcasý tam olarak
                                                 // anlayamadým ancak kullanma amacým mouse pozisyonunu
                                                 // almak ve o pozisyona doðru mermi göndermekti
                                                 // raycast'i forumlarda araþtýrarak buldum
        {
            positionWorldCoordinates = hitInfo.point; // mouse pozisyonunu deðiþkene
                                                      // atýyoruz
            positionWorldCoordinates.y = 3.578f;        // mouse pozisyonunun yerden yüksekliðini arttýrmam
                                                        // gerekti
        }

        return positionWorldCoordinates;
    }
}
