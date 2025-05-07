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
    // ������������������������������������������������������������
    // �⺻ �ɷ�ġ (�����̳ʰ� ���� ����)
    // ������������������������������������������������������������

    [SerializeField] private int maxHP = 100;           // �ִ� ü��
    [SerializeField] private int maxMP = 50;            // �ִ� ����
    [SerializeField] private int attack = 10;           // ���ݷ�
   
    [SerializeField] private float moveSpeed = 5f;      // �̵� �ӵ�

    // ���� ���� (��Ÿ�ӿ��� ����, Inspector������ ����)
    [HideInInspector] public int currentHP;             // ���� ü��
    [HideInInspector] public int currentMP;             // ���� ����

    // ������������������������������������������������������������
    // �ʱ�ȭ
    // ������������������������������������������������������������

    /// <summary>
    /// ���� ���� �� ȣ���Ͽ� ���� ü��/������ �ִ밪���� �ʱ�ȭ
    /// </summary>
    public void Init(MonoBehaviour owner)
    {
        currentHP = maxHP;
        currentMP = maxMP;

        // �ڵ� ȸ�� �ڷ�ƾ ����
        owner.StartCoroutine(AutoRecover());
    }

    // ������������������������������������������������������������
    // �ܺο��� ���� ������ �б� ���� ������Ƽ��
    // ������������������������������������������������������������

    /// <summary> �̵� �ӵ� </summary>
    public float MoveSpeed => moveSpeed;

    /// <summary> ���ݷ� </summary>
    public int Attack => attack;

    /// <summary> ���� </summary>
    

    /// <summary> �ִ� ü�� </summary>
    public int MaxHP => maxHP;

    /// <summary> �ִ� ���� </summary>
    public int MaxMP => maxMP;

    /// <summary> ���� ü�� (�б� ����) </summary>
    public int CurrentHP => currentHP;

    /// <summary> ���� ���� (�б� ����) </summary>
    public int CurrentMP => currentMP;

    // ������������������������������������������������������������
    // ������ ó��
    // ������������������������������������������������������������

    /// <summary>
    /// �������� �޾� ü���� ���ҽ�Ű�� ���� ���θ� Ȯ��
    /// </summary>
    /// <param name="dmg">���� ���ݷ�</param>
    public void TakeDamage(int dmg)
    {
       
        
        currentHP -= dmg;

        Debug.Log($"[Status] ���� {dmg} �� ���� ü��: {currentHP}");

        // ü���� 0 ���϶�� ��� ó��
        if (currentHP <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// ü�� 0 ������ �� ȣ��Ǵ� ���� ��� ó�� �Լ�
    /// ���� ���ӿ����� �� �Լ��� ������� �ִϸ��̼� �Ǵ� �̺�Ʈ ȣ��
    /// </summary>
    private void Die()
    {
       
        
        
        Debug.Log("[Status] ��� ó��");
        // ��� ó�� ������ �ܺ� ��Ʈ�ѷ� �Ǵ� �̺�Ʈ �ý��ۿ��� ���
    }
    /// <summary>
    /// �ڵ�ȸ�� �Լ�
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

            // HP 5�ʸ��� 1 ȸ��
            if (hpTimer >= 5f)
            {
                while (hpTimer >= 5f) // ����� ��� ���� �ʾƼ� ���� ���Ϲ� 
                {
                    hpTimer -= 5f;
                    if (currentHP < maxHP)
                        currentHP += 1;
                }
            }

            // MP 1�ʸ��� 1 ȸ��
            if (mpTimer >= 1f)
            {
                while (mpTimer >= 1f)
                {
                    mpTimer -= 1f;
                    if (currentMP < maxMP)
                    {
                        currentMP += 1;
                        Debug.Log("MP1ȸ��");
                    }
                }
            }
        }
    }

    /// <summary>
    /// MP�� �Ҹ��ϰ�, ������ ��� false ��ȯ
    /// </summary>
    public bool UseMP(int amount)
    {
        if (currentMP < amount)
        {
            Debug.Log("[Status] ���� ����!");
            return false;
        }

        currentMP -= amount;
        Debug.Log($"[Status] MP {amount} ��� �� ���� MP: {currentMP}");
        return true;
    }
}