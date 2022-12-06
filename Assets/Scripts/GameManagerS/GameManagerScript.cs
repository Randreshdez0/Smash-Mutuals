using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManagerScript : MonoBehaviour
{
    private int playersLeft = 2;
    public float currentTime = 67f;

    public TextMeshProUGUI timeText;

    public GameObject endScreen;

    public bool hitboxesVisible = false;

    // Start is called before the first frame update
    void Start()
    {
        //Application.targetFrameRate = 60;
        //Turn on time panel   

        //currentTime = PlayerPrefs.GetInt("Time");
        SetGameSettings();

/*        GameObject[] hboxes = GameObject.FindGameObjectsWithTag("Hitbox");

        foreach (GameObject g in hboxes)
        {
            g.GetComponent<SpriteRenderer>().enabled = hitboxesVisible;
        }*/
    }
    private void Update()
    {
        if (PlayerPrefs.GetInt("Time") != -1) //Infinite Time
        {
            HandleTime(); 
        }

        DisplayTime(currentTime);

/*        if(Input.GetKeyDown(KeyCode.Z))
        {
            print("z");

            hitboxesVisible = !hitboxesVisible;

            GameObject[] hboxes = GameObject.FindGameObjectsWithTag("Hitbox");

            foreach (GameObject g in hboxes)
            {
                g.GetComponent<SpriteRenderer>().enabled = hitboxesVisible;
            }
        }
        */

    }

    // Lives checked in another script (Everytime a life is taken).
    public void HandleLives()
    {
        //if style is lives and players left is one and not training
        if (playersLeft == 1)
            EndGame();
    }

    private void CreatePlayers()
    {
        
    }

    private void HandleTime()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
        }
        if (currentTime <= 0 && currentTime != -1)
        {
            currentTime = 0;
            EndGame();
        }
    }
    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    private void EndGame()
    {
        //Play GAME end animation
        endScreen.SetActive(true);
        //Closing animation
        //Go to End Results
    }

    void SetGameSettings()
    {
        PlayerDamage[] p = FindObjectsOfType<PlayerDamage>();
        int livesForGame = 0;

        if (PlayerPrefs.GetInt("Style") == 0) //Lives
        {
            livesForGame = PlayerPrefs.GetInt("Lives");

            Debug.Log("Lives Battle with " + PlayerPrefs.GetInt("Lives") + " Lives");
        }
        if (PlayerPrefs.GetInt("Style") == 1) //Time
        {
            currentTime = PlayerPrefs.GetInt("Time") * 60;

            Debug.Log("Time Battle with " + PlayerPrefs.GetInt("Time") + " minutes");
        }

        foreach  (PlayerDamage i in p)
        {
            i.SetLives(livesForGame);
        }
    }
    public void RemovePlayer()
    {
        playersLeft--;
    }
}
