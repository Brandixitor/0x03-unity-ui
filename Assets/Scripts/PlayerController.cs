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
    


    // Update is called once per frame
    void Update()
    {
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
            Debug.Log("Game Over!");
            health = 5;
            score = 0;
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
            
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
            /// Debug.Log($"Health: {health}"); /// removed and replaced witha UI.
        }


        // Condition to let the player know that they've won upon touching the finish line.
        if (other.tag == "Goal")
        {
            Debug.Log("You win!");
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
}
