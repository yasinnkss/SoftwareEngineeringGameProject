using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class CharacterMovement : MonoBehaviour
{

    GameObject cam;
    //Vector3 camOffset;
    Animator playerAnim;
    public AudioSource footsPlayer;
    public AudioClip[] footSteps;


    public bool isDead;
    float arrow;
    float upArrow;
    [SerializeField]
    private float speed = 5;
    float verticalInput;
    float horizontalInput;
    float angleF;

    float forward;
    float right;

    // Start is called before the first frame update
    void Start()
    {
        //thisCharacther = gameObject.GetComponent<CharacterController>();// karakter kontrol componentini kullanacaðýz
        cam = GameObject.Find("CamRotater");// main kamerayý kullanacaðýz
        //camOffset = cam.GetComponent<Transform>().position; // kamera pozsiyonunu kullanarak kamerayý takip ettireceðiz
        playerAnim = gameObject.GetComponent<Animator>();// player nesnemiz için animasyon ayarlayacaðýz
        footsPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!isDead)
        {
            angleF = gameObject.GetComponent<Transform>().rotation.eulerAngles.y;
            Movement();//karakter hareket fonksiyonu

            Vector3 farePozisyon = Input.mousePosition;

            // Karakterin dünya koordinatlarýndaki pozisyonunu al
            Vector3 karakterPozisyon = Camera.main.WorldToScreenPoint(transform.position);

            // Fare pozisyonunu karakter pozisyonuna göre düzenle
            Vector3 fareYatay = new(farePozisyon.x - karakterPozisyon.x, 0f, farePozisyon.y - karakterPozisyon.y);

            // Fare pozisyonuyla karakterin y ekseni etrafýnda dönmesini saðla
            float aci = Mathf.Atan2(fareYatay.x, fareYatay.z) * Mathf.Rad2Deg;
            Quaternion hedefRotasyon = Quaternion.Euler(0f, aci, 0f);

            // Karakteri yavaþça hedef rotasyona doðru döndür
            transform.rotation = Quaternion.Lerp(transform.rotation, hedefRotasyon, 5f * Time.deltaTime);
        }
    }

    private void LateUpdate()
    {
        CamFollow(); // kamera takip
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

        gameObject.GetComponent<CharacterController>().Move(new Vector3(arrow, -1, upArrow));





        //transform.localPosition += new Vector3(arrow, 0, upArrow);

        AnimationContrl();


    }

    //camera takip kodu
    void CamFollow()
    {
        cam.transform.position = transform.position;
        //ofset.y = Mathf.Clamp(ofset.y, 0, ofset.y);

        // kameranýn pozisyonunu o mesafeye eþitliyoruz.
    }

    static KeyCode[] RotateKeys(float angle)
    {
        // WASD tuþlarýnýn sýralý dizisi
        KeyCode[] keys = { KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D };

        //    // Açýya göre tuþlarý döndür
        if (angle >= 315 || angle < 45)
        {
            // Durum: 315 <= angle < 45
            return new KeyCode[] { keys[0], keys[1], keys[2], keys[3] };
        }
        else if (angle >= 45 && angle < 135)
        {
            // Durum: 45 <= angle < 135
            return new KeyCode[] { keys[3], keys[2], keys[0], keys[1] };
        }
        else if (angle >= 135 && angle < 225)
        {
            // Durum: 135 <= angle < 225
            return new KeyCode[] { keys[1], keys[0], keys[3], keys[2] };
        }
        else if (angle >= 225 && angle < 315)
        {
            // Durum: 225 <= angle < 315
            return new KeyCode[] { keys[2], keys[3], keys[1], keys[0] };
        }
        return new KeyCode[] { keys[0], keys[1], keys[2], keys[3] };
    }

    void AnimationContrl()
    {

        KeyCode[] RKeys = RotateKeys(angleF);

        if (Input.GetKey(RKeys[0]))
        {
            forward += Time.deltaTime * 2; forward = Mathf.Clamp(forward, -1, 1);

            playerAnim.SetFloat("WalkVertical", forward);
        }
        else if (Input.GetKey(RKeys[1]))
        {
            forward -= Time.deltaTime * 2; forward = Mathf.Clamp(forward, -1, 1);
            playerAnim.SetFloat("WalkVertical", forward);
        }
        else
        {
            if (forward > 0) forward -= Time.deltaTime;
            else if (forward < 0) forward += Time.deltaTime;
            if (0.1f >= forward && forward >= -0.1f) { forward = 0; }
            playerAnim.SetFloat("WalkVertical", forward);
        }

        if (Input.GetKey(RKeys[2]))
        {
            right -= Time.deltaTime * 2; right = Mathf.Clamp(right, -1, 1);

            playerAnim.SetFloat("WalkHorizontal", right);

        }
        else if (Input.GetKey(RKeys[3]))
        {
            right += Time.deltaTime * 2; right = Mathf.Clamp(right, -1, 1);
            playerAnim.SetFloat("WalkHorizontal", right);
        }
        else
        {
            if (right < 0) right += Time.deltaTime;
            else if (right > 0) right -= Time.deltaTime;
            if (0.1f >= right && right >= -0.1f) { right = 0; }
            playerAnim.SetFloat("WalkHorizontal", right);
        }
    }

    public void FootSteps()
    {
        int ndx = Random.Range(0, 2);


        footsPlayer.PlayOneShot(footSteps[ndx]);


    }



}

