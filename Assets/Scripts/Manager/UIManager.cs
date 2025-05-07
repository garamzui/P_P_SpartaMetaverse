using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
   
    public static UIManager Instance; //싱글톤 작성
    [Header("버튼 및 판넬")]
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


    [Header("슬라이더 연결")]
    [SerializeField] private Slider hpSlider;
    [SerializeField] private Slider mpSlider;

    [Header("수치 텍스트")]
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI mpText;

   
   

    /// <summary>
    /// 스탯 데이터 연결 및 초기 슬라이더 설정
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
    /// 슬라이더 위에 현재 HP/MP 수치 표시
    /// </summary>
    /// 
    [Header("미니게임용 패널")]
    public TextMeshProUGUI currentScore;
    public TextMeshProUGUI bestScoreText;

    private void UpdateText()
    {
        hpText.text = $"{PlayerController.Instance.Status.CurrentHP} / {PlayerController.Instance.Status.MaxHP}";
        mpText.text = $"{PlayerController.Instance.Status.CurrentMP} / {PlayerController.Instance.Status.MaxMP}";
    }

    //미니게임용 스코어 매서드

    public void UpdateScore(int score,int bscore)
    {
        currentScore.text = score.ToString();
        bestScoreText.text = bscore.ToString();
    }
    
    
}
