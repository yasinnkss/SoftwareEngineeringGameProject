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
            {// ate�leyen e�er player nesnesi ise mouse
             // butonu ile istedi�i zaman ate� edebilecek
             // bu k�sma da cooldown ekleyece�im

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

    // merminin g�nderilece�i pozisyonu input alan ve o y�ne mermi g�nderen fonksiyon
    void Shooting()
    {
        if (!alreadyShooted)
        {
            //Vector3 shootingPoint = gameObject.transform.position + ammoPrefab.transform.position; // at���n yap�ld��� nokta
            //shootingPoint.y = ammoPrefab.transform.position.y; // merminin yerden y�ksekli�ini sabitledim
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
            yeniMermi.GetComponent<AmmoMove>().isPlayer = isPlayer; // Mermiyi ate�leyen ki�inin player olup olmad���n� kontrol
            ammoTextUI.text = ammoCount.ToString() + " / 30";
            playerAnim.SetBool("isShoot", true);

            StartCoroutine("animationStopper");
            muzzleFx.Emit(1);
            shootVol.PlayOneShot(impact, 0.5f);
            ammoCount--;
            alreadyShooted = true;
            StartCoroutine("allowShoot");
            // ediyoruz e�er player ate�lediyse enemy'e hasar verecek
            // ate�leyen enemy ise enemy objeleri birbirine hasar veremeyecek    
        }// mermiyi hareket ettiriyoruz
    }

    private void ReloadGun()
    {

        StartCoroutine(methodName: "ReloadCrtn");

    }

    // Bu fonksiyon mouse pozsiyonunu almak i�in
    // daha sonra al�nan pozisyona mermi g�ndermek i�in
    public Vector3 MousePos()
    {

        // mouse pozisyonunu cekmek
        Ray lazer = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(lazer, out RaycastHit hitInfo)) // raycasti a��kcas� tam olarak
        {                                                    // anlayamad�m ancak kullanma amac�m mouse pozisyonunu
                                                             // almak ve o pozisyona do�ru mermi g�ndermekti                                                    // raycast'i forumlarda ara�t�rarak buldum

            positionWorldCoordinates = hitInfo.point; // mouse pozisyonunu de�i�kene
                                                      // at�yoruz
            positionWorldCoordinates.y = 3.578f;        // mouse pozisyonunun yerden y�ksekli�ini artt�rmam
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
