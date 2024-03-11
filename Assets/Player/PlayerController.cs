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

    private void Update()
    {
        if ( Time.timeScale > 0) // health.IsAlive &&
        {
            var timeScaledSpeed = speed * Time.deltaTime;
            mover.Move(new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized *
                       timeScaledSpeed);

            // var mousePosition = Input.mousePosition;
            // if (mousePosition.x < 0 || mousePosition.x > Screen.width || mousePosition.y < 0 ||
            //     mousePosition.y > Screen.height)
            // {
            //     return;
            // }

            // var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // var plane = new Plane(Vector3.up, new Vector3(0, 1, 0));
            // if (plane.Raycast(ray, out var distance))
            // {
            //     var point = ray.GetPoint(distance);
            //     mover.LookAt(point);
            // }


            transform.Rotate(new Vector3(Input.GetAxis("Mouse Y")*3f, Input.GetAxis("Mouse X")*3f, 0));
        }
    }

    private void DestroyOnDeath()
    {
        Destroy(transform.GetChild(0).gameObject);
    }
}
