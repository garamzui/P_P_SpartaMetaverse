using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �÷��̾� ���� ��Ʈ�ѷ�
/// - �Է� ó��
/// - �̵�/����/���� ����
/// - ���� ȸ�� �� ĳ���� ���� ����
/// - �ִϸ��̼� ����
/// - �̱������� �ν��Ͻ� ����
/// </summary>
public class PlayerController : BaseController
{
    /// <summary> �������� ���� ������ �ν��Ͻ� (�̱���) </summary>
    public static PlayerController Instance { get; private set; }

    // �Է°� ����
    private float moveX;
    private float moveY;
    private Vector2 moveInput;

    // ���� ȸ�� ������ (���� �ǹ�)
    [SerializeField] private Transform mainPivot;
    [SerializeField] private Transform weaponPivotRight;
    [SerializeField] private Transform weaponPivotLeft;
    private Transform currentWeaponPivot;


    [Header("���� ����")]
    [SerializeField] private float jumpCooldown = 1f; // ���� ����
    private float lastJumpTime = -999f; // ������ ���� �ð� ���

    [Header("���� ����")]
    [SerializeField] private Weapon weaponRight; // ������ ����
    [SerializeField] private Weapon weaponLeft;  // �޼� ����
    [SerializeField] private float attackInterval = 0.2f; // ���� ���� ����
    private float lastAttackTime = -999f; // ������ ���� �ð�
    private bool isRightHandNext = true;  // ���� ������ ��

    // �ִϸ��̼� ����� ��Ʈ�ѷ�
    private AnimatorController animatorController;

    [SerializeField] private int skillCost = 10; // ��ų MP �Ҹ�
    [SerializeField] private float skillCooldown = 1f;   // ��ų ��Ÿ�� (1��)
    private float lastSkillTime = -999f;                 // ������ ��ų ��� �ð�

    private void Start()
    {
        status.Init(this);
        PlayerUI.Instance.Init(status);
    }


    /// <summary>
    /// Awake: ������Ʈ �ʱ�ȭ �� �̱��� ����
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        animatorController = GetComponentInChildren<AnimatorController>();

        // �̱��� �ν��Ͻ� ����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �� �ı� ����
        }
        else
        {
            Destroy(gameObject); // �ߺ� �ν��Ͻ� ����
        }
    }

    /// <summary>
    /// Update: �����Ӹ��� ȣ��, �Է� ó�� �� �ִϸ��̼� ���� ���
    /// </summary>
    private void Update()
    {
        HandleInput();         // �̵� �Է� ����
        HandleAnimation();     // �̵� �ִϸ��̼� ó��
        HandleWeaponRotation(); // ���� �ǹ� ȸ��
        HandleSpriteFlip();    // ĳ���� �¿� ����
        HandleJump();          // ���� �Է� ó��
        HandleAttack();        // ���� �Է� �� ��Ÿ ó��
    }

    /// <summary>
    /// FixedUpdate: ���� �ð����� ȣ��Ǹ�, ���� ��� �̵� ó��
    /// </summary>
    private void FixedUpdate()
    {
        Vector2 input = new Vector2(moveX, moveY);
        Move(input); // BaseController�� �̵� ó�� ȣ��
    }

    /// <summary>
    /// �̵� Ű �Է��� �޾� ���� ���� ���
    /// </summary>
    private void HandleInput()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        // Ⱦ��ũ�� ����� ��� Y�� �̵� ����
        if (GameManager.Instance.IsSideScroll)
            moveY = 0f;

        moveInput = new Vector2(moveX, moveY).normalized;
    }

    /// <summary>
    /// �̵� �� ���ο� ���� �ִϸ��̼� �Ķ���� ����
    /// </summary>
    private void HandleAnimation()
    {
        bool isMoving = moveX != 0 || moveY != 0;
        animatorController.SetMove(isMoving);
    }


    /// <summary>
    /// ĳ���� �̵� ���⿡ ���� ���� ȸ�� ó��
    /// </summary>
    private void HandleWeaponRotation()
    {
        if (moveInput != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveInput.y, moveInput.x) * Mathf.Rad2Deg;

            // ĳ���Ͱ� ������ ���� ���� �� ȸ�� ���� ����
            if (moveX < 0)
                angle += 180f;

            weaponPivotRight.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            weaponPivotLeft.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    /// <summary>
    /// ĳ���� �¿� ���� (flipX) ó��
    /// ���⿡�� �������� ����
    /// </summary>
    private void HandleSpriteFlip()
    {
        if (moveX != 0)
            animatorController.SetFlip(moveX < 0);
        if (moveX != 0)
            mainPivot.localScale = new Vector3(moveX < 0 ? -1f : 1f, 1f, 1f); // flipX ȿ��
    }

    /// <summary>
    /// ���� Ű �Է� ���� �� ��Ÿ�� Ȯ�� �� ���� ����
    /// </summary>
    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.C) && Time.time >= lastJumpTime + jumpCooldown)
        {
            animatorController.SetJumpTrigger();
            lastJumpTime = Time.time;
        }
    }

    /// <summary>
    /// ���� Ű ���� �� ���� �������� ������ ���� ���� ���
    /// </summary>
    private void HandleAttack()
    {
        if (Input.GetKey(KeyCode.X) && Time.time >= lastAttackTime + attackInterval)
        {
            if (isRightHandNext)
                weaponRight.Use();
            else
                weaponLeft.Use();

            isRightHandNext = !isRightHandNext;
            lastAttackTime = Time.time;
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (Time.time >= lastSkillTime + skillCooldown)
            {
                if (status.UseMP(skillCost))
                {
                    int damage = status.Attack * 3; //  ��ų ������ ���
                    Debug.Log($"��ų �ߵ�! ������: {damage}");

                    lastSkillTime = Time.time;

                    // ���߿� ���⼭ ������ damage �����ϸ� ��
                    // ex: targetEnemy.TakeDamage(damage);
                }
                else
                {
                    Debug.Log("���� ����! ��ų ����");
                }
            }
            else
            {
                Debug.Log("��Ÿ�� ��! �ߵ� �Ұ�");
            }
        }
    }

}