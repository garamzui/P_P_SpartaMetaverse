using System.Collections;
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


    [Header("ž�� ���� ���� ����")]
    
    private float lastJumpTime = -999f; // ������ ���� �ð� ���
    [Header("Ⱦ��ũ�� ���� ��� ���� �ý���")]
    [SerializeField] private Transform groundCheck;      // �ٴ� ���� ��ġ
    [SerializeField] private float groundCheckRadius = 0.1f; // �ٴ� üũ ����
    [SerializeField] private LayerMask groundLayer;      // �ٴ� ���̾�
    [SerializeField] private float jumpPower = 50f;       // ���� ��
    [SerializeField] private int maxJumpCount = 2;
    private int currentJumpCount = 0;
   




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
        UIManager.Instance.Init(status);
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
                            
        HandleAttack();        // ���� �Է� �� ��Ÿ ó��

    }

    /// <summary>
    /// FixedUpdate: ���� �ð����� ȣ��Ǹ�, ���� ��� �̵� ó��
    /// </summary>
    private void FixedUpdate()
    {
        Vector2 input = new Vector2(moveX, moveY);


        if (!GameManager.Instance.IsSideScroll) // Move�� ����� Y���� ��� �ʱ�ȭ�ؼ� ���� ������ ������ ���� ���⼭ ����ó����
        {
            Move(input); // BaseController�� �̵� ó�� ȣ��
       
        }
        else
        {
            // �˹� �߿� AutoRun ��� �ߴ�
            if (!isKnockback)
            {
                AutoRun();
            }
           
        }
        //�˹� Ÿ�̸� ���� 
        if (isKnockback)
        {
            knockbackTimer -= Time.fixedDeltaTime;
            if (knockbackTimer <= 0f)
            {
                isKnockback = false;
            }
        }





        HandleJump();   // ���� �Է� ó��
        if (GameManager.Instance.IsSideScroll)
        {
            if (Mathf.Abs(rb.velocity.y) < 0.01f && currentJumpCount > 0)
            {
                currentJumpCount = 0;
                animatorController.SetRJump(false);
                Debug.Log("����!");
            }
        }
    }

    /// <summary>
    /// �̵� Ű �Է��� �޾� ���� ���� ���
    /// </summary>
    private void HandleInput()
    {
        if (GameManager.Instance.IsSideScroll)
        {
            // �Է� ���� (�÷��̾�� �ڵ����� ������ �̵�)
            moveX = 1f;   // ��� ���������� �̵�

        }
        else
        {
            // ���� �Է� ���� (ž�ٿ� ���)
            moveX = Input.GetAxisRaw("Horizontal");
            moveY = Input.GetAxisRaw("Vertical");
        }

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
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (GameManager.Instance.IsSideScroll)
            {
                if (currentJumpCount < maxJumpCount)
                {
                    //Vector3 v = rb.velocity;
                    //v.y +=  jumpPower;
                    //rb.velocity = v;
                    rb.velocity = new Vector2(rb.velocity.x, 0f); // Y �ӵ� �ʱ�ȭ
                    rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

                    currentJumpCount++;
                    animatorController.SetRJump(true);
                    Debug.Log($"���� {currentJumpCount}/{maxJumpCount}");
                }
            }
            else
            {
                // ž�� ���: ���� ����
                animatorController.SetJumpTrigger();
                lastJumpTime = Time.time;
                Debug.Log("���� ����");
            }
        }
    }
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
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
                    // ��ų ������ ���
                    int damage = status.Attack * 3;
                    Debug.Log($"��ų �ߵ�! ������: {damage}");


                    
                    weaponLeft.UseSkill();
                    weaponRight.UseSkill();



                    isRightHandNext = !isRightHandNext;
                    lastSkillTime = Time.time;
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


    //�̴ϰ��ӿ� �浹 ����� �� �˹� ����
    private bool isKnockback = false;
    private float knockbackDuration = 0.5f;
    private float knockbackTimer = 0f;
    private void ApplyKnockback()
    {
        Vector2 knockbackForce = new Vector2(-10f, 5f);
        rb.velocity = Vector2.zero;
        rb.AddForce(knockbackForce, ForceMode2D.Impulse);
        animatorController.SetCrashTrigger();
        isKnockback = true;
        knockbackTimer = knockbackDuration;

        Debug.Log("�˹�!");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("HitZone"))
        {
            ApplyDamage(25); // ���� ����� or ��ֹ� �Ӽ��� ����
        }
    }
    public void ApplyDamage(int dmg)
    {
        
            status.TakeDamage(dmg); // �� �ʰ� ���� ��� StatusManager�� TakeDamage ȣ��
            ApplyKnockback();
        
    }
}