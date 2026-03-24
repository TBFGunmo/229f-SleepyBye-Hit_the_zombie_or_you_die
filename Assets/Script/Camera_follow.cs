using UnityEngine;
using UnityEngine.UIElements;

public class Camera_follow : MonoBehaviour
{
    public Transform cameraPos;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        transform.position = cameraPos.position ;
    }

}
