using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class JumpGameManager : MonoBehaviour
{
    public static JumpGameManager Instance; //�̱��� �ۼ�

    public GameObject StartPanel;
    private void Awake()
    {
         
        if (Instance == null)
        {
            Instance = this;
            
        }
        else
        {
            Destroy(gameObject);   //�ߺ����� ����  
        }
        if (StartPanel == null)
        { 
            Debug.Log("��ŸƮ�ǳ��� ���̿�");
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
