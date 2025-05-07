using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 캐릭터의 체력, 마나, 공격력, 이동속도 등 스탯을 관리하는 순수 데이터 클래스
/// MonoBehaviour를 상속하지 않으므로 new로 직접 생성해야 함
/// </summary>
[System.Serializable]
public class StatusManager
{
    // ──────────────────────────────
    // 기본 능력치 (디자이너가 조정 가능)
    // ──────────────────────────────

    [SerializeField] private int maxHP = 100;           // 최대 체력
    [SerializeField] private int maxMP = 50;            // 최대 마나
    [SerializeField] private int attack = 10;           // 공격력
   
    [SerializeField] private float moveSpeed = 5f;      // 이동 속도

    // 현재 상태 (런타임에만 사용됨, Inspector에서는 숨김)
    [HideInInspector] public int currentHP;             // 현재 체력
    [HideInInspector] public int currentMP;             // 현재 마나

    // ──────────────────────────────
    // 초기화
    // ──────────────────────────────

    /// <summary>
    /// 게임 시작 시 호출하여 현재 체력/마나를 최대값으로 초기화
    /// </summary>
    public void Init(MonoBehaviour owner)
    {
        currentHP = maxHP;
        currentMP = maxMP;

        // 자동 회복 코루틴 시작
        owner.StartCoroutine(AutoRecover());
    }

    // ──────────────────────────────
    // 외부에서 접근 가능한 읽기 전용 프로퍼티들
    // ──────────────────────────────

    /// <summary> 이동 속도 </summary>
    public float MoveSpeed => moveSpeed;

    /// <summary> 공격력 </summary>
    public int Attack => attack;

    /// <summary> 방어력 </summary>
    

    /// <summary> 최대 체력 </summary>
    public int MaxHP => maxHP;

    /// <summary> 최대 마나 </summary>
    public int MaxMP => maxMP;

    /// <summary> 현재 체력 (읽기 전용) </summary>
    public int CurrentHP => currentHP;

    /// <summary> 현재 마나 (읽기 전용) </summary>
    public int CurrentMP => currentMP;

    // ──────────────────────────────
    // 데미지 처리
    // ──────────────────────────────

    /// <summary>
    /// 데미지를 받아 체력을 감소시키고 죽음 여부를 확인
    /// </summary>
    /// <param name="dmg">받은 공격력</param>
    public void TakeDamage(int dmg)
    {
       
        
        currentHP -= dmg;

        Debug.Log($"[Status] 피해 {dmg} → 현재 체력: {currentHP}");

        // 체력이 0 이하라면 사망 처리
        if (currentHP <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// 체력 0 이하일 때 호출되는 내부 사망 처리 함수
    /// 실제 게임에서는 이 함수를 기반으로 애니메이션 또는 이벤트 호출
    /// </summary>
    private void Die()
    {
       
        
        
        Debug.Log("[Status] 사망 처리");
        // 사망 처리 로직은 외부 컨트롤러 또는 이벤트 시스템에서 담당
    }
    /// <summary>
    /// 자동회복 함수
    /// </summary>
    private IEnumerator AutoRecover()
    {
        float hpTimer = 0f;
        float mpTimer = 0f;

        while (true)
        {
            yield return null;

            hpTimer += Time.deltaTime;
            mpTimer += Time.deltaTime;

            // HP 5초마다 1 회복
            if (hpTimer >= 5f)
            {
                while (hpTimer >= 5f) // 제대로 계산 되지 않아서 넣은 와일문 
                {
                    hpTimer -= 5f;
                    if (currentHP < maxHP)
                        currentHP += 1;
                }
            }

            // MP 1초마다 1 회복
            if (mpTimer >= 1f)
            {
                while (mpTimer >= 1f)
                {
                    mpTimer -= 1f;
                    if (currentMP < maxMP)
                    {
                        currentMP += 1;
                        Debug.Log("MP1회복");
                    }
                }
            }
        }
    }

    /// <summary>
    /// MP를 소모하고, 부족할 경우 false 반환
    /// </summary>
    public bool UseMP(int amount)
    {
        if (currentMP < amount)
        {
            Debug.Log("[Status] 마나 부족!");
            return false;
        }

        currentMP -= amount;
        Debug.Log($"[Status] MP {amount} 사용 → 남은 MP: {currentMP}");
        return true;
    }
}