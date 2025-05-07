using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCController : MonoBehaviour
{

    public float heal;
    private bool playerInRange = false;
    public Animator animator;
    private MyAnimatorController animatorController;
    private void Start()
    {

        animatorController = GetComponentInChildren<MyAnimatorController>();
        heal = PlayerController.Instance.MaxHP;
    }
    void Update()
    {


        if (playerInRange && Input.GetKeyDown(KeyCode.H))
        {
            HealPlayer();
        }
        else if (playerInRange && Input.GetKeyDown(KeyCode.B))
        { markBScore(); }
        else if (playerInRange && Input.GetKeyDown(KeyCode.T))
        { dialog(); }

    }
    public GameObject SelectInteraction;
    public GameObject BestScore;
    public TextMeshProUGUI BestScoreMark;
    public GameObject Dialog;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (SelectInteraction != null)
                SelectInteraction.SetActive(true);
        }

        if (other.CompareTag("Attack"))
        {
            animatorController.SetNPCHitrigger();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (SelectInteraction != null)
            {
                SelectInteraction.SetActive(false);
                Dialog.SetActive(false);
                BestScore.SetActive(false);
            }
        }
    }

    private void HealPlayer()
    {
        PlayerController.Instance.Status.RecoverMP();
        PlayerController.Instance.Status.RecoverHP();
        Debug.Log("Èú ¿Ï·á");
        animatorController.SetNPCUseHealTrigger();
    }

    private void dialog()
    {
        SelectInteraction.SetActive(false);
        Dialog.SetActive(true);
    }

    private void markBScore()
    {
        BestScoreMark.text = GameManager.Instance.bestScore.ToString();
        SelectInteraction.SetActive(false);
        BestScore.SetActive(true);
    }
}
