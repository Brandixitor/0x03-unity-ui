using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{   
    /// <summary> Rigidbody of the player. </summary>
    public Rigidbody rb;
    
    /// <summary> Movement speed of the player. </summary>
    public float speed = 1000f;
    
    private int score; // Score of the player
    
    /// <summary> Health of the player. </summary>
    public int health = 5;

    /// <summary> Score of the player. </summary>
    public Text scoreText;

    /// <summary> Health of the player. </summary>
    public Text healthText;

    private GameObject WinLoseBG; /// Displays victory or lose.
    


    // Start is called before the first frame update
    void Start()
    {
      WinLoseBG = GameObject.Find("Canvas").transform.GetChild(2).gameObject;
    }
    
    
    
    
    // Update is called once per frame
    void Update()
    {
        esc_pause();
        /// Updates the score to the UI.
        SetScoreText();

        /// Updates the health to the UI.
        SetHealthText();


        // GetAxis Inputs.
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(x, 0, z).normalized;
        Vector3 force = dir * speed * Time.deltaTime; 
        rb.AddForce(force);



        // Condition to reset the score upon health reaches 0.
        if (health == 0)
        {
            /// Debug.Log("Game Over!"); /// Removed and replaced with a UI.
            health = 5;
            score = 0;
            Scene scene = SceneManager.GetActiveScene();
           /// SceneManager.LoadScene(scene.name);
            
        }

    }

    void OnTriggerEnter(Collider other) 
    {
        // Condition to add +1 to the score and destroy the coin upon touching it.
        if (other.tag == "Pickup")
        {
            score++;
            /// Debug.Log($"Score: {score}"); /// removed and replaced with a UI.
            Destroy(other.gameObject);
        }

        // Condition to manage the health of the player.
        if (other.tag == "Trap")
        {
            health--;
            /// Debug.Log($"Health: {health}"); /// removed and replaced with a UI.
            
            if (health == 0)
            {
            WinLoseBG.SetActive(true);
            WinLoseBG.transform.GetChild(0).GetComponent<Text>().text = "Game Over!";
            WinLoseBG.transform.GetChild(0).GetComponent<Text>().color = Color.white;
            WinLoseBG.GetComponent<Image>().color = Color.red;
            StartCoroutine(LoadScene(3));
            }
        }


        // Condition to let the player know that they've won upon touching the finish line.
        if (other.tag == "Goal")
        {
            /// Debug.Log("You win!"); /// removed and replaced with a UI.
            WinLoseBG.SetActive(true);
            WinLoseBG.transform.GetChild(0).GetComponent<Text>().text = "You Win!";
            WinLoseBG.transform.GetChild(0).GetComponent<Text>().color = Color.black;
            WinLoseBG.GetComponent<Image>().color = Color.green;
            StartCoroutine(LoadScene(3));
        }

    }



    void SetScoreText() /// Method to manage the score UI.
    {
        this.scoreText.text = $"score: {score}";
    }


    void SetHealthText() /// Method to manage the health UI.
    {
        this.healthText.text = $"health: {health}";
    }


/// <summary> Coroutine to reload the scene after number of seconds. </summary>
    /// <param name="seconds">Number of seconds before reloading the scene</param>
    /// <returns></returns>
    IEnumerator LoadScene(float seconds){
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void esc_pause()
    {
        if (Input.GetButton("Cancel") == true)
        { 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }

}
