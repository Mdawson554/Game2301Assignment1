using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
   
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float ZAxis = 0f;
        float XAxis = 0f;
        
        if (Input.GetKey(KeyCode.W))
        {
            ZAxis = 1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            XAxis = -1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            ZAxis = -1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            XAxis = 1;
        }
        
        rb.linearVelocity = new Vector3(XAxis, rb.linearVelocity.y, ZAxis * speed);
    }
    
}
