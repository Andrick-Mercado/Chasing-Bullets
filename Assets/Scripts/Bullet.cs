using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class Bullet : MonoBehaviour
{
    Vector3 _initialPosition;
    [SerializeField] private float _BulletPower = 10000;
    private bool _BulletLaunched;
    private float _TimeSittingAround;

    private int _bulletCount;

    public GameObject textDisplayBullet;

    [SerializeField] Transform spawnPoint;


    private void Awake()
    {
        _initialPosition = transform.position;
        _bulletCount = 0;
    }


    private void Update()
    {
        GetComponent<LineRenderer>().SetPosition(1, _initialPosition);
        GetComponent<LineRenderer>().SetPosition(0, transform.position);

        if (_BulletLaunched && GetComponent<Rigidbody2D>().velocity.magnitude <= 0.1)
        {
            _TimeSittingAround += Time.deltaTime;
        }

        if(transform.position.y > 50 || transform.position.x > 100 ||
            transform.position.y < -50 || transform.position.x < -100 ||
            _TimeSittingAround > .3)
        {//death of the bullet
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            transform.position = spawnPoint.position;
            GetComponent<Rigidbody2D>().gravityScale = 0;
            _TimeSittingAround = 0;
            _BulletLaunched = false;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f,0f);
            //GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }

    private void OnMouseDown()
    {
        if (Game_Manager.GetInstance() != null)
        {
            if (Game_Manager.GetInstance().getplayerColor() == "Green")
                GetComponent<SpriteRenderer>().color = Color.green;
            else if (Game_Manager.GetInstance().getplayerColor() == "Blue")
                GetComponent<SpriteRenderer>().color = Color.blue;
            else if (Game_Manager.GetInstance().getplayerColor() == "Yellow")
                GetComponent<SpriteRenderer>().color = Color.yellow;
            else if (Game_Manager.GetInstance().getplayerColor() == "Orange")
                GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.64f, 0.0f);
            else if (Game_Manager.GetInstance().getplayerColor() == "Purple")
                GetComponent<SpriteRenderer>().color = new Color(233f / 255f, 79f / 255f, 55f / 255f);
            else if (Game_Manager.GetInstance().getplayerColor() == "Pink")
                GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.64f, .89f);
            else
                GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
        

        GetComponent<LineRenderer>().enabled = true;
        GetComponent<BoxCollider2D>().enabled = false;
    }

    private void OnMouseUp()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        Vector2 directionToInitialPosition = _initialPosition - transform.position;
        GetComponent<Rigidbody2D>().AddForce(directionToInitialPosition * _BulletPower);
        GetComponent<Rigidbody2D>().gravityScale = 1;
        _BulletLaunched = true;
        GetComponent<LineRenderer>().enabled = false;

        GetComponent<BoxCollider2D>().enabled = true;

        FindObjectOfType<AudioManager>().Play("ShotSound");

        _bulletCount++;
        textDisplayBullet.GetComponent<TextMeshProUGUI>().text = "Bullet Count: " + _bulletCount;
    }

    private void OnMouseDrag()
    {
        //GetComponent<Rigidbody2D>().gravityScale = 1;
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(newPosition.x, newPosition.y);
    }

    public void blockDragMovement()
    {
        GetComponent<LineRenderer>().enabled = false;

        FindObjectOfType<Bullet>().gameObject.SetActive(false);
        //GetComponent<BoxCollider2D>().enabled = false;

    }

    [SerializeField] private GameObject _DeadAnim;

    [SerializeField] private Canvas _canvas_t;

    public GameObject cowboy;
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.GetComponent<Cowboy_Iddle>() != null)
        {
            Instantiate(_DeadAnim, cowboy.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);

            Instantiate(_DeadAnim, transform.position, Quaternion.identity);

            FindObjectOfType<MobSpawner>().LostScene();

            FindObjectOfType<AudioManager>().PauseSound("Theme");

            FindObjectOfType<Bullet>().blockDragMovement();

            FindObjectOfType<AudioManager>().Play("LostMusic");

            _canvas_t.gameObject.SetActive(false);
            return;
        }
    }




}
