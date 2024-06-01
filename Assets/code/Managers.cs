using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    // Start is called before the first frame update
    static Managers s_instance;
    static Managers Instance { get { Init(); return s_instance; } }
    SaveDataManager _saveDataManager;
    public static SaveDataManager SaveData { get { return Instance._saveDataManager; } }

    
    



    private void Awake()
    {
        Init();
        
    }

    void Start()
    {
        _saveDataManager = gameObject.GetComponent<SaveDataManager>();
        
    }



    static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("Managers");
            if (go == null)
            {
                go = new GameObject { name = "Managers" };
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();
        }
    }
}
