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

      
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            HealPlayer();
        }

    }
    public GameObject healCanvas;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (healCanvas != null)
                healCanvas.SetActive(true);
        }
        
        if (other.CompareTag("Attack"))
        {
            animatorController.SetNPCHitTrigger();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (healCanvas != null)
                healCanvas.SetActive(false);
        }
    }

    private void HealPlayer()
    {
        Debug.Log("Èú ¿Ï·á");
        animatorController.SetNPCUseHealTrigger();
    }
}
