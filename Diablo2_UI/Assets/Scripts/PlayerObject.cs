using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;
using MenuManager;
using PlayerAttributes;

public class PlayerObject : MonoBehaviour
{



    public Camera playerCamera;
    public PlayerAttributes.PlayerAttributes playerAttributes;
    public GameObject menuManager;
    public Slider healthSlider;
    public Slider manaSlider;
    public Slider xpSlider;
    public TMP_Text damageText;
    public TMP_Text actualXPText;
    public TMP_Text nextLevelXPText;
    public TMP_Text actualLevelText;
    public TMP_Text actualHealthText;
    public TMP_Text maxHealthText;

    private bool colddown = false;
    private bool healingCD = false;
    private int actualXp = 0;
    private int xpToNextLevel = 45;
    private int actualLevel = 1;
    private int chanceToHit;
    private string groundTag = "Ground";
    private string wallTag = "Wall";
    private string enemyTag = "Enemy";

    private GameObject enemyObj;
    private NavMeshAgent agent;
    private RaycastHit hit;

    private GameObject wallObj;

    void Start()
    {
        wallObj = GameObject.FindGameObjectWithTag("Wall");
        actualLevelText.text = actualLevel.ToString();
        nextLevelXPText.text = xpToNextLevel.ToString();
        actualHealthText.text = playerAttributes.healthPoints.ToString();
        maxHealthText.text = playerAttributes.maxHealthPoints.ToString();
        healthSlider.maxValue = playerAttributes.healthPoints;
        manaSlider.maxValue = playerAttributes.healthPoints;
        manaSlider.value = playerAttributes.healthPoints;
        xpSlider.maxValue = xpToNextLevel;
        xpSlider.value = actualXp;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        actualXPText.text = actualXp.ToString();
        xpSlider.value = actualXp;
        healthSlider.value = playerAttributes.healthPoints;
        actualHealthText.text = playerAttributes.healthPoints.ToString();
        maxHealthText.text = playerAttributes.maxHealthPoints.ToString();

        chanceToHit = Random.Range(0, 10);

        if (Input.GetKeyDown(KeyCode.I))
            menuManager.GetComponent<MenuManagerClass>().ManageInventory();

        if (Input.GetKeyDown(KeyCode.T))
            menuManager.GetComponent<MenuManagerClass>().ManageSkillTree();

        if (Input.GetKeyDown(KeyCode.C))
            menuManager.GetComponent<MenuManagerClass>().ManageCharacter();

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag(groundTag) || hit.collider.CompareTag(wallTag))
                {
                    agent.SetDestination(hit.point);
                }

                else if (hit.collider.CompareTag(enemyTag))
                {
                    Vector3 enemyPos = hit.collider.GetComponent<Transform>().position;
                    enemyObj = hit.collider.gameObject;
                    
                    if (enemyPos.x - this.gameObject.transform.position.x < playerAttributes.attackRange && !colddown)
                        Attack(enemyObj);
                    else
                        agent.SetDestination(hit.point);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.H) && !healingCD)
            Healing();

        if (playerAttributes.healthPoints <= 0)
            Time.timeScale = 0;

        if (actualXp >= xpToNextLevel)
            LevelUp();
    }

    void LevelUp()
    {
        AdjustXPSettings();
        AdjustAttributes();
        AdjustAttributesTexts();
        actualLevel++;
    }

    void AdjustXPSettings()
    {
        xpSlider.minValue = xpToNextLevel;
        xpToNextLevel = xpToNextLevel + (int)(xpToNextLevel * 1.4f);
        xpSlider.maxValue = xpToNextLevel;
    }

    void AdjustAttributes()
    {
        playerAttributes.availablePoints += 5;
        playerAttributes.maxHealthPoints += (int)(playerAttributes.maxHealthPoints * 0.1f);
        playerAttributes.healthPoints = playerAttributes.maxHealthPoints;
    }

    void AdjustAttributesTexts()
    {
        actualLevelText.text = actualLevel.ToString();
        nextLevelXPText.text = xpToNextLevel.ToString();
        maxHealthText.text = playerAttributes.maxHealthPoints.ToString();
    }

    void Healing()
    {
        if (playerAttributes.maxHealthPoints - playerAttributes.healthPoints >= 25)
        {
            playerAttributes.healthPoints += 25;
            damageText.text = "25";
        }
        else
        {
            playerAttributes.healthPoints += (playerAttributes.maxHealthPoints - playerAttributes.healthPoints);
            damageText.text = (playerAttributes.maxHealthPoints - playerAttributes.healthPoints).ToString();
        }
        damageText.gameObject.SetActive(true);
        StartCoroutine(HealingColddown());
        StartCoroutine(DamageText());
    }

    public void TakeDamage(int damage)
    {
        damageText.text = damage.ToString();
        damageText.gameObject.SetActive(true);
        playerAttributes.healthPoints = playerAttributes.healthPoints - damage;
        StartCoroutine(DamageText());
    }

    public void Attack(GameObject enemyObj)
    {
        if (chanceToHit > 3)
            enemyObj.GetComponent<EnemyObject>().TakeDamage(playerAttributes.attackDamage);
        
        StartCoroutine(AttackColddown());
    }

    public void ReceiveExperience(int receivedExperience)
    {
        actualXp += receivedExperience;
    }

    IEnumerator DamageText()
    {
        yield return new WaitForSeconds(1);
        damageText.gameObject.SetActive(false);
        damageText.text = "0";
    }

    IEnumerator HealingColddown()
    {
        healingCD = true;
        yield return new WaitForSeconds(2.5f);
        healingCD = false;
    }

    IEnumerator AttackColddown()
    {
        colddown = true;
        yield return new WaitForSeconds(1.2f);
        colddown = false;
    }
}
