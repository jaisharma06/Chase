using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 2f; //Movement speed of the enemy
    [SerializeField]
    private float detectionRadius = 1f; //The radius in which enemy can detect player

    private PlayerController player; //Playercontroller reference
    private Rigidbody rb; //rigidbody component attached to the enemy

    private bool isGameOver = false; //True if game is over
    [SerializeField]
    private bool isActive = false; //True if enemy is following player

    private Vector3 direction; //Direction of the movement of the enemy

    private void OnEnable()
    {
        EventManager.OnGameOver += OnGameOver; //Register gameover event
    }

    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        rb = GetComponent<Rigidbody>();
        InvokeRepeating("SetDirection", 0, 2f); //Change direction of the enemy after every 1 second
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
            return; // returns if game is already over
        if (!isActive)
            MoveRandom(); //if not following player then move in random direction
        else
            FollowPlayer(); //else follow player
    }


    private void OnDisable()
    {
        EventManager.OnGameOver -= OnGameOver; //Deregister gameover event
    }

    /// <summary>
    /// This method is called to follow player
    /// </summary>
    private void FollowPlayer()
    {
        if (!player)
            return; //if no player controller is found then return

        var dir = (player.transform.position - transform.position).normalized; //get the direction of the player

        transform.Translate(dir * Time.deltaTime * moveSpeed); //Move towards the player
    }

    /// <summary>
    /// This method is called to move the enemy in a random direction
    /// </summary>
    private void MoveRandom()
    {
        transform.Translate(direction * Time.deltaTime * moveSpeed); //Move the enemy in the random direction
        

        var cols = Physics.OverlapSphere(transform.position, detectionRadius); //Draw a sphere around

        //if player is found then set the enemy active
        for (int i = 0; i < cols.Length; i++) 
        {
            var player = cols[i].GetComponent<PlayerController>();
            if (player)
                isActive = true;
        }

    }

    /// <summary>
    /// This method is called on gameover
    /// </summary>
    private void OnGameOver()
    {
        isGameOver = true; //Set gameover to true
        if (rb)
            rb.velocity = Vector3.zero; //Set the velocity of enemy to zero
    }


    private void OnDrawGizmos()
    {
        Color color = Color.green;
        color.a = 0.3f;
        Gizmos.color = color;

        Gizmos.DrawSphere(transform.position, detectionRadius);
    }

    /// <summary>
    /// Changes the direction of movement
    /// </summary>
    private void SetDirection()
    {
        int value = Random.Range(0, 4);

        switch (value)
        {
            case 0: direction = Vector3.forward;
                break;
            case 1: direction = -Vector3.forward;
                break;
            case 2:
                direction = Vector3.right;
                break;
            case 3:
                direction = -Vector3.right;
                break;
        }
    }
}
