using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

// an enum is a list of constants
public enum BattleState { START, PLAYERTURN, ENEMYTURN, COMBAT, WON, LOST }

public class battleSystem : MonoBehaviour
{
    public static battleSystem instance;

    [Header("objects")]
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public GameObject endTurnbutton;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    public Unit playerUnit;
    public Unit enemyUnit;

    public Text dialogueText;

    public BattleHud playerHUD;
    public BattleHud enemyHUD;

    public BattleState state;

    private void Awake()
    {
        SetUpSingleton();
    }
    private void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    private void SetUpSingleton()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }

    IEnumerator SetupBattle()
    {
        //instantiate the player, make it a child off the player battle station.
        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        //create a variable called "playerUnit" which is basically the script attached to the player we instantiated.
        playerUnit = playerGO.GetComponent<Unit>();


        //same as above but for enemy
        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Unit>();


        //change the text at the bottom of the screen to show which enemy appeared
        dialogueText.text = "A wild " + enemyUnit.unitName + " approachs";

        //calls the script playerHUD and enemyHUD and calls the function setHUD from that script, that sets the HP bar/name/level of enemy
        // and displays it on screen
        playerHUD.SetHud(playerUnit);
        enemyHUD.SetHud(enemyUnit);

        //wait 2 seconds
        yield return new WaitForSeconds(2f);

        //change the enum to PLAYERTURN and then call the function playerturn;
        state = BattleState.PLAYERTURN;
        playerTurn();
    }
    IEnumerator EndTurn()
    {

        dialogueText.text = "Turn over";


        // wait 2 seconds
        yield return new WaitForSeconds(2f);


        //check if that hit killed the enemy, if so trigger the WON state, if it didnt trigger the ENEMY TURN state.
        if(enemyUnit.currentHP <= 0)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());

        }

        //check if enemy dead
        //change state
    }


    IEnumerator EnemyTurn()
    {
        dialogueText.text = enemyUnit.unitName + " Attacks";

        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

        playerHUD.SetHP(playerUnit.currentHP);

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            TechController.instance.DrawCard(3);
            playerTurn();
        }


    }

    void EndBattle()
    {
        if(state == BattleState.WON)
        {
            dialogueText.text = "you won the battle!";
        }else if(state == BattleState.LOST)
        {
            dialogueText.text = "you were defeated.";
        }
    }

    void playerTurn()
    {

        Debug.Log("Player turn");

        // set energy to 3 and update hud

        endTurnbutton.SetActive(true);
        dialogueText.text = "choose an action";
    }

    public void EndTurnButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        endTurnbutton.SetActive(false);

        playerUnit.GainEnergy(3);
        playerHUD.SetEnergy(playerUnit.currentEnergy);
        StartCoroutine(EndTurn());
    }


    public void TechPlayed(Techs lastTechPlayed)
    {
        //spend energy
        //playerUnit.SpendEnergy(lastTechPlayed.EnergyCost);
        //update energy HUD
        playerHUD.SetEnergy(playerUnit.currentEnergy);

        if (lastTechPlayed.doesDamage)
        {
            enemyUnit.TakeDamage(lastTechPlayed.damageAmount);
            enemyHUD.SetHP(enemyUnit.currentHP);

        }
        if (lastTechPlayed.restoresHP)
        {
            playerUnit.Heal(lastTechPlayed.healingAmount);
            playerHUD.SetHP(playerUnit.currentHP);
        }
    }
}
