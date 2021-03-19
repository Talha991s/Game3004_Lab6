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
}
