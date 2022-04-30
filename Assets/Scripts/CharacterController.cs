using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace holiday6rpg
{
    [RequireComponent(typeof(CharacterMotor))]
    public class CharacterController : MonoBehaviour
    {
        private Camera cam;
        private CharacterMotor motor;
        // Start is called before the first frame update
        void Start()
        {
            cam = Camera.main;
            motor = GetComponent<CharacterMotor>();
        }

        // Update is called once per frame
        void Update()
        {
            // Move character to mouse hit point.
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log("Mouse hit " + hit.collider.name + " " + hit.point);

                    motor.MoveToPoint(hit.point);
                }
            }
        }
    }
}

