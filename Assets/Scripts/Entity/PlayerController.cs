using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �÷��̾� ���� ��Ʈ�ѷ�
/// �Է��� �޾� �̵� ó�� �� �̱������� �ν��Ͻ��� ������
/// </summary>
public class PlayerController : BaseController
{
    public static PlayerController Instance { get; private set; }

    private float moveX;
    private float moveY;

    [SerializeField] private float jumpCooldown = 1f;  // ���� ��Ÿ�� 1��
    private float lastJumpTime = -999f;

   

    private AnimatorController animatorController;

    /// <summary> �̱��� �� DontDestroy ó�� </summary>

    protected override void Awake()
    {
        animatorController = GetComponentInChildren<AnimatorController>();

        base.Awake();

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // �� �̵� �� �ı� ����
        }
        else
        {
            Destroy(gameObject);  // �ߺ� ����
        }

       
    }

    private void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        // Ⱦ��ũ�� ���̶�� Y�� �Է��� ���� (���Ʒ� �̵� �Ұ�)
        if (GameManager.Instance.isSideScroll)
        {
            moveY = 0f;
        }

        // �̵� �ִϸ��̼� Ʈ����
        animatorController.SetMove(moveX != 0 || moveY != 0);

        // �¿� ���� ���� ó��
        if (moveX != 0)
        {
            animatorController.SetFlip(moveX < 0);
        }

        // ���� �Է� �� ��Ÿ�� �˻�
        if (Input.GetKeyDown(KeyCode.C) && Time.time >= lastJumpTime + jumpCooldown)
        {
            animatorController.SetJumpTrigger();       // ���� �ִϸ��̼� ����
            lastJumpTime = Time.time;                  // ��Ÿ�� ����
        }
    }

    /// <summary> �̵� ó�� (���� ���� ��� �̵�) </summary>
    private void FixedUpdate()
    {
        Vector2 input = new Vector2(moveX, moveY);
        Move(input);  // BaseController�� Move ȣ��
    }

   
}