using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ShootingSc : MonoBehaviour
{
    //Vector3 playerPos;
    [SerializeField] GameObject ammoPrefab; // mermi
    Vector3 positionWorldCoordinates;
    Vector3 ammoPlayerBulletPos;
    public Animator playerAnim;
    public ParticleSystem muzzleFx;
    public AudioClip impact;
    public AudioSource shootVol;

    TextMeshProUGUI ammoTextUI;
    bool isReloaded;
    int ammoCount = 30;
    public bool isDead;

    [SerializeField]
    public bool isPlayer = false;
    public float cldown = 0; // cooldown
    bool alreadyShooted;

    private void Start()
    {
        if (gameObject.CompareTag("Player"))
        {
            isPlayer = true;
        }
        else
        {
            isPlayer = false;
        }
        ammoTextUI = GameObject.Find("AmmoAmount").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            // mouse pozisyonunu cekmek
            if (Input.GetMouseButtonDown(0) && isPlayer)
            {// ateþleyen eðer player nesnesi ise mouse
             // butonu ile istediði zaman ateþ edebilecek
             // bu kýsma da cooldown ekleyeceðim

                if (ammoCount > 0)
                {
                    Shooting();
                    ammoTextUI.text = ammoCount.ToString() + " / 30";
                }
                else { ReloadGun(); ammoTextUI.text = "Reloading..."; }

            }
            if (isReloaded)
            {
                isReloaded = false;
                ammoTextUI.text = ammoCount.ToString() + " / 30";
            }

            //cldown += Time.deltaTime;
            //if (cldown > 0.5f)
            //{
            //    cldown = 0f;
            //    muzzleFx.Stop();
            //}
        }
    }

    // merminin gönderileceði pozisyonu input alan ve o yöne mermi gönderen fonksiyon
    void Shooting()
    {
        if (!alreadyShooted)
        {
            //Vector3 shootingPoint = gameObject.transform.position + ammoPrefab.transform.position; // atýþýn yapýldýðý nokta
            //shootingPoint.y = ammoPrefab.transform.position.y; // merminin yerden yüksekliðini sabitledim
            if (isPlayer)
            {
                ammoPlayerBulletPos = GameObject.Find("bulletPoint").transform.position;
            }
            else
            {
                ammoPlayerBulletPos = gameObject.transform.position;
            }

            //Vector3 fireSide = (pos - ammoPlayerBulletPos).normalized; // mousepos => GameObject.Find("Player").transform.position
            GameObject yeniMermi = Instantiate(ammoPrefab,
                                               ammoPlayerBulletPos,
                                               Quaternion.identity);
            yeniMermi.GetComponent<AmmoMove>().isPlayer = isPlayer; // Mermiyi ateþleyen kiþinin player olup olmadýðýný kontrol
            ammoTextUI.text = ammoCount.ToString() + " / 30";
            playerAnim.SetBool("isShoot", true);

            StartCoroutine("animationStopper");
            muzzleFx.Emit(1);
            shootVol.PlayOneShot(impact, 0.5f);
            ammoCount--;
            alreadyShooted = true;
            StartCoroutine("allowShoot");
            // ediyoruz eðer player ateþlediyse enemy'e hasar verecek
            // ateþleyen enemy ise enemy objeleri birbirine hasar veremeyecek    
        }// mermiyi hareket ettiriyoruz
    }

    private void ReloadGun()
    {

        StartCoroutine(methodName: "ReloadCrtn");

    }

    // Bu fonksiyon mouse pozsiyonunu almak için
    // daha sonra alýnan pozisyona mermi göndermek için
    public Vector3 MousePos()
    {

        // mouse pozisyonunu cekmek
        Ray lazer = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(lazer, out RaycastHit hitInfo)) // raycasti açýkcasý tam olarak
        {                                                    // anlayamadým ancak kullanma amacým mouse pozisyonunu
                                                             // almak ve o pozisyona doðru mermi göndermekti                                                    // raycast'i forumlarda araþtýrarak buldum

            positionWorldCoordinates = hitInfo.point; // mouse pozisyonunu deðiþkene
                                                      // atýyoruz
            positionWorldCoordinates.y = 3.578f;        // mouse pozisyonunun yerden yüksekliðini arttýrmam
                                                        // gerekti
        }

        return positionWorldCoordinates;
    }

    IEnumerator ReloadCrtn()
    {
        yield return new WaitForSeconds(5);
        ammoCount = 30;
        isReloaded = true;
    }

    IEnumerator allowShoot()
    {
        Debug.Log("Wait for shoot!!");
        yield return new WaitForSeconds(.5f);
        alreadyShooted = false;
    }

    IEnumerator animationStopper()
    {
        yield return new WaitForSeconds(.05f);
        playerAnim.SetBool("isShoot", false);
    }
}
