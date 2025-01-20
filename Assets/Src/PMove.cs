using Unity.VisualScripting;
using UnityEngine;


public class PMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    public JoystickController joystick; 

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (rb != null)
        {
            if (joystick!=null)
            {

           
            Vector2 movement = new Vector2(joystick.Horizontal, joystick.Vertical);
            rb.velocity = movement * moveSpeed;


            if (movement != Vector2.zero)
            {
                float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
                rb.rotation = angle;
            }
            }
        }
    }
}