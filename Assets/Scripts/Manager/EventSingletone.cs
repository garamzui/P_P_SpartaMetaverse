using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSingletone : MonoBehaviour
{
     public static EventSingletone Instance; //�̱��� �ۼ�


    private void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);  
        }

    }
}
