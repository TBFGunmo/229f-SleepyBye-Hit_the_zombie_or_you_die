using UnityEngine;

public class Zombie : MonoBehaviour
{
    public int currentLane;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        //print("111");
        if (collision.gameObject.CompareTag("Item"))
        {
            Destroy(collision.gameObject);
            GameManager.instance.AddScore(1);
            GameManager.instance.OnZombieDeath(currentLane);

            Destroy(gameObject);

            //print("work");
        }
    }
}
