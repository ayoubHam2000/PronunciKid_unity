using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class settings : MonoBehaviour
{
    public float progressIncreaseAmount = 0.1f;

    [HideInInspector] public sounds gameSounds = null;

    private void Start()
    {
        gameSounds = GetComponent<sounds>();
    }
}
