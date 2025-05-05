using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ĳ������ ü��, ����, ���ݷ�, �̵��ӵ� �� ������ �����ϴ� ���� ������ Ŭ����
/// MonoBehaviour�� ������� �����Ƿ� new�� ���� �����ؾ� ��
/// </summary>
[System.Serializable]
public class StatusManager
{
    //�⺻ �ɷ�ġ
    [SerializeField] private int maxHP = 100;           // �ִ� ü��
    [SerializeField] private int maxMP = 50;            // �ִ� ����
    [SerializeField] private int attack = 10;           // ���ݷ�
    [SerializeField] private int defense = 5;           // ����
    [SerializeField] private float moveSpeed = 5f;      // �̵� �ӵ�

    [HideInInspector] public int currentHP;             // ���� ü�� (��Ÿ�ӿ�)
    [HideInInspector] public int currentMP;             // ���� ���� (��Ÿ�ӿ�)

    /// <summary> ���� ���� �� ȣ���Ͽ� ���� ü��/���� �ʱ�ȭ </summary>
    public void Init()
    {
        currentHP = maxHP;
        currentMP = maxMP;
    }

    // �б� ���� ������Ƽ��
    public float MoveSpeed => moveSpeed;
    public int Attack => attack;
    public int Defense => defense;
    public int MaxHP => maxHP;
    public int MaxMP => maxMP;

    /// <summary> �������� �޾� ü���� ���ҽ�Ű�� ������ ���� </summary>
    public void TakeDamage(int dmg)
    {
        int finalDmg = Mathf.Max(1, dmg - defense);
        currentHP -= finalDmg;
        Debug.Log($"[Status] ���� {finalDmg} �� ���� ü��: {currentHP}");

        if (currentHP <= 0)
        {
            Die();
        }
    }

    /// <summary> ü�� 0 ������ �� ȣ��Ǵ� ���� ��� ó�� </summary>
    private void Die()
    {
        Debug.Log("[Status] ��� ó��");
        // ���� ��� ó���� ��Ʈ�ѷ� �ʿ��� ���� (�̺�Ʈ ��)
    }
}