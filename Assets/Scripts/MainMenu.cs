using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
// <summary>trapMat material.</summary>
public Material trapMat;

// <summary>goalMat material.</summary>
public Material goalMat;

// <summary>Colorblind checkbox.</summary>
public Toggle colorblindMode;


    public void PlayMaze()
    {
        if (colorblindMode.isOn)
        {
            trapMat.color = new Color32(255, 112, 0, 1);
            goalMat.color = Color.blue;
        }
        else
        {
           trapMat.color = Color.red;
           goalMat.color = Color.green;
        }
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public void QuitMaze()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }
}
