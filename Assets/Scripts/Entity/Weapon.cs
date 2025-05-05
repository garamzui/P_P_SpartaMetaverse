using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private AnimatorController animatorController;
    private void Awake()
    {
        animatorController = GetComponent<AnimatorController>();

    }
    public void Use()
    {
        animatorController.SetAttackTrigger();
        Debug.Log($"{gameObject.name} 공격 실행!");
        StartCoroutine(SwingMotion());
    }

    private IEnumerator SwingMotion()
    {
        Vector3 origin = transform.localPosition;
        Vector3 swing = origin + new Vector3(0.2f, 0.1f, 0); // 예시

        transform.localPosition = swing;
        yield return new WaitForSeconds(0.1f);
        transform.localPosition = origin;
    }
}
