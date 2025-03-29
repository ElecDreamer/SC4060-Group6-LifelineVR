using UnityEngine;

public class PlayerRBCModeZoneTracker : MonoBehaviour
{
    public string currentZone = "";
    public GameObject currentZoneObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zone"))
        {
            currentZone = other.name; // other.GetComponent<ZoneInfo>().zoneName
            currentZoneObject = other.gameObject;
            Debug.Log("Entered Zone: " + currentZone);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Zone") && other.gameObject == currentZoneObject)
        {
            currentZone = "";
            currentZoneObject = null;
            Debug.Log("Left zone.");
        }
    }
}