using UnityEngine;

/*
*  Copyright (c) Jonathan Carter
*  E: jonathan@carter.games
*  W: https://jonathan.carter.games/
*/

namespace CarterGames.RTS
{
    public class Unit : MonoBehaviour, IHealth<float>, IDamageable<float>
    {
        public enum UnitType { Infanty, Tank, Plane, Boat };
        public UnitType unitType;

        public float health { get; set; }
        public float moveSpeed;


        public void Damage(float damageTaken)
        {
            if (health - damageTaken > 0)
            {
                health -= damageTaken;
            }
            else
            {
                // Kill Unit
            }
        }
    }
}