
using System.Collections.Generic;
using ScriptTableObject;
using UnityEngine;

public class LevelManger : MonoBehaviour
{
    private void Start()
    {
        LoadLevel();
    }
    public void LoadLevel()
    {
        Instantiate(Data.instance.LevelData.levels[Data.instance.Stage].map);
    }
    
   
}
