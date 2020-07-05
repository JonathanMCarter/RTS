using UnityEngine;

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


        private void Start()
        {
            cam = GetComponent<Camera>();
        }


        private void Update()
        {

        }
    }
}