using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GetAbilities : MonoBehaviour
{
    public Canvas scoreImprovement;

    [SerializeField] EnemySpawning enemySpawning;
    [SerializeField] Button abilityButton;
    [SerializeField] PlayerManager playerManager;
    [SerializeField] KunaiManager kunaiManager;
    [SerializeField] PlayerMovement playerMovement;

    public List<Button> scoreButtonPrefabs = new List<Button>();
    public List<Button> scoreButtons = new List<Button>();

    void Start()
    {
        scoreImprovement.enabled = false;
    }

    public void GenerateAbilities()
    {
        Time.timeScale = 0;
        scoreImprovement.enabled = true;
        GetScoreImprovements();

    }

    public void GetScoreImprovements()
    {
        for(int i = 0; i < scoreButtons.Count; i++)
        {
            int x = Random.Range(0, scoreButtonPrefabs.Count);
            scoreButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = scoreButtonPrefabs[x].GetComponentInChildren<TextMeshProUGUI>().text;
            scoreButtons[i].onClick = scoreButtonPrefabs[x].onClick;
        }
    }

    public void atkSpd15()
    {
        playerManager.atkSpeed += 0.15f;
        closeMenu();

    }

    public void atkSpd10()
    {
        playerManager.atkSpeed += 0.10f;
        closeMenu();

    }

    public void atkSpd5()
    {
        playerManager.atkSpeed += 0.05f;
        closeMenu();
    }

    public void dmg15()
    {
        kunaiManager.damageMultiplier += 0.15f;
        closeMenu();

    }

    public void dmg10()
    {
        kunaiManager.damageMultiplier += 0.10f;
        closeMenu();
    }

    public void dmg5()
    {
        kunaiManager.damageMultiplier += 0.05f;
        closeMenu();
    }

    public void move10()
    {
        playerMovement.moveMultiplier += 0.10f;
        closeMenu();
    }

    public void move5()
    {
        playerMovement.moveMultiplier += 0.05f;
        closeMenu();
    }

    public void health5()
    {
        playerMovement.maxHealth += 5;
        closeMenu();
    }

    private void closeMenu()
    { 
        scoreImprovement.enabled = false;
        Time.timeScale = 1;
    }
}
