using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class AndroidNativeBridge : MonoBehaviour
{
    [SerializeField] private Board _board;
    
    public void Restart(string aMessage)
    {
        if (_board.IsGameOver)
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.buildIndex);
        }
    }
}
