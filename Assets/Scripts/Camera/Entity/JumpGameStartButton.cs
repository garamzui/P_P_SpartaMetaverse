using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpGameStartButton : MonoBehaviour
{
    // Start is called before the first frame update
    public void OneClick   ()

    {
        JumpGameManager.Instance.JumpGameStart ();
    }
}
