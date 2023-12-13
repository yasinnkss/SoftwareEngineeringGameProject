using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{



    public Transform hedef; // Hedef olarak oyuncunun transformini atayın

    public float atesHizi = 10f; // Mermi hızı
    public GameObject mermiPrefab; // Mermi prefabı

    private float atesAraligi = 2f; // Ateş aralığı
    private float zamanSayaci = 0f;
    Vector3 hedefYonu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Belirli bir süre aralığında ateş etme
        zamanSayaci += Time.deltaTime;
        if (zamanSayaci >= atesAraligi)
        {
            AtesEt();
            zamanSayaci = 0f;
        }
    }

    void AtesEt()
    {
        if (hedef != null)
        {
            hedefYonu = (hedef.position - transform.position).normalized;
            // Mermi oluştur
            GameObject yeniMermi = Instantiate(mermiPrefab, transform.position, Quaternion.identity);
            
            // Mermiye hız ver
            Rigidbody mermiRigidbody = yeniMermi.GetComponent<Rigidbody>();
            if (mermiRigidbody != null)
            {
                mermiRigidbody.AddForce( atesHizi * hedefYonu,ForceMode.Impulse);
            }
        }
    }

    
}
