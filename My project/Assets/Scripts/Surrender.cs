using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surrender : MonoBehaviour
{
    public float surrenderDistance = 5f;
    // enemyin polise teslim olmasi gereken mesafe
    public int warningCountThreshold = 2;
    // ikaz esigi
    
    private Transform playerTransform;
    //playerin transformunu tutyorum
    private bool isSurrendering;
    // dusmanin teslim olup olmadigini 
    private int warningCount;
    // ikaz degiskeni


    //private Rigidbody2D rigidbody;
    //private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        // Player in transformunu buluyur
        warningCount = 0;
        isSurrendering= false;

        //rigidbody= GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(playerTransform.position, transform.position);
        // player ile enemy arasindaki mesafeyi kontrol ediyur

        
        if (distanceToPlayer < surrenderDistance && isSurrendering)
            // eger player belirli meafede bulunuyor ve enemy teslim olmadiysa
        {
            //ikaz olayi
            if (Input.GetKeyDown(KeyCode.F))
            {
                warningCount++;

                if (warningCount >= warningCountThreshold)
                    // eger ikaz iki veya daha fazla olduysa Surrenderi cagiriyor
                {
                    Surrender();
                }
            }
        }        
               
    }

    void Surrender()
    {
        Debug.Log("Enemy teslim oldu, rahat ol.");


        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        // enemyin hizini zero yapip durdurmamiz gerekiyor

        isSurrendering = true;

        Destroy(this.gameObject);
    }

    

}
