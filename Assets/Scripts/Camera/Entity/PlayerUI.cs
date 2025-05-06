using UnityEngine;
using UnityEngine.UI;
using TMPro;
/// <summary>
/// �÷��̾� UI �̱���
/// - HP/MP �����̴� ǥ��,��ġǥ��
/// - �� ��ȯ �� �ı� ����
/// </summary>
public class PlayerUI : MonoBehaviour
{
    // �̱��� �ν��Ͻ�
    public static PlayerUI Instance { get; private set; }

    [Header("�����̴� ����")]
    [SerializeField] private Slider hpSlider;
    [SerializeField] private Slider mpSlider;
    
    [Header("��ġ �ؽ�Ʈ")]
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI mpText;
    
    private StatusManager playerStats; // (�Ʒ����� ����!)

    private void Awake()
    {
        // �ߺ� ����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �ı� ����
        }
        else
        {
            Destroy(gameObject); // �̹� ������ ���� ���� �� ����
        }
    }

    /// <summary>
    /// ���� ������ ���� �� �ʱ� �����̴� ����
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
    /// �����̴� ���� ���� HP/MP ��ġ ǥ��
    /// </summary>
    private void UpdateText()
    {
        hpText.text = $"{playerStats.CurrentHP} / {playerStats.MaxHP}";
        mpText.text = $"{playerStats.CurrentMP} / {playerStats.MaxMP}";
    }
}