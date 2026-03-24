using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int score = 0;
    //public int targetPoint = 5;
    public float timeLeft = 30f;
    public float timeAdd = 2f;


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

    private float randomTimeSpawn;

    private bool getTime1 = false;
    private bool getTime2 = false;
    private bool getTime3 = false;

    private float lane1Time;
    private float lane2Time;
    private float lane3Time;

    private bool gameOver = false;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //targetPoint = lane1Amout + lane2Amout + lane3Amout;
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
            gameOver = true;
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

                    //lane3current++;
                }
            }
        //}


        //checkWin();



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
                break;
            case 2:
                lane2Spawned = false;
                break;
            case 3:
                lane3Spawned = false;
                break;
        }

        timeLeft += timeAdd;

    }

    /*private void checkWin() 
    {
        if (score >= targetPoint) 
        {
            Debug.Log("win");
        }
    }*/


}
