using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    CharacterController thisCharacther;
    GameObject cam;
    Vector3 camOffset;
    Animator playerAnim;
    ShootingSc mousePos;

    float arrow;
    float upArrow;
    [SerializeField]
    private float speed = 5;




    // Start is called before the first frame update
    void Start()
    {
        mousePos = gameObject.GetComponent<ShootingSc>();
        thisCharacther = gameObject.GetComponent<CharacterController>();// karakter kontrol componentini kullanaca��z
        cam = GameObject.Find("Main Camera");// main kameray� kullanaca��z
        camOffset = cam.GetComponent<Transform>().position; // kamera pozsiyonunu kullanarak kameray� takip ettirece�iz
        playerAnim = gameObject.GetComponent<Animator>();// player nesnemiz i�in animasyon ayarlayaca��z
    }

    // Update is called once per frame
    void Update()
    {
        Movement();//karakter hareket fonksiyonu

        Vector3 farePozisyon = Input.mousePosition;

        // Karakterin d�nya koordinatlar�ndaki pozisyonunu al
        Vector3 karakterPozisyon = Camera.main.WorldToScreenPoint(transform.position);

        // Fare pozisyonunu karakter pozisyonuna g�re d�zenle
        Vector3 fareYatay = new Vector3(farePozisyon.x - karakterPozisyon.x, 0f, farePozisyon.y - karakterPozisyon.y);

        // Fare pozisyonuyla karakterin y ekseni etraf�nda d�nmesini sa�la
        float aci = Mathf.Atan2(fareYatay.x, fareYatay.z) * Mathf.Rad2Deg;
        Quaternion hedefRotasyon = Quaternion.Euler(0f, aci, 0f);

        // Karakteri yava��a hedef rotasyona do�ru d�nd�r
        transform.rotation = Quaternion.Lerp(transform.rotation, hedefRotasyon, 5f * Time.deltaTime);

    }

    private void LateUpdate()
    {
        camFollow(); // kamera takip
                     // late update i�erisinde olma nedenini tam olarak bilmiyorum
                     // ancak update ya da fixed update i�erisinde kullan�nca kamerada titreme sorunu oluyor
                     // belki de sebebi oyuncunun g�ncel pozisyonunu, pozisyonu g�ncellendikten sonra almas�d�r
    }

    void Movement()
    {
        upArrow = Input.GetAxis("Vertical") * Time.deltaTime * speed;// S ve W tu�lar�n� kullanarak -1("S") ve +1("W") de�erler d�nd�r�yor.
                                                                     // (Bizim oyunumuzda vertical Z eksenini ifade ediyor)
        arrow = Input.GetAxis("Horizontal") * Time.deltaTime * -speed;// A ve D tu�lar�n� kullanarak -1("A") ve +1("D") de�erler d�nd�r�yor.

        thisCharacther.Move(transform.TransformDirection(new Vector3(arrow * Vector3.left.x, 0, upArrow * Vector3.forward.z)));

        AnimasyonControl();

    }

    // camera takip kodu
    void camFollow()
    {
        Vector3 ofset = camOffset + thisCharacther.transform.position; // oyuncu ile kamera aras�ndaki mesafeyi ayarl�yoruz
        ofset.y = camOffset.y;
        ofset.z += 4.05f;
        cam.transform.position = ofset; // kameran�n pozisyonunu o mesafeye e�itliyoruz.
    }
    float AngleBetweenPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.z - b.z, a.x - b.x) * Mathf.Rad2Deg;
    }

    void AnimasyonControl()
    {   // Forward and back

        if (Input.GetKey(KeyCode.A))
        {
            playerAnim.SetBool("rightRun", false); // hareket tu�lar� kullan�l�rken animasyonu ba�lat�r
            playerAnim.SetBool("leftRun", true);
            if (Input.GetKeyUp(KeyCode.A))
            {
                playerAnim.SetBool("leftRun", false);
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                playerAnim.SetFloat("idle-run", 0f);
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                playerAnim.SetBool("rightRun", false);
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                playerAnim.SetFloat("idle-run", 0f);
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            playerAnim.SetBool("leftRun", false);
            playerAnim.SetBool("rightRun", true); // hareket tu�lar� kullan�l�rken animasyonu ba�lat�r
            if (Input.GetKeyUp(KeyCode.A))
            {
                playerAnim.SetBool("leftRun", false);
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                playerAnim.SetFloat("idle-run", 0f);
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                playerAnim.SetBool("rightRun", false);
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                playerAnim.SetFloat("idle-run", 0f);
            }
        }

        else if (Input.GetKey(KeyCode.W))
        {
            playerAnim.SetFloat("idle-run", 1f); // hareket tu�lar� kullan�l�rken animasyonu ba�lat�r
            if (Input.GetKeyUp(KeyCode.A))
            {
                playerAnim.SetBool("leftRun", false);
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                playerAnim.SetFloat("idle-run", 0f);
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                playerAnim.SetBool("rightRun", false);
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                playerAnim.SetFloat("idle-run", 0f);
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            playerAnim.SetFloat("idle-run", 2f); // hareket tu�lar� kullan�l�rken animasyonu ba�lat�r  
            if (Input.GetKeyUp(KeyCode.A))
            {
                playerAnim.SetBool("leftRun", false);
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                playerAnim.SetFloat("idle-run", 0f);
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                playerAnim.SetBool("rightRun", false);
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                playerAnim.SetFloat("idle-run", 0f);
            }
        }
        else
        {
            playerAnim.SetFloat("idle-run", 0f);
            playerAnim.SetBool("leftRun", false);
            playerAnim.SetBool("rightRun", false);
        }
        // Left and Right

    }
}
