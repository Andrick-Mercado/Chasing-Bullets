using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MobSpawner : MonoBehaviour
{
    public GameObject enemy;
    float randx;
    Vector2 whereToSpawn;
    public float spawnNum;
    int nextSpawn = 0;
    public int[] randomLoc;
    private Alien[] _enemies;
    int isCalled = 0;

    public GameObject textDisplayName;

    [SerializeField] Canvas _canvasEnd;
    [SerializeField] Canvas _canvasEndLost;
    // Start is called before the first frame update
    private void Awake()
    {
        _canvasEnd.gameObject.SetActive(false);
        _canvasEndLost.gameObject.SetActive(false);

        if (Game_Manager.GetInstance() != null)
            textDisplayName.GetComponent<TextMeshProUGUI>().text = Game_Manager.GetInstance().getplayerName();
        else
            textDisplayName.GetComponent<TextMeshProUGUI>().text = "No Name Sir";
    }
    void Start()
    {
        randomLoc = new int[6];
        for (int i = 0; i < 6; i++)
        {
            randomLoc[i] = (i + 1) * 5;
        }

        if(Game_Manager.GetInstance() == null)
        {
            spawnNum = 6;

            return;
        }
        string dificulty = Game_Manager.GetInstance().getplayerDifficulty();

        if (dificulty.Contains("Normal"))
            spawnNum = 4;
        else if (dificulty.Contains("Hard"))
            spawnNum = 6;
        else
            spawnNum = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnNum > nextSpawn)
        {
            //randx = Random.Range(0f,30f);   randx
            whereToSpawn = new Vector2(randomLoc[nextSpawn], -8f);//transform.position.y);
            Instantiate(enemy, whereToSpawn, Quaternion.identity);
            nextSpawn++;

        }
        else if(isCalled ==0)
        {
            _enemies = FindObjectsOfType<Alien>();
            foreach (Alien alien in _enemies)
            {
                if (alien != null)
                {
                    return;
                }
            }
            Debug.Log("all aliens are actually dead!");
            
            GameObject.FindObjectOfType<Bullet>().blockDragMovement();  //.blockDragMovement()

            _canvasEnd.gameObject.SetActive(true);
            isCalled++;
            FindObjectOfType<AudioManager>().Play("WinMusic");
        }
    }

    public void LostScene()
    {
        Alien[] cur_enemies = FindObjectsOfType<Alien>();
        foreach (Alien alien in cur_enemies)
        {
            if (alien != null)
            {
                alien.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                alien.GetComponent<Animator>().enabled = false;
            }
        }
        _canvasEndLost.gameObject.SetActive(true);

    }

    public void retryLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        FindObjectOfType<AudioManager>().PauseSound("LostMusic");
        FindObjectOfType<AudioManager>().Play("Theme");
    }

    public void quitLevel()
    {
        Debug.Log("Game is exiting");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        // set the playmode to stop
#else
         Application.Quit();
#endif
    }

    public void nextLevel()
    {
        SceneManager.LoadScene("MainMenu");
    }


}
