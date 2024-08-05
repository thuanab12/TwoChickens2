using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T instance;
    public static T Instance { get { return instance; } }

    protected virtual void Awake()
    {
        // Check if the instance already exists
        if (instance != null && this.gameObject != null)
        {
            Destroy(this.gameObject);
            return; // Exit the method if we destroy the object
        }
        else
        {
            instance = (T)this;
        }

        // Make sure the object persists across scenes
        if (!gameObject.transform.parent)
        {
            DontDestroyOnLoad(gameObject);
        }

        // Destroy the instance if we are in Scene 1
        if (SceneManager.GetActiveScene().buildIndex ==  1) // Assuming Scene 1 has build index 1
        {
            Destroy(gameObject);
           instance = null; // Reset the instance to null
        }
    }
}
