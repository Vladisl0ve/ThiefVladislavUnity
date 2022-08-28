using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] GameObject[] enemies;
    Vector3[] initialPos;

    private void Awake()
    {
        initialPos = new Vector3[enemies.Length];
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
                initialPos[i] = enemies[i].transform.position;
        }
    }

    public void ActivateRoom(bool status)
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                enemies[i].SetActive(status);
                enemies[i].transform.position = initialPos[i];
            }
        }
    }
}
