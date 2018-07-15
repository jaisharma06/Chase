using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 2f; //Speed at which player moves
    [SerializeField]
    private GameObject gameOverText;

    private bool isGameOver = false; //True if game is over else false

    // Update is called once per frame
    void Update()
    {

        if (isGameOver) //Returns if game is already over
            return;
        var xPos = Input.GetAxis("Horizontal"); //Value of horizontal axis
        var yPos = Input.GetAxis("Vertical");   //Value of vertical axis

        transform.Translate(new Vector3(xPos, 0, yPos) * moveSpeed * Time.deltaTime); //Move player according to the axis values
    }

    /// <summary>
    /// Called whenever player collides with a gameobject
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        var other = collision.collider; //Get the collider of the other gameobject

        if (other.GetComponent<EnemyController>()) //Checks if the other object is enemy
        {
            GameOver();
        }
        else if (other.name.Contains("winLocation"))//Checks if the other object is winlocation
        {
            GameOver();
        }
    }

    /// <summary>
    /// Called whenever the game is over
    /// </summary>
    private void GameOver()
    {
        isGameOver = true; //Sets gameover to true
        if (gameOverText)
            gameOverText.SetActive(true); //Set gameover text active
        EventManager.GameOver(); //Call gameover event
    }
}
