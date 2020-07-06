using UnityEngine;
using UnityEngine.AI;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.RTS
{
    public class StandardInfantry : Unit
    {
        private NavMeshAgent navMesh;

        void Start()
        {
            navMesh = GetComponent<NavMeshAgent>();
            navMesh.speed = moveSpeed;
        }
    }
}