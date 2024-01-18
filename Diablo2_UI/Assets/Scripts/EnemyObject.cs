using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class EnemyObject : MonoBehaviour
{
    public float range;
    public int givenExperience;
    public int healthPoints = 30;
    public int pureDamage = 7;

    public TMP_Text damageText;
    public Transform centrePoint;
    public Vector3 playerPos;

    private bool colddown = false;
    private bool isSeeingPlayer = false;

    private GameObject player;
    private PlayerObject playerObj;

    NavMeshAgent agent;
    [SerializeField] LayerMask groundLayer, playerLayer;
    [SerializeField] float Walkrange;
    Vector3 destinationPoint;
    bool walkpointSet;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerObj = player.GetComponent<PlayerObject>();
    }


    void Update()
    {
        if (playerPos.x - this.gameObject.transform.position.x < 1 && !colddown && isSeeingPlayer)
            Attack();

        Patrol();

        if (healthPoints <= 0)
        {
            playerObj.ReceiveExperience(givenExperience);
            Destroy(this.gameObject);
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        isSeeingPlayer = true;
        playerPos = other.gameObject.transform.position;
        if (other.gameObject.tag == "Player")
        {
            print("Player found");
            Attack();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        print("Player lost");
        playerPos = other.gameObject.transform.position;
        isSeeingPlayer = false;
    }


    void Attack()
    {
        int chanceToHit = Random.Range(0, 10);
        if (chanceToHit > 7 )
            playerObj.TakeDamage(pureDamage);
        StartCoroutine(ColdDown());
    }

    void Patrol()
    {
        if (!walkpointSet) SearchForDestination();
        if (walkpointSet) agent.SetDestination(destinationPoint);
        if (Vector3.Distance(transform.position, destinationPoint) < 10) walkpointSet = false;
    }

    void SearchForDestination()
    {
        float x = Random.Range(-range, range);
        float z = Random.Range(-range, range);

        destinationPoint = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);

        if (Physics.Raycast(destinationPoint, Vector3.down, groundLayer))
        {
            walkpointSet = true;
        }
    }

    public void TakeDamage(float damage)
    {
        healthPoints -= (int)damage;
        damageText.text = damage.ToString();
        damageText.gameObject.SetActive(true);
        StartCoroutine(DamageText());
    }

    IEnumerator DamageText()
    {
        yield return new WaitForSeconds(1f);
        damageText.text = "0";
        damageText.gameObject.SetActive(false);
    }
        
    IEnumerator ColdDown()
    {
        colddown = true;
        yield return new WaitForSeconds(2.5f);
        colddown = false;
    }
}
