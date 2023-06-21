using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 5f;
    [SerializeField] float arrowText;
    // float rotateSpeed = 10f;
    Rigidbody2D rb;
    PlayerMovement player;
    float xSpeed;
    [SerializeField] AudioClip ghostDeathSFX;
    [SerializeField] AudioClip arrowDeathSFX;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.Rotate(0f, 0f, -88f);
        player = FindObjectOfType<PlayerMovement>();
        xSpeed = player.transform.localScale.x * bulletSpeed; //localScale is left or right 
    }

    // MOVE BULLETS X POSITION through Vector2
    void Update()
    {
        rb.velocity = new Vector2(xSpeed, 0f);
        // transform.Rotate(0, 0, rotateSpeed);
    }

    // DESTROY BULLET && ENEMY on TAG COLLISIONs
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            // Destroy Enemy
            Destroy(other.gameObject);
            Debug.Log("ARROW HIT ENEMY");
            AudioSource.PlayClipAtPoint(ghostDeathSFX, Camera.main.transform.position);
        }

        Destroy(gameObject);
        //AudioSource.PlayClipAtPoint(arrowDeathSFX, Camera.main.transform.position);
        // Destroys Bullet
        // Destroy(gameObject);
    } 

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("ARROW HIT SO MUSIC PLAYS!");
        AudioSource.PlayClipAtPoint(arrowDeathSFX, Camera.main.transform.position);
        Destroy(gameObject);
    }
}
