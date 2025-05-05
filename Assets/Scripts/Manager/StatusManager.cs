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
    //기본 능력치
    [SerializeField] private int maxHP = 100;           // 최대 체력
    [SerializeField] private int maxMP = 50;            // 최대 마나
    [SerializeField] private int attack = 10;           // 공격력
    [SerializeField] private int defense = 5;           // 방어력
    [SerializeField] private float moveSpeed = 5f;      // 이동 속도

    [HideInInspector] public int currentHP;             // 현재 체력 (런타임용)
    [HideInInspector] public int currentMP;             // 현재 마나 (런타임용)

    /// <summary> 게임 시작 시 호출하여 현재 체력/마나 초기화 </summary>
    public void Init()
    {
        currentHP = maxHP;
        currentMP = maxMP;
    }

    // 읽기 전용 프로퍼티들
    public float MoveSpeed => moveSpeed;
    public int Attack => attack;
    public int Defense => defense;
    public int MaxHP => maxHP;
    public int MaxMP => maxMP;

    /// <summary> 데미지를 받아 체력을 감소시키고 죽음을 판정 </summary>
    public void TakeDamage(int dmg)
    {
        int finalDmg = Mathf.Max(1, dmg - defense);
        currentHP -= finalDmg;
        Debug.Log($"[Status] 피해 {finalDmg} → 현재 체력: {currentHP}");

        if (currentHP <= 0)
        {
            Die();
        }
    }

    /// <summary> 체력 0 이하일 때 호출되는 내부 사망 처리 </summary>
    private void Die()
    {
        Debug.Log("[Status] 사망 처리");
        // 실제 사망 처리는 컨트롤러 쪽에서 진행 (이벤트 등)
    }
}