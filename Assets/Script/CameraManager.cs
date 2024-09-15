using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    //storycamera
    public Transform StoryCamera;

    public static CameraManager instance;
    private void Awake()
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

    public void AddUItoCamera(RectTransform rect)
    {
        rect.parent = Camera.current.transform;
    }

}
