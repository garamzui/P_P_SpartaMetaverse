using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
   
    public static UIManager Instance; //�̱��� �ۼ�
    [Header("��ư �� �ǳ�")]
    public GameObject gameOverPanel;
    public GameObject restartbutton;
    public GameObject startPanel;
    public GameObject jumpGameStartButton;
   
    public GameObject exitButton;
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

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.Instance.Status == null) return;

        hpSlider.value = PlayerController.Instance.Status.CurrentHP;
        mpSlider.value = PlayerController.Instance.Status.CurrentMP;
        UpdateText();
    }


    public void JumpGameStart()
    {
        currentScore.gameObject.SetActive(true);
        bestScoreText.gameObject.SetActive(true);
        startPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void BackToMain()
    {
        GameManager.Instance.currentScore = 0;
        UpdateScore(0, GameManager.Instance.bestScore);
        GameManager.Instance.SetTopDownMode();
        SceneManager.LoadScene("MainScene");
        exitButton.SetActive(false);
        currentScore.gameObject.SetActive(false);
        bestScoreText.gameObject .SetActive(false);
            }
    
    public void ReStart()
    {
        Time.timeScale = 1f;
        PlayerController.Instance.Status.RecoverMP();
        PlayerController.Instance.Status.RecoverHP();
        gameOverPanel.SetActive(false);
        BackToMain();
        
    }


    [Header("�����̴� ����")]
    [SerializeField] private Slider hpSlider;
    [SerializeField] private Slider mpSlider;

    [Header("��ġ �ؽ�Ʈ")]
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI mpText;

   
   

    /// <summary>
    /// ���� ������ ���� �� �ʱ� �����̴� ����
    /// </summary>
    public void Init(StatusManager stats)
    {
        PlayerController.Instance.Status = stats;

        hpSlider.maxValue = PlayerController.Instance.Status.MaxHP;
        mpSlider.maxValue = PlayerController.Instance.Status.MaxMP;

        hpSlider.value = PlayerController.Instance.Status.CurrentHP;
        mpSlider.value = PlayerController.Instance.Status.CurrentMP;
        UpdateText();
    }




    /// <summary>
    /// �����̴� ���� ���� HP/MP ��ġ ǥ��
    /// </summary>
    /// 
    [Header("�̴ϰ��ӿ� �г�")]
    public TextMeshProUGUI currentScore;
    public TextMeshProUGUI bestScoreText;

    private void UpdateText()
    {
        hpText.text = $"{PlayerController.Instance.Status.CurrentHP} / {PlayerController.Instance.Status.MaxHP}";
        mpText.text = $"{PlayerController.Instance.Status.CurrentMP} / {PlayerController.Instance.Status.MaxMP}";
    }

    //�̴ϰ��ӿ� ���ھ� �ż���

    public void UpdateScore(int score,int bscore)
    {
        currentScore.text = score.ToString();
        bestScoreText.text = bscore.ToString();
    }
    
    
}
