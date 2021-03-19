using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PlayerData", menuName = "Data/PlayerData")]
public class PlayerDataSO : ScriptableObject
{
    [Header("Player Transform Properties")]
    public Vector3 PlayerPosition;
    public Quaternion PlayerRotation;

    [Header("Player Attributes")]
    public int PlayerHealth;

}
