using UnityEngine;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.RTS
{
    public class ClickController : MonoBehaviour
    {
        [SerializeField] private Vector3 clickPos;
        private Camera cam;


        private void Start()
        {
            cam = Camera.main;
        }


        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                clickPos = cam.ScreenToWorldPoint(Input.mousePosition);
            }
        }
    }
}