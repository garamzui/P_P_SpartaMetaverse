using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  public static GameManager Instance; //�̱��� �ۼ�
    

    private void Awake()
    {
        Application.targetFrameRate = 60;
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); //�� ��ȯ�Ͽ��� �ı������ʰ� ���
        }
        else
        { 
            Destroy(gameObject);   //�ߺ����� ����  
        }
        uiManager = UIManager.Instance;
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
                UIManager.Instance.exitButton.SetActive(true);
                break;
            case "JumpGame":
                SetSideScrollMode();
                UIManager.Instance.exitButton.SetActive(true);
                break;
        }
    }
              // ���� ���� Ⱦ��ũ������ �Ǵ�
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
            player.transform.position = new Vector2(-5, 0);     
            player.GetComponent<Rigidbody2D>().gravityScale = 1f; // �߷� ����
            IsSideScroll = true;
            
        }
        UIManager.Instance.MiniGameOperationInstructions.SetActive(true);
        UIManager.Instance. OperationInstructions.SetActive(false);
        UIManager.Instance.startPanel.SetActive(true);
        Time.timeScale = 0f;


    }


    //�̴ϰ��ӿ� ���ھ�
    UIManager uiManager;
    public UIManager UIManager { get { return uiManager; } }
   public int currentScore { get; set; } 
    public int bestScore {  get; set; } 
    public void AddScore(int score)
    {
        currentScore += score;
        
        if (bestScore <= currentScore)
        { bestScore = currentScore; }
        
        
        Debug.Log("Score:" + currentScore);
        UIManager.UpdateScore(currentScore,bestScore);
     
    }
   


}
