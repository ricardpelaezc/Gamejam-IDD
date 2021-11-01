using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IAState>())
        {

            IAState.GetIA().IndexWaypoint++;
            IAState.GetIA().timerToNextWayPoint = 0;
        }
    }
}
