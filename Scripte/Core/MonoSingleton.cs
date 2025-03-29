using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    static private T instance;

    static public T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<T>();

                /*if (instance == null)
                {
                    Debug.LogError("스크립트 안넣었어 멍청아");
                }*/
            }
            return instance;
        }
    }
}
