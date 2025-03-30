using UnityEngine;

public class ZoneController : MonoBehaviour
{
    public bool isZone = false;
    public string zoneName = "Lungs";

    public void Start()
    {
        if (isZone)
        {
            foreach (Transform children in transform)
            {
                children.gameObject.tag = "Zone";
                children.gameObject.name = zoneName + " Zone";
            }
        }
    }
}
