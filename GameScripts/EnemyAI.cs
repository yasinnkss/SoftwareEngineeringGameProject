using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    GameManager gameManager;
    public Animator enemyrAnim;
    //patrolling
    public Vector3 walkPoint;
    public GameObject projectile;
    public GameObject gun;
    public ParticleSystem gunFx;

    public AudioSource enemyAudioSource;
    public AudioClip akVol;

    bool walkPointSet;
    public float walkPointRange;
    public float health;
    //attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    float forward;

    List<GameObject> waypoints;

    float attackTime = 0;
    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    public bool isDead;
    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (!isDead)
        {
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            if (!playerInSightRange && !playerInAttackRange) Patroling(); forward += Time.deltaTime; forward = Mathf.Clamp(forward, -1, 1);
            if (playerInSightRange && !playerInAttackRange) ChasePlayer(); forward += Time.deltaTime; forward = Mathf.Clamp(forward, -1, 1);
            if (playerInAttackRange && playerInSightRange) ReadyForAttack();
            CheckHeal();
        }

    }

    void Patroling()
    {
        enemyrAnim.SetBool("shootReady", false);
        if (!walkPointSet) { enemyrAnim.SetFloat("VerticalAx", forward); SearchWalkPoint(); }

        if (walkPointSet) { enemyrAnim.SetFloat("VerticalAx", forward); agent.SetDestination(walkPoint); transform.LookAt(walkPoint); }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    void SearchWalkPoint()
    {

        waypoints = ClassExtension.GetAllChilds(GameObject.Find("Waypoints/Enemy1")); //GameObject.Find("Waypoints/Enemy1").transform.GetAllChilds();
        int ndx = Random.Range(0, 5);


        walkPoint = waypoints[ndx].transform.position;

        if (walkPoint.magnitude < 1f)
        {
            SearchWalkPoint();
        }

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }


    void ChasePlayer()
    {
        enemyrAnim.SetBool("shootReady", false);
        agent.SetDestination(player.position);
        enemyrAnim.SetFloat("VerticalAx", 1f, 0.1f, Time.deltaTime);
    }

    void ReadyForAttack()
    {
        enemyrAnim.SetBool("shootReady", true);
        forward -= Time.deltaTime; forward = Mathf.Clamp(forward, 0, 1);
        agent.SetDestination(transform.position);
        enemyrAnim.SetFloat("VerticalAx", 0);
        transform.LookAt(player);

        attackTime += Time.deltaTime * 0.5f; //(gameObject.transform.position - player.position).magnitude;
        attackTime = Mathf.Clamp(attackTime, 0, 0.5f);
        if (attackTime > 0.3f)
        {
            enemyrAnim.SetFloat("fireRate", attackTime);

        }

    }

    void AttackPlayer()
    {

        Rigidbody rb = Instantiate(projectile, gun.transform.position,
            Quaternion.identity).GetComponent<Rigidbody>();

        rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
        gunFx.Play();
        enemyAudioSource.PlayOneShot(akVol);

        StartCoroutine("fireFX");
    }

    void ResetAttack()
    {
        if (alreadyAttacked) attackTime -= Time.deltaTime; attackTime = Mathf.Clamp(attackTime, 0, 1);
        if (attackTime == 0) alreadyAttacked = false;

    }

    void CheckHeal()
    {
        if (health <= 0)
        {
            enemyrAnim.SetTrigger("dead"); isDead = true;
            enemyrAnim.SetBool("shootReady", false);
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            gameManager.AddPoint(100);
        }//Invoke(nameof(DestroyEnemy), 0.5f);
    }

    void DestroyEnemy() { Destroy(gameObject); }
    IEnumerator fireFX()
    {
        yield return new WaitForSeconds(0.2f);
        gunFx.Stop();
    }
}


public static class ClassExtension
{
    public static List<GameObject> GetAllChilds(this GameObject Go)
    {
        List<GameObject> list = new();
        for (int i = 0; i < Go.transform.childCount; i++)
        {
            list.Add(Go.transform.GetChild(i).gameObject);
        }
        return list;
    }
}

