using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCController : MonoBehaviour
{
    
    public float heal;
    private bool playerInRange = false;
    public Animator animator;
    private AnimatorController animatorController;
    private void Start()
    {
        
        animatorController = GetComponent<AnimatorController>();
        heal = PlayerController.Instance.MaxHP;
    }
    void Update()
    {


        if (playerInRange && Input.GetKeyDown(KeyCode.H))
        {
            HealPlayer();
        }
        else if (playerInRange && Input.GetKeyDown(KeyCode.B))
        { }
        else if (playerInRange && Input.GetKeyDown(KeyCode.D))
        { }
    
    }
    public GameObject SelectInteraction;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (SelectInteraction != null)
                SelectInteraction.SetActive(true);
        }
        
        if (other.CompareTag("Attack") )
        {
            animatorController.SetNPCHitTrigger();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (SelectInteraction != null)
                SelectInteraction.SetActive(false);
        }
    }
   
    private void HealPlayer()
    {
        PlayerController.Instance.Status.RecoverMP();
        PlayerController.Instance.Status.RecoverHP();
        Debug.Log("Èú ¿Ï·á");
        animatorController.SetNPCUseHealTrigger();
    }
}
