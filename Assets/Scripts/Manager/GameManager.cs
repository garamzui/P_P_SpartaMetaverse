using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public static GameManager Instance; //싱글톤 작성
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); //씬 전환하여도 파괴하지않고 사용
        }
        else
        { 
            Destroy(gameObject);   //중복생성 방지  
        }
    }



    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
