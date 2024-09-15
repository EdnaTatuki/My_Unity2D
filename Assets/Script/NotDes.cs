using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotDes : MonoBehaviour
{
    private static NotDes instance;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
