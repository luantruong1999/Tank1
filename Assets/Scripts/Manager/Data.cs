using System.Collections.Generic;
using UnityEngine;
using ScriptTableObject;

public class Data : MonoBehaviour
{
    public static Data instance;
    [SerializeField]private LevelData _levelData;
    public LevelData LevelData => _levelData;
    private int stage=0;
    
    [SerializeField]private PowerUp PowerData;
    
    [SerializeField] private PlayerData _playerData;
    public PlayerData PlayerData => _playerData;
    
    [SerializeField] private EnemyData _enemyData;
    public EnemyData EnemyData => _enemyData;
    public int Stage
    {
        get => stage;
    }
    private void Awake()
    {
        if (instance == this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    public void NextStage()
    {
        stage++;
    }
    
}
