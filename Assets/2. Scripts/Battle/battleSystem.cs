using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// an enum is a list of constants


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

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            ObjectEnabler.instance.enableUI();
            SceneManager.LoadScene("A5. Town");
        }
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
        playerHUD.SetEnergy(playerUnit);
        enemyHUD.SetHud(enemyUnit);

        setUpEnemyTechs();

        //wait 2 seconds
        yield return new WaitForSeconds(2f);

        //change the enum to PLAYERTURN and then call the function playerturn;
        state = BattleState.PLAYERTURN;
        playerTurn();
    }

    public void setUpEnemyTechs()
    {
        //set up the combinations of techs the enemy can use
        EnemyTechController.instance.Combo1 = enemyUnit.Combo1;
        EnemyTechController.instance.Combo2 = enemyUnit.Combo2;
        EnemyTechController.instance.Combo3 = enemyUnit.Combo3;
        EnemyTechController.instance.Combo4 = enemyUnit.Combo4;
        EnemyTechController.instance.Combo5 = enemyUnit.Combo5;
        EnemyTechController.instance.Combo6 = enemyUnit.Combo6;
        EnemyTechController.instance.Combo7 = enemyUnit.Combo7;
        EnemyTechController.instance.Combo8 = enemyUnit.Combo8;
        EnemyTechController.instance.Combo9 = enemyUnit.Combo9;
        EnemyTechController.instance.Combo10 = enemyUnit.Combo10;
        EnemyTechController.instance.SetCombo(1);
    }


    IEnumerator EndTurn()
    {

        dialogueText.text = "Turn over";


        // wait 2 seconds
        yield return new WaitForSeconds(2f);


        //check if that hit killed the enemy, if so trigger the WON state, if it didnt trigger the ENEMY TURN state.
        if(enemyUnit.currentResolve <= 0)
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
        dialogueText.text = " Enemy turn! ";

        yield return new WaitForSeconds(1f);

        bool isDead = false;

        for (int i = 0; i < EnemyTechController.instance.HandTechs.Length; i++)
        {
            Debug.Log("enemy using TECH!");
            EnemyTechController.instance.UseTech();


            yield return new WaitForSeconds(2f);
        }


        if(playerUnit.currentResolve <= 0)
        { isDead = true; }else
        { isDead = false; }


        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {

            state = BattleState.COMBAT;
            StartCoroutine(EnemyCombat());
            StartCoroutine(PlayerCombat());
        }
    }
 


    IEnumerator PlayerCombat()
    {


        yield return new WaitForSeconds(1f);

        bool isDead = false;

        for (int i = 0; i < playerUnit.HitsPerRound; i++)
        {
            Debug.Log("player attack!" + playerUnit.HitsPerRound);
            isDead = enemyUnit.TakeDamage(playerUnit.Power);

            enemyHUD.SetHP(enemyUnit.currentResolve);

            yield return new WaitForSeconds(8f/ playerUnit.HitsPerRound);
        }


        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            //set enemies next techs
            EnemyTechController.instance.SetCombo(Random.Range(1, enemyUnit.numberOfCombos));
            state = BattleState.PLAYERTURN;
            TechController.instance.DrawCard(3);
            playerTurn();
        }
    }

    IEnumerator EnemyCombat()
    {
        dialogueText.text = " Combat! ";

        yield return new WaitForSeconds(1f);

        bool isDead = false;

        for (int i = 0; i < enemyUnit.HitsPerRound; i++)
        {
            Debug.Log("enemy attack!" + enemyUnit.HitsPerRound);
            isDead = playerUnit.TakeDamage(enemyUnit.Power);

            playerHUD.SetHP(playerUnit.currentResolve);

            yield return new WaitForSeconds(8f / enemyUnit.HitsPerRound);
        }


        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
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
            enemyHUD.SetHP(enemyUnit.currentResolve);

        }
        if (lastTechPlayed.restoresHP)
        {
            playerUnit.Heal(lastTechPlayed.healingAmount);
            playerHUD.SetHP(playerUnit.currentResolve);
        }
    }

    public void EnemyTechPlayed(Techs lastTechPlayed)
    {
        //spend energy
        //playerUnit.SpendEnergy(lastTechPlayed.EnergyCost);
        //update energy HUD

        if (lastTechPlayed.doesDamage)
        {
            playerUnit.TakeDamage(lastTechPlayed.damageAmount);
            playerHUD.SetHP(playerUnit.currentResolve);

        }
        if (lastTechPlayed.restoresHP)
        {
            enemyUnit.Heal(lastTechPlayed.healingAmount);
            enemyHUD.SetHP(enemyUnit.currentResolve);
        }
    }
}
