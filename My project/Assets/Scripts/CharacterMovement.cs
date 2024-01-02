using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    CharacterController thisCharacther;
    GameObject cam;
    Vector3 camOffset;
    Animator playerAnim;
    Quaternion cAngle;

    float arrow;
    float upArrow;
    [SerializeField]
    private float speed = 5;
    float verticalInput;
    float horizontalInput;


    // Start is called before the first frame update
    void Start()
    {

        thisCharacther = gameObject.GetComponent<CharacterController>();// karakter kontrol componentini kullanacaðýz
        cam = GameObject.Find("Main Camera");// main kamerayý kullanacaðýz
        camOffset = cam.GetComponent<Transform>().position; // kamera pozsiyonunu kullanarak kamerayý takip ettireceðiz
        playerAnim = gameObject.GetComponent<Animator>();// player nesnemiz için animasyon ayarlayacaðýz
    }

    // Update is called once per frame
    void Update()
    {

        Movement();//karakter hareket fonksiyonu

        Vector3 farePozisyon = Input.mousePosition;

        // Karakterin dünya koordinatlarýndaki pozisyonunu al
        Vector3 karakterPozisyon = Camera.main.WorldToScreenPoint(transform.position);

        // Fare pozisyonunu karakter pozisyonuna göre düzenle
        Vector3 fareYatay = new Vector3(farePozisyon.x - karakterPozisyon.x, 0f, farePozisyon.y - karakterPozisyon.y);

        // Fare pozisyonuyla karakterin y ekseni etrafýnda dönmesini saðla
        float aci = Mathf.Atan2(fareYatay.x, fareYatay.z) * Mathf.Rad2Deg;
        Quaternion hedefRotasyon = Quaternion.Euler(0f, aci, 0f);

        // Karakteri yavaþça hedef rotasyona doðru döndür
        transform.rotation = Quaternion.Lerp(transform.rotation, hedefRotasyon, 5f * Time.deltaTime);

    }

    private void LateUpdate()
    {
        camFollow(); // kamera takip
                     // late update içerisinde olma nedenini tam olarak bilmiyorum
                     // ancak update ya da fixed update içerisinde kullanýnca kamerada titreme sorunu oluyor
                     // belki de sebebi oyuncunun güncel pozisyonunu, pozisyonu güncellendikten sonra almasýdýr
    }

    void Movement()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        upArrow = verticalInput * Time.deltaTime * speed;// S ve W tuþlarýný kullanarak -1("S") ve +1("W") deðerler döndürüyor.
                                                         // (Bizim oyunumuzda vertical Z eksenini ifade ediyor)
        arrow = horizontalInput * Time.deltaTime * speed;// A ve D tuþlarýný kullanarak -1("A") ve +1("D") deðerler döndürüyor.

        thisCharacther.Move(new Vector3(arrow, 0, upArrow));

        AnimasyonControl();

    }

    // camera takip kodu
    void camFollow()
    {
        Vector3 ofset = camOffset + thisCharacther.transform.position; // oyuncu ile kamera arasýndaki mesafeyi ayarlýyoruz
        ofset.y = camOffset.y;
        ofset.z += 4.05f;
        cam.transform.position = ofset; // kameranýn pozisyonunu o mesafeye eþitliyoruz.
    }
    float AngleBetweenPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.z - b.z, a.x - b.x) * Mathf.Rad2Deg;
    }

    void AnimasyonControl()
    {   // Forward and back

        float angle = gameObject.GetComponent<Transform>().rotation.eulerAngles.y;
        if (angle >= 315 || angle < 45)
        {
            Debug.Log(angle);
            if (verticalInput > 0)
            {
                playerAnim.SetFloat("idle-run", 1f);

                playerAnim.SetBool("rightRun", false);
                playerAnim.SetBool("leftRun", false);
            }
            else if (horizontalInput > 0)
            {
                playerAnim.SetBool("rightRun", true);
                playerAnim.SetFloat("idle-run", 0f);

                playerAnim.SetBool("leftRun", false);
            }
            else if (horizontalInput < 0)
            {
                playerAnim.SetBool("leftRun", true);
                playerAnim.SetFloat("idle-run", 0f);
                playerAnim.SetBool("rightRun", false);

            }
            else if (verticalInput < 0)
            {
                playerAnim.SetFloat("idle-run", 2f);
                playerAnim.SetBool("rightRun", false);
                playerAnim.SetBool("leftRun", false);
            }
            else
            {
                // W tuþu býrakýldýðýnda veya deðeri 0 olduðunda ilgili animasyonu durdur
                playerAnim.SetFloat("idle-run", 0f);
                playerAnim.SetBool("rightRun", false);
                playerAnim.SetBool("leftRun", false);
            }
        }
        else if (angle >= 45 && angle < 135)
        {
            if (verticalInput > 0)
            {
                playerAnim.SetBool("leftRun", true);
                playerAnim.SetFloat("idle-run", 0f);
                playerAnim.SetBool("rightRun", false);
            }
            else if (horizontalInput > 0)
            {
                playerAnim.SetFloat("idle-run", 1f);
                playerAnim.SetBool("rightRun", false);
                playerAnim.SetBool("leftRun", false);
            }
            else if (horizontalInput < 0)
            {
                playerAnim.SetFloat("idle-run", 2f);
                playerAnim.SetBool("rightRun", false);
                playerAnim.SetBool("leftRun", false);
            }
            else if (verticalInput < 0)
            {
                playerAnim.SetBool("rightRun", true);
                playerAnim.SetFloat("idle-run", 0f);
                playerAnim.SetBool("leftRun", false);
            }
            else
            {
                // W tuþu býrakýldýðýnda veya deðeri 0 olduðunda ilgili animasyonu durdur
                playerAnim.SetFloat("idle-run", 0f);
                playerAnim.SetBool("rightRun", false);
                playerAnim.SetBool("leftRun", false);
            }
        }
        else if (angle >= 135 && angle < 225)
        {
            if (verticalInput > 0)
            {
                playerAnim.SetFloat("idle-run", 2f);
                playerAnim.SetBool("rightRun", false);
                playerAnim.SetBool("leftRun", false);
            }
            else if (horizontalInput > 0)
            {
                playerAnim.SetBool("leftRun", true);
                playerAnim.SetFloat("idle-run", 0f);
                playerAnim.SetBool("rightRun", false);
            }
            else if (horizontalInput < 0)
            {
                playerAnim.SetBool("rightRun", true);
                playerAnim.SetFloat("idle-run", 0f);
                playerAnim.SetBool("leftRun", false);
            }
            else if (verticalInput < 0)
            {
                playerAnim.SetFloat("idle-run", 1f);
                playerAnim.SetBool("rightRun", false);
                playerAnim.SetBool("leftRun", false);
            }
            else
            {
                playerAnim.SetFloat("idle-run", 0f);
                playerAnim.SetBool("rightRun", false);
                playerAnim.SetBool("leftRun", false);
            }
        }
        else if (angle >= 225 && angle < 315)
        {
            if (verticalInput > 0)
            {
                playerAnim.SetBool("rightRun", true);
                playerAnim.SetFloat("idle-run", 0f);
                playerAnim.SetBool("leftRun", false);
            }
            else if (horizontalInput > 0)
            {
                playerAnim.SetFloat("idle-run", 2f);
                playerAnim.SetBool("rightRun", false);
                playerAnim.SetBool("leftRun", false);
            }
            else if (horizontalInput < 0)
            {
                playerAnim.SetFloat("idle-run", 1f);
                playerAnim.SetBool("rightRun", false);
                playerAnim.SetBool("leftRun", false);
            }
            else if (verticalInput < 0)
            {
                playerAnim.SetBool("leftRun", true);
                playerAnim.SetFloat("idle-run", 0f);
                playerAnim.SetBool("rightRun", false);
            }
            else
            {
                playerAnim.SetFloat("idle-run", 0f);
                playerAnim.SetBool("rightRun", false);
                playerAnim.SetBool("leftRun", false);
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
