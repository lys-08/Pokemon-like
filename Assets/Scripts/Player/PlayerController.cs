using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    // private Health health;
    private Mover mover;

    private void Start()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;

        mover = gameObject.GetComponent<Mover>();
        // health = gameObject.GetComponent<Health>();

        // health.AddOnDeathListener(DestroyOnDeath);
    }

    public void HandleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed *= 10f;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 5f;
        }

        var timeScaledSpeed = speed * Time.deltaTime;
        var movement  =  transform.right * Input.GetAxisRaw("Horizontal") + transform.forward * Input.GetAxisRaw("Vertical");

        mover.Move(movement.normalized * timeScaledSpeed);

        // var mousePosition = Input.mousePosition;
        // if (mousePosition.x < 0 || mousePosition.x > Screen.width || mousePosition.y < 0 ||
        //     mousePosition.y > Screen.height)
        // {
        //     return;
        // }
        

        // transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X")*3f, 0)); // Input.GetAxis("Mouse Y")*3f
    }

    private void DestroyOnDeath()
    {
        Destroy(transform.GetChild(0).gameObject);
    }
}
