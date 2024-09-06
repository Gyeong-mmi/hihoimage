using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_object : MonoBehaviour
{
    public static Manager_object instance = null;
    public GameObject Launcher;
    public GameLauncher_ICT Glauncher_ICT;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
                Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Launcher = this.gameObject;
        Glauncher_ICT = this.GetComponent<GameLauncher_ICT>();
    }

}
