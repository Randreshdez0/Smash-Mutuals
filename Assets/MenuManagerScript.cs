using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuManagerScript : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Dropdown dropdown;
    [SerializeField] private TMPro.TMP_InputField livesInput;
    [SerializeField] private TMPro.TMP_InputField timeInput;
    public Scene Dune;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GoToGame()
    {
        SceneManager.LoadScene("MapSelect");
    }
    public void GoToDune()
    {
        SceneManager.LoadScene(Dune.ToString());
    }
    public void SetGameMode()
    {
        int val = dropdown.value;
        PlayerPrefs.SetInt("Style", val);
        Debug.Log(val + ", " + PlayerPrefs.GetInt("Style"));
        print("hello");
    }
    public void GoToMap(string map)
    {
        SceneManager.LoadScene(map);
    }

    public void SetLives()
    {
        PlayerPrefs.SetInt("Lives", int.Parse(livesInput.text));
    }
    public void SetTime()
    {
        PlayerPrefs.SetInt("Time", int.Parse(timeInput.text));
    }
}
