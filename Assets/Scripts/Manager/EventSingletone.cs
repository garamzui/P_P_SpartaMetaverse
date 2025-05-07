using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSingletone : MonoBehaviour
{
     public static EventSingletone Instance; //ΩÃ±€≈Ê ¿€º∫


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
