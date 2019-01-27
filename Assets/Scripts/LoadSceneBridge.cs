using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneBridge : MonoBehaviour
{

    public void Load()
    {
        GameManager.Instance.StartGame();
    }
}
