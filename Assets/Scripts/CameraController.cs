using UnityEngine;
using UnityEngine.Assertions.Must;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.RTS
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Camera cam;
        [SerializeField] private float speed = 5;

        private Rigidbody rB;
        private Vector2 moveBounds;


        private void Start()
        {
            cam = GetComponent<Camera>();
            rB = GetComponent<Rigidbody>();
            moveBounds = new Vector2(Screen.width / 25, Screen.height / 25);
            //Cursor.lockState = CursorLockMode.Confined;
        }


        private void FixedUpdate()
        {
            if ((Input.mousePosition.x < Screen.width && Input.mousePosition.x > 0f) && (Input.mousePosition.y < Screen.height && Input.mousePosition.y > 0f))
            {
                // Bounds - in theory should scale fine with monitor res
                if ((Input.mousePosition.x > Screen.width - moveBounds.x) && (Input.mousePosition.y > Screen.height - moveBounds.y))
                {
                    Debug.Log("Top Right");
                    Vector3 _newPos = (transform.right / 2 + transform.forward / 2).normalized;
                    _newPos.y = 0;
                    rB.MovePosition(transform.position + _newPos * Time.deltaTime * speed);
                }
                else if ((Input.mousePosition.y > Screen.height - moveBounds.y) && (Input.mousePosition.x < moveBounds.x))
                {
                    Debug.Log("Top Left");
                    Vector3 _newPos = (-transform.right / 2 + transform.forward / 2).normalized;
                    _newPos.y = 0;
                    rB.MovePosition(transform.position + _newPos * Time.deltaTime * speed);
                }
                else if ((Input.mousePosition.y < moveBounds.y) && (Input.mousePosition.x < moveBounds.x))
                {
                    Debug.Log("Bottom Left");
                    Vector3 _newPos = (-transform.right / 2 - transform.forward / 2).normalized;
                    _newPos.y = 0;
                    rB.MovePosition(transform.position + _newPos * Time.deltaTime * speed);
                }
                else if ((Input.mousePosition.x > Screen.width - moveBounds.x) && (Input.mousePosition.y < moveBounds.y))
                {
                    Debug.Log("Bottom Right");
                    Vector3 _newPos = (transform.right / 2 - transform.forward / 2).normalized;
                    _newPos.y = 0;
                    rB.MovePosition(transform.position + _newPos * Time.deltaTime * speed);
                }
                else if (Input.mousePosition.x > Screen.width - moveBounds.x)
                {
                    Debug.Log("Right");
                    rB.MovePosition(transform.position + transform.right * Time.deltaTime * speed);
                }
                else if (Input.mousePosition.y > Screen.height - moveBounds.y)
                {
                    Debug.Log("Top");
                    Vector3 _newPos = transform.forward;
                    _newPos.y = 0;
                    rB.MovePosition(transform.position + _newPos * Time.deltaTime * speed);
                }
                else if (Input.mousePosition.x < moveBounds.x)
                {
                    Debug.Log("Left");
                    rB.MovePosition(transform.position - transform.right * Time.deltaTime * speed);
                }
                else if (Input.mousePosition.y < moveBounds.y)
                {
                    Debug.Log("Bottom");
                    Vector3 _newPos = transform.forward;
                    _newPos.y = 0;
                    rB.MovePosition(transform.position - _newPos * Time.deltaTime * speed);
                }
            }
        }
    }
}