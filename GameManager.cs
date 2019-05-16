/*
 * **** - If we make multiplayer, should we move the money manager on the player directly?
 * 
 * 
 *   GameManager is the central hub for other managers
 *
 *   Begins & ends game
 *
 *   Can be used to access other managers, etc.
 *   
 *   
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject levelGenerator;

    public GameObject pauseMenu;
    
    public GameObject mainMenu;

    public GameObject teamDeltaLogo;

    public GameObject player;

    #region OtherComponents


    private int currentMoney;                            // Total amount of money player has gained
                                                         // Used for store purchases

    private int highScore;                               // Best score on any run

    #endregion


    // Creates turret spawn points, start run score at 0
    void Awake()
    {
        // Freezes game before playing

        DisplayTeamLogo();
    }


    #region BeginGame

    protected void DisplayTeamLogo()
    {
        Invoke("BeginGamePrep", 3f);
    }

    protected IEnumerator DisplayTeamLogoFade()
    {



        yield return null;
    }

    /// <summary>
    /// After level and player spawns, begin the game
    /// </summary>
    public void BeginGamePrep()
    {
        // Build Level - does level creator spawn the player, or do we do it on the next line?
        // If level creator spawns player, does that prevent from falling through the floor?
        mainMenu = Instantiate(mainMenu);

        pauseMenu = Instantiate(pauseMenu);

        BuildLevels();

        MakePlayer();

        SetupMainMenu();

        StartGame();
    }



    
    public void BuildLevels()
    {
        levelGenerator = Instantiate(levelGenerator);
    }

    public void MakePlayer()
    {
        LevelGenManualManager levelgen = levelGenerator.GetComponent<LevelGenManualManager>();

        levelgen.StartLevelBuilding();

        levelgen.SetDebugsOff();
        
        levelgen.MakePlayer();

        player = levelgen.player;

        Destroy(levelgen);
    }

    public void SetupMainMenu()
    {
        Camera cam = FindObjectOfType<Camera>();

        Vector3 newPos = player.transform.position;

        cam.transform.position = cam.transform.position + newPos;

        mainMenu.transform.position = new Vector3(newPos.x, newPos.y + 10, newPos.z);
    }


    /// <summary>
    /// Let's do this thing!
    /// </summary>
    public void StartGame()
    {
        // Allow time to move again
        Time.timeScale = 1;
    }

    #endregion


    #region Money Management

    /// <summary>
    /// Adds money for the player
    /// </summary>
    /// <param name="money"></param>
    public void MoneyAdded(int money)
    {
        currentMoney += money;
    }

    /// <summary>
    /// Checks if there's enough money to spend at a shop
    /// </summary>
    /// <param name="money"></param>
    /// <returns></returns>
    public bool MoneyRemoved(int money)
    {
        if (currentMoney < money)
            return false;

        currentMoney -= money;

        return true;
    }

    #endregion

    /// <summary>
    /// Ends game
    /// </summary>
    public void GameOver()
    {
        

        // Check and set high score - maybe list the richest players?
    }
}
