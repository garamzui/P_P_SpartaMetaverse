using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public static GameManager Instance; //�̱��� �ۼ�
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); //�� ��ȯ�Ͽ��� �ı������ʰ� ���
        }
        else
        { 
            Destroy(gameObject);   //�ߺ����� ����  
        }
    }



    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
