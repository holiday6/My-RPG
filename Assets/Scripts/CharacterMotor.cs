using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace holiday6rpg
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class CharacterMotor : MonoBehaviour
    {
        NavMeshAgent agent;
        // Start is called before the first frame update
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        public void MoveToPoint(Vector3 point)
        {
            agent.SetDestination(point);
        }

    }

}
