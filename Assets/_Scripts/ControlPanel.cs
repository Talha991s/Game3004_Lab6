using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class ControlPanel : MonoBehaviour
{
    public List<NavMeshAgent> agents;
    public List<MonoBehaviour> scripts;

    public PlayerBehaviour player;

    public bool IsGamePause = false;

    public GameObject PauseLabelPanel;

    public PlayerDataSO playerdata;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerBehaviour>();
        agents = FindObjectsOfType<NavMeshAgent>().ToList();

        foreach(var enemy in FindObjectsOfType<EnemyBehaviour>())
        {
            scripts.Add(enemy);
        }
        scripts.Add(player);
        scripts.Add(FindObjectOfType<CameraController>());

        LoadFromPlayerPreferences();
       // playerdata = FindObjectOfType<PlayerDataSO>()
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnLoadButtonPressed()
    {
        player.controller.enabled = false;
        player.transform.position = playerdata.PlayerPosition;
        player.transform.rotation = playerdata.PlayerRotation;
        player.health = playerdata.PlayerHealth;
        player.controller.enabled = true;
    }
    public void onSaveButtonPressed()
    {
        playerdata.PlayerPosition = player.transform.position;
        playerdata.PlayerRotation = player.transform.rotation;
        playerdata.PlayerHealth = player.health;

        SaveFromPlayerPreferences();
    }
    public void OnPauseButtonToggled()
    {
        IsGamePause = !IsGamePause;
        PauseLabelPanel.SetActive(IsGamePause);
        foreach(var agent in agents)
        {
            agent.enabled = !IsGamePause;
        }
        foreach(var script in scripts)
        {
            script.enabled = !IsGamePause;
        }
    }
    public void OnApplicationQuit()
    {
        SaveFromPlayerPreferences();
    }
    public void LoadFromPlayerPreferences()
    {
        playerdata.PlayerPosition.x= PlayerPrefs.GetFloat("PlayerPositionX");
        playerdata.PlayerPosition.y=PlayerPrefs.GetFloat("PlayerPositionY");
        playerdata.PlayerPosition.z=PlayerPrefs.GetFloat("PlayerPositionZ");

        playerdata.PlayerRotation.x= PlayerPrefs.GetFloat("PlayerPositionX");
        playerdata.PlayerRotation.y=PlayerPrefs.GetFloat("PlayerPositionY");
        playerdata.PlayerRotation.z= PlayerPrefs.GetFloat("PlayerPositionZ");
        playerdata.PlayerRotation.w =PlayerPrefs.GetFloat("PlayerPositionW");

        playerdata.PlayerHealth= PlayerPrefs.GetInt("PlayerHealth");
    }
    public void SaveFromPlayerPreferences()
    {
        Debug.Log(playerdata.PlayerPosition.ToString());


        PlayerPrefs.SetFloat("PlayerPositionX", playerdata.PlayerPosition.x);
        PlayerPrefs.SetFloat("PlayerPositionY", playerdata.PlayerPosition.y);
        PlayerPrefs.SetFloat("PlayerPositionZ", playerdata.PlayerPosition.z);

        PlayerPrefs.SetFloat("PlayerPositionX", playerdata.PlayerRotation.x);
        PlayerPrefs.SetFloat("PlayerPositionY", playerdata.PlayerRotation.y);
        PlayerPrefs.SetFloat("PlayerPositionZ", playerdata.PlayerRotation.z);
        PlayerPrefs.SetFloat("PlayerPositionW", playerdata.PlayerRotation.w);

        PlayerPrefs.SetInt("PlayerHealth", playerdata.PlayerHealth);
        PlayerPrefs.Save();
    }
}
