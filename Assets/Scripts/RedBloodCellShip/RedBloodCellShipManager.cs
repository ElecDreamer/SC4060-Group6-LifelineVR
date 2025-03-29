using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RedBloodCellShipManager : MonoBehaviour
{
    public GameObject redBloodCellShip;

    // Start is called before the first frame update
    void Start()
    {
        // Store the ship's starting rotation
        initialRotation = redBloodCellShip.transform.rotation;

        // Initialise starting forward speed
        currentForwardSpeedType = FORWARD_SPEED_TYPE.STOP;
        forwardSpeedText_Stop.color = forwardTextActiveColor;

        // Initialise starting o2 storage
        currentO2InStorage = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Forward
        currentSpeed = GetForwardSpeedFromType(currentForwardSpeedType);
        redBloodCellShip.transform.Translate(currentSpeed * Time.deltaTime * -Vector3.forward);

        float yaw = (xrSteeringWheelYaw.value - 0.5f) * 2f * MAX_YAW_DEGREE;    // -MAX_YAW to +MAX_YAW
        float pitch = (xrSteeringWheelPitch.value - 0.5f) * 2f * MAX_PITCH_DEGREE;  // -MAX_PITCH to +MAX_PITCH

        // Apply yaw and pitch as offset from initial rotation
        Quaternion yawRotation = Quaternion.AngleAxis(yaw, Vector3.up);
        Quaternion pitchRotation = Quaternion.AngleAxis(pitch, Vector3.right);

        redBloodCellShip.transform.rotation = initialRotation * yawRotation * pitchRotation;

        // O2 Storage Level
        GetCurrentFillO2StorageLevel();
    }


    /**
     * Steering (YAW + PITCH)
     */
    public XRSteeringWheel xrSteeringWheelYaw;
    public XRSteeringWheel xrSteeringWheelPitch;
    public float MAX_YAW_DEGREE = 45f; // max 45 degrees to either direction
    public float MAX_PITCH_DEGREE = 45f; // max 45 degrees to either direction

    private Quaternion initialRotation;

    /**
     * Forward motion
     */
    public FORWARD_SPEED_TYPE currentForwardSpeedType;
    public float MAX_FORWARD_SPEED = 10f;
    private float currentSpeed;

    public enum FORWARD_SPEED_TYPE
    {
        STOP,
        SLOW,
        MEDIUM,
        FAST
    }
    public TMP_Text forwardSpeedText_Stop;
    public TMP_Text forwardSpeedText_Slow;
    public TMP_Text forwardSpeedText_Medium;
    public TMP_Text forwardSpeedText_Fast;


    Color forwardTextActiveColor = new Color(0.9176471f, 0.1098039f, 0.5372549f, 1);
    Color forwardTextDefaultColor = new Color(0.9333333f, 0.9333333f, 0.9333333f, 1);
    private float GetForwardSpeedFromType(FORWARD_SPEED_TYPE type)
    {
        return type switch
        {
            FORWARD_SPEED_TYPE.STOP => 0f,
            FORWARD_SPEED_TYPE.SLOW => 1f,
            FORWARD_SPEED_TYPE.MEDIUM => 2f,
            FORWARD_SPEED_TYPE.FAST => 3f,
            _ => 0f,
        };
    }
    private void SetForwardSpeed(FORWARD_SPEED_TYPE type)
    {
        Debug.Log("RBC Ship Forward Speed Set: " + type.ToString());
        // if already in the that speed
        if (currentForwardSpeedType == type) return;
        currentForwardSpeedType = type;

        // Update Speed Controls UI Panel

        switch (type)
        {
            case FORWARD_SPEED_TYPE.STOP:
                forwardSpeedText_Stop.color = forwardTextActiveColor;

                forwardSpeedText_Slow.color = forwardTextDefaultColor;
                forwardSpeedText_Medium.color = forwardTextDefaultColor;
                forwardSpeedText_Fast.color = forwardTextDefaultColor;
                break;
            case FORWARD_SPEED_TYPE.SLOW:
                forwardSpeedText_Slow.color = forwardTextActiveColor;

                forwardSpeedText_Stop.color = forwardTextDefaultColor;
                forwardSpeedText_Medium.color = forwardTextDefaultColor;
                forwardSpeedText_Fast.color = forwardTextDefaultColor;
                break;
            case FORWARD_SPEED_TYPE.MEDIUM:
                forwardSpeedText_Medium.color = forwardTextActiveColor;

                forwardSpeedText_Stop.color = forwardTextDefaultColor;
                forwardSpeedText_Slow.color = forwardTextDefaultColor;
                forwardSpeedText_Fast.color = forwardTextDefaultColor;
                break;
            case FORWARD_SPEED_TYPE.FAST:
                forwardSpeedText_Fast.color = forwardTextActiveColor;

                forwardSpeedText_Stop.color = forwardTextDefaultColor;
                forwardSpeedText_Slow.color = forwardTextDefaultColor;
                forwardSpeedText_Medium.color = forwardTextDefaultColor;
                break;
        }
    }
    public void ToggleForwardSpeedStop()
    {
        SetForwardSpeed(FORWARD_SPEED_TYPE.STOP);
    }
    public void ToggleForwardSpeedSlow()
    {
        SetForwardSpeed(FORWARD_SPEED_TYPE.SLOW);
    }
    public void ToggleForwardSpeedMedium()
    {
        SetForwardSpeed(FORWARD_SPEED_TYPE.MEDIUM);
    }
    public void ToggleForwardSpeedFast()
    {
        SetForwardSpeed(FORWARD_SPEED_TYPE.FAST);
    }

    /**
     * Oxygen Storage
     */
    public Image maskO2StorageLevel;
    public int currentO2InStorage;
    private readonly int MAX_O2_STORAGE_LEVEL = 100;
    
    void GetCurrentFillO2StorageLevel()
    {
        float fillAmount = currentO2InStorage / MAX_O2_STORAGE_LEVEL;
        maskO2StorageLevel.fillAmount = fillAmount;
    }
    
    public void AddOxygenToStorage()
    {
        // TODO
        currentO2InStorage += 10;
    }
    public void RemoveOxygenFromStorage()
    {
        // TODO
        currentO2InStorage -= 10;
    }
}
