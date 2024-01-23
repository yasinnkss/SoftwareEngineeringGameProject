using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSpecs : MonoBehaviour
{
    public bool isDead;
    Animator playerAnim;
    public int heal = 100;
    HealthBar healUIScript;

    int[] damages = { 5, 5, 5, 8, 8, 8, 10, 10, 10, 10, 5, 5, 10, 12, 5, 5 };
    // Start is called before the first frame update
    void Start()
    {
        healUIScript = GameObject.Find("HealthBar").GetComponent<HealthBar>();
        playerAnim = GetComponent<Animator>();
        //healUIScript.maxHealth(heal);
    }


    public void TakenDamage()
    {

        int damage = Random.Range(0, damages.Length);
        damage = damages[damage];
        heal -= damage;
        healUIScript.SetHealth(heal);
        if (heal <= 0)
        {
            if (!isDead)
            {
                playerAnim.SetTrigger("isDead");
            }
            isDead = true;
            GetComponent<ShootingSc>().isDead = isDead;
            GetComponent<CharacterMovement>().isDead = isDead;
        }
        else
        {
            playerAnim.SetTrigger("hitted");
        }

    }

}
