using UnityEngine;
using TMPro;
using System.Collections;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int score = 0;
    //public int targetPoint = 5;
    public float timeLeft = 20f;
    public float timeAdd = 1.5f;


    //public int lane1Amout = 1;
   // private int lane1current = 0;
    public bool lane1Spawned = false;

   // public int lane2Amout = 1;
    //private int lane2current = 0;
    public bool lane2Spawned = false;

    //public int lane3Amout = 1;
    //private int lane3current = 0;
    public bool lane3Spawned = false;

    public Transform zombiePos1;
    public Transform zombiePos2;
    public Transform zombiePos3;

    public GameObject zombiePrefab;

    public GameObject[] Obstacles; // 0 wall 1 pole
    public Transform ObstaclesPos1;
    public Transform ObstaclesPos2;
    public Transform ObstaclesPos3;

    private GameObject obs1;
    private GameObject obs2;
    private GameObject obs3;



    private float randomTimeSpawn;

    private bool getTime1 = false;
    private bool getTime2 = false;
    private bool getTime3 = false;

    private float lane1Time;
    private float lane2Time;
    private float lane3Time;

    private bool gameOver = false;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI addTimeText;

    public Color normalColor = Color.white;
    public Color warningColor = Color.red;
    public float dangerTime = 5f;

    private Vector3 addTimeStartPos;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //targetPoint = lane1Amout + lane2Amout + lane3Amout;
        addTimeStartPos = addTimeText.transform.position;
        addTimeText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver) 
        {
            return;
        }

        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0) 
        {
            GameOver();
        }

        //if (!(lane1current >= lane1Amout))
        //{
            if (!lane1Spawned && !getTime1)
            {
                randomTimeSpawn = Random.Range(1, 3);
                lane1Time = Time.time + randomTimeSpawn;
                getTime1 = true;

            }
            else if (!lane1Spawned && getTime1)
            {
                if (Time.time >= lane1Time)
                {
                    lane1Spawned = true;
                    getTime1 = false;
                    GameObject currentZombie = Instantiate(zombiePrefab, zombiePos1.position, zombiePrefab.transform.rotation);
                    Zombie currentZombieScript = currentZombie.GetComponent<Zombie>();
                    currentZombieScript.currentLane = 1;

                    int randomObs = Random.Range(0, 3);
                
                    if (randomObs != 2)
                    {
                        obs1 = Instantiate(Obstacles[randomObs], ObstaclesPos1.position, Obstacles[randomObs].transform.rotation);
                        obs1.name = "Obstacle_1";
                    }
                    else
                    {
                        obs1 = null;
                    }


                //lane1current++;
            }
            }
        //}

        //if (!(lane2current >= lane2Amout))
        //{
            //print("ss1");
            if (!lane2Spawned && !getTime2)
            {
                randomTimeSpawn = Random.Range(1, 3);
                lane2Time = Time.time + randomTimeSpawn;
                getTime2 = true;
            }
            else if (!lane2Spawned && getTime2)
            {
                if (Time.time >= lane2Time)
                {
                    lane2Spawned = true;
                    getTime2 = false;
                    GameObject currentZombie = Instantiate(zombiePrefab, zombiePos2.position, zombiePrefab.transform.rotation);
                    Zombie currentZombieScript = currentZombie.GetComponent<Zombie>();
                    currentZombieScript.currentLane = 2;

                    int randomObs = Random.Range(0, 3);

                if (randomObs != 2)
                {
                    obs2 = Instantiate(Obstacles[randomObs], ObstaclesPos2.position, Obstacles[randomObs].transform.rotation);
                    obs2.name = "Obstacle_2";
                }
                else 
                {
                    obs2 = null;
                }

                //lane2current++;
            }
            }
        //}

        //if (!(lane3current >= lane3Amout))
        //{
            //print("ss");
            if (!lane3Spawned && !getTime3)
            {
                randomTimeSpawn = Random.Range(1, 3);
                lane3Time = Time.time + randomTimeSpawn;
                getTime3 = true;
            }
            else if (!lane3Spawned && getTime3)
            {
                if (Time.time >= lane3Time)
                {
                    lane3Spawned = true;
                    getTime3 = false;
                    GameObject currentZombie = Instantiate(zombiePrefab, zombiePos3.position, zombiePrefab.transform.rotation);
                    Zombie currentZombieScript = currentZombie.GetComponent<Zombie>();
                    currentZombieScript.currentLane = 3;

                    int randomObs = Random.Range(0, 3);

                    if (randomObs != 2)
                    {
                        obs3 = Instantiate(Obstacles[randomObs], ObstaclesPos3.position, Obstacles[randomObs].transform.rotation);
                        obs3.name = "Obstacle_3";
                    }
                    else
                    {
                        obs3 = null;
                    }


                //lane3current++;
            }
            }
        //}


        //checkWin();
        UpdateUI();


    }

    public void AddScore(int amout) 
    {
        score += amout;
    }

    public void OnZombieDeath(int lane) 
    {
        switch (lane)
        {
            case 1:
                lane1Spawned = false ;
                if (obs1) 
                {
                    Destroy(obs1 );
                }

                break;
            case 2:
                lane2Spawned = false;
                if (obs2)
                {
                    Destroy(obs2);
                }

                break;
            case 3:
                lane3Spawned = false;
                if (obs3)
                {
                    Destroy(obs3);
                }

                break;
        }

        timeLeft += timeAdd;
        ShowAddTime(timeAdd);
    }

    /*private void checkWin() 
    {
        if (score >= targetPoint) 
        {
            Debug.Log("win");
        }
    }*/

    private void GameOver() 
    {
        gameOver = true;
        Debug.Log("GameOver");
    }

    public bool IsGameOver() { return gameOver; }

    void UpdateUI()
    {
        scoreText.text = "Score: " + score;
        timeText.text = "Time: " + timeLeft.ToString("0.0");

        if (timeLeft <= dangerTime)
        {
            float t = timeLeft / dangerTime; // 1 → 0
            timeText.color = Color.Lerp(warningColor, normalColor, t);
        }
        else
        {
            timeText.color = normalColor;
        }
    }

    void ShowAddTime(float amount)
    {
        StopAllCoroutines();
        StartCoroutine(AddTimePopup(amount));
    }

    IEnumerator AddTimePopup(float amount)
    {
        addTimeText.gameObject.SetActive(true);
        addTimeText.text = "+" + amount.ToString("0.0") + "s";

        Color c = addTimeText.color;
        c.a = 1;
        addTimeText.color = c;

        float duration = 1f;
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;

            
            addTimeText.transform.position = addTimeStartPos + Vector3.up * time * 30f;

            
            c.a = 1 - (time / duration);
            addTimeText.color = c;

            yield return null;
        }

        addTimeText.gameObject.SetActive(false);

        
        addTimeText.transform.position = addTimeStartPos;
    }


}



