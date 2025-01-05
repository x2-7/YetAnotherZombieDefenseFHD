using System;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Serializable]
    private struct Enemy
    {
        public GameObject GameObject;
    }

    [SerializeField]
    private Enemy[] enemyObjs;

    public int CurrentWave { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
