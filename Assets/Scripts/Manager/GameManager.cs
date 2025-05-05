using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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


   


    private void OnEnable()//���� �ٲ� ������ �ڵ����� �˸��޵��� �̺�Ʈ ���
    {
        // �� �ε� �̺�Ʈ ��� (���� �ٲ� ������ OnSceneLoaded ȣ���)
        SceneManager.sceneLoaded += OnSceneLoaded;
        
    }
    private void OnDisable() //������Ʈ�� ������ �̺�Ʈ ���� (�� �ϸ� �޸� ���� ����)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)//�� �̸��� �����ؼ�, � ���� �������� ����
    {
        switch (scene.name)
        {
            case "MainMap":
                SetTopDownMode();     // ž�� ���
                break;

            case "Dungeon":
                SetTopDownMode();  // Ⱦ��ũ�� ���
                break;
            case "JumpGame":
                SetSideScrollMode();
                break;
        }
    }
    private bool isSideScroll = false;                   // ���� ���� Ⱦ��ũ������ �Ǵ�
    public bool IsSideScroll { get; set; }

    public void SetTopDownMode()//ž�� ����� �� ��ġ�� �߷� ����
    {
        var player = PlayerController.Instance;

        if (player != null)
        {
            player.transform.position = new Vector2(0, 0);      // ���� ��ġ ����
            player.GetComponent<Rigidbody2D>().gravityScale = 0f; // �߷� ����
            IsSideScroll = false;
        }
    }
    


    private void SetSideScrollMode() //Ⱦ��ũ�� ���� �� �Ʒ�ó�� �߷��� ���ϰ� �ְ� ��ġ�� �ణ �����
    {
        var player = PlayerController.Instance;

        if (player != null)
        {
            player.transform.position = new Vector2(0, 2);      // �ణ ������ ����
            player.GetComponent<Rigidbody2D>().gravityScale = 20f; // �߷� ����
            IsSideScroll = true;
        }
    }



   
}
