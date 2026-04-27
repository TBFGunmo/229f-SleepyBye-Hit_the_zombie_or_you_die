using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : MonoBehaviour
{
    public Transform lane1;
    public Transform lane2;
    public Transform lane3;

    private int currentLane = 2;

    private InputAction moveLeft;
    private InputAction moveRight;
    private InputAction fireKey;
    private InputAction item1;
    private InputAction item2;
    private InputAction item3;

    public GameObject[] items;
    public Transform[] thrownPos;
    private int currentItem = 0;

    public float pushAcc = 10f;

    public float thrownAcc = 10f;
    public float heightThrown = 2f;

    public float kickForce = 10f;
    public float spinForce = 10f;
    public float curveForce = 1.0f;




    public Transform[] zombieLanes;

    private Rigidbody currentBall;




    private void Awake()
    {
        moveLeft = InputSystem.actions.FindAction("moveLeft");
        moveRight = InputSystem.actions.FindAction("moveRight");

        fireKey = InputSystem.actions.FindAction("Fire");

        item1 = InputSystem.actions.FindAction("Item1");
        item2 = InputSystem.actions.FindAction("Item2");
        item3 = InputSystem.actions.FindAction("Item3");
    }

    void Start()
    {
        transform.position = lane2.position;

        /*
        print(moveLeft);
        print(moveRight);

        print(fireKey);

        print(item1);
        print(item2);
        print(item3);
        */



    }


    void Update()
    {
        if (GameManager.instance.IsGameOver()) 
        {
            return;
        }

        if (moveLeft.triggered && currentLane != 1)
        {
            currentLane = Mathf.Clamp(currentLane - 1, 1, 3);
            ChangeLane(currentLane);
        }
        else if (moveRight.triggered && currentLane != 3)
        {
            currentLane = Mathf.Clamp(currentLane + 1, 1, 3);
            ChangeLane(currentLane);
        }


        if (item1.triggered)
        {
            currentItem = 0;
        }
        else if (item2.triggered)
        {
            currentItem = 1;
        }
        else if (item3.triggered)
        {
            currentItem = 2;
        }

        if (fireKey.triggered)
        {
            GameObject currentItemSelect = items[currentItem];
            Transform currentItemSelectPos = thrownPos[currentItem];

            GameObject currentOBJ = Instantiate(currentItemSelect, currentItemSelectPos.position, Quaternion.identity);
            Rigidbody rigidbody = currentOBJ.GetComponent<Rigidbody>();

            if (currentItem == 0)
            {

                float pushForce = rigidbody.mass * pushAcc;

                rigidbody.AddForce(pushForce * Vector3.forward, ForceMode.Impulse);
                //print("yes1");
            }
            else if (currentItem == 1)
            {
                Vector3 direction = (zombieLanes[currentLane - 1].position - transform.position);

                direction.y = heightThrown;

                float thrownForce = rigidbody.mass * thrownAcc;


                rigidbody.AddForce(direction * thrownForce, ForceMode.Impulse);

                //print("yes2");
            }
            else if (currentItem == 2)
            {
                //Vector3 direction = (zombieLanes[currentLane - 1].position - transform.position).normalized;


                rigidbody.AddForce(new Vector3(-0.3f,0,1) * kickForce, ForceMode.Impulse);
                rigidbody.AddTorque(Vector3.up * spinForce);
                
                currentBall = rigidbody;
                
                /*Vector3 velocity = rigidbody.linearVelocity;
                Vector3 spin = rigidbody.angularVelocity;

                Vector3 magnusForce = curveForce * Vector3.Cross(spin, velocity);

                rigidbody.AddForce(magnusForce);*/

                //print("yes3");

            }

            Destroy(currentOBJ, 2);

        }
    }

    private void FixedUpdate()
    {

        if (GameManager.instance.IsGameOver())
        {
            return;
        }

        if (currentBall != null) 
        {
            Vector3 velocity = currentBall.linearVelocity;
            Vector3 spin = currentBall.angularVelocity;

            Vector3 magnusForce = curveForce * Vector3.Cross(spin, velocity);

            currentBall.AddForce(magnusForce);
        }
    }






    private void ChangeLane(int lane) 
    {
        switch (lane) 
        {
            case 1:
                transform.position = lane1.position;
                break;
            case 2:
                transform.position = lane2.position;
                break;
            case 3:
                transform.position = lane3.position;
                break;
            default:
                Debug.Log("Error out of lane 1-3");
                break;
        }
    } 

}
