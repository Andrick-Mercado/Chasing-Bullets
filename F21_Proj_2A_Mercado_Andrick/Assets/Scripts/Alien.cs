using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Alien : MonoBehaviour
{
    [SerializeField] private GameObject _DeadAnim;
    [SerializeField] private GameObject _DeadAnimCowboy;

    public GameObject cowboy;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.GetComponent<Bullet>() != null)
        {
            Instantiate(_DeadAnim, transform.position, Quaternion.identity);
            Destroy(gameObject);
            FindObjectOfType<AudioManager>().Play("AlienDeath");
            return;
        }

        Alien alien = collision.collider.GetComponent<Alien>();
        if(alien != null)
        {
            return;
        }
        
        
         if(collision.collider.GetComponent<Cowboy_Iddle>() != null)
        {
            Instantiate(_DeadAnimCowboy, collision.gameObject.transform.position, Quaternion.identity);//cowboy.transform.position
            Destroy(  collision.gameObject  );

            GameObject.Find("Canvas_Tutorial").gameObject.SetActive(false);

            //GameObject.Find("Canvas_end_lost").gameObject.SetActive(true);

            FindObjectOfType<MobSpawner>().LostScene();//MobSpawner.LostScene();

            FindObjectOfType<AudioManager>().PauseSound("Theme");

            FindObjectOfType<Bullet>().blockDragMovement();

            FindObjectOfType<AudioManager>().Play("AlienPunch");
            FindObjectOfType<AudioManager>().Play("LostMusic");
            
            return;
        }
         
        if ( collision.contacts[0].normal.y < -0.7)
        {
            Instantiate(_DeadAnim, transform.position, Quaternion.identity);
            Destroy(gameObject);
            FindObjectOfType<AudioManager>().Play("AlienDeath");
        }
    }

    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(-5f, 0f);
    }


}
