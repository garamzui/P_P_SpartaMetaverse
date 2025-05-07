using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class JumpGameManager : MonoBehaviour
{
    public static JumpGameManager Instance; //싱글톤 작성

    public GameObject StartPanel;
    private void Awake()
    {
         
        if (Instance == null)
        {
            Instance = this;
            
        }
        else
        {
            Destroy(gameObject);   //중복생성 방지  
        }
        if (StartPanel == null)
        { 
            Debug.Log("스타트판넬이 널이여");
            return;
        }
        Time.timeScale = 0f;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void JumpGameStart()
    { 
    
    StartPanel.SetActive(false);
        Time.timeScale = 1f;
    }



}
