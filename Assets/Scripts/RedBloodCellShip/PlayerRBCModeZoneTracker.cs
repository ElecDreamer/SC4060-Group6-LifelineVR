using System.Collections.Generic;
using UnityEngine;

public class PlayerRBCModeZoneTracker : MonoBehaviour
{
    private readonly HashSet<GameObject> zones = new();

    public string currentZone = "";
    public GameObject currentZoneObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zone"))
        {
            zones.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Zone"))
        {
            zones.Remove(other.gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (zones.Count > 0)
        {
            foreach (GameObject zone in zones)
            {
                currentZone = zone.name;
                currentZoneObject = zone;
                break;
            }
            Debug.Log("Current zone: " + currentZone);
        }
        else
        {
            currentZone = "";
            currentZoneObject = null;
            Debug.Log("No zones detected.");
        }
    }
}