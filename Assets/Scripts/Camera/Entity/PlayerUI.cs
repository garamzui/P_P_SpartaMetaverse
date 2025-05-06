using UnityEngine;
using UnityEngine.UI;
using TMPro;
/// <summary>
/// 플레이어 UI 싱글톤
/// - HP/MP 슬라이더 표시,수치표시
/// - 씬 전환 시 파괴 방지
/// </summary>
public class PlayerUI : MonoBehaviour
{
    // 싱글톤 인스턴스
    public static PlayerUI Instance { get; private set; }

    [Header("슬라이더 연결")]
    [SerializeField] private Slider hpSlider;
    [SerializeField] private Slider mpSlider;
    
    [Header("수치 텍스트")]
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI mpText;
    
    private StatusManager playerStats; // (아래에서 설명!)

    private void Awake()
    {
        // 중복 방지
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 파괴 방지
        }
        else
        {
            Destroy(gameObject); // 이미 있으면 새로 생긴 건 삭제
        }
    }

    /// <summary>
    /// 스탯 데이터 연결 및 초기 슬라이더 설정
    /// </summary>
    public void Init(StatusManager stats)
    {
        playerStats = stats;

        hpSlider.maxValue = playerStats.MaxHP;
        mpSlider.maxValue = playerStats.MaxMP;

        hpSlider.value = playerStats.CurrentHP;
        mpSlider.value = playerStats.CurrentMP;
        UpdateText();
    }

    private void Update()
    {
        if (playerStats == null) return;

        hpSlider.value = playerStats.CurrentHP;
        mpSlider.value = playerStats.CurrentMP;
        UpdateText();
    }


    /// <summary>
    /// 슬라이더 위에 현재 HP/MP 수치 표시
    /// </summary>
    private void UpdateText()
    {
        hpText.text = $"{playerStats.CurrentHP} / {playerStats.MaxHP}";
        mpText.text = $"{playerStats.CurrentMP} / {playerStats.MaxMP}";
    }
}