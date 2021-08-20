using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        restartGame();
    }

    public void restartGame()
    {
        Debug.Log("alpha");
        SceneManager.LoadScene("alpha");
    }

    public void congratulation()
    {
        Debug.Log("win");
        SceneManager.LoadScene("win");
    }
}
