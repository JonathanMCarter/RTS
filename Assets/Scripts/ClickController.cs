using UnityEngine;
using UnityEngine.AI;

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
        [SerializeField] private Vector3 hitPos;
        [SerializeField] private GameObject cube;
        private Camera cam;
        private LineRenderer lR;

        private void Start()
        {
            cam = Camera.main;
            lR = cube.GetComponent<LineRenderer>();
        }


        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                clickPos = cam.ScreenToWorldPoint(Input.mousePosition);

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;


                if (Physics.Raycast(ray, out hit))
                {
                    cube.GetComponent<NavMeshAgent>().SetDestination(hit.point);

                    lR.SetPosition(1, hit.point);
                }
            }

            lR.SetPosition(0, cube.transform.position);
        }
    }
}