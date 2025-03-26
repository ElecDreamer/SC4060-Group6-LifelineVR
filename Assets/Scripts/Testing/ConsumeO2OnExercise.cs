using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ConsumeO2OnExercise : MonoBehaviour
{
    // Game Objects
    public GameObject LeftHand;
    public GameObject RightHand;

    public SwingingArmMotion SAMscript;

    [SerializeField] private XRGrabInteractable grabbable;

    //Vector3 Positions
    [SerializeField] private Vector3 PositionPreviousFrameLeftHand;
    [SerializeField] private Vector3 PositionPreviousFrameRightHand;
    [SerializeField] private Vector3 PositionCurrentFrameLeftHand;
    [SerializeField] private Vector3 PositionCurrentFrameRightHand;

    [SerializeField] private int OxygenTestCount = 500;
    [SerializeField] private float HandSpeed;

    // Start is called before the first frame update
    void Start()
    {
        PositionPreviousFrameLeftHand = LeftHand.transform.position;
        PositionPreviousFrameRightHand = RightHand.transform.position;
        grabbable = GetComponent<XRGrabInteractable>();
        grabbable.selectEntered.AddListener(ConsumeO2);
    }

    // Update is called once per frame
    void Update()
    {
        while(grabbable.isSelected)
        {
            SAMscript.enabled = false;

            PositionCurrentFrameLeftHand = LeftHand.transform.position;
            PositionCurrentFrameRightHand = RightHand.transform.position;



            PositionPreviousFrameLeftHand = PositionCurrentFrameLeftHand;
            PositionPreviousFrameRightHand = PositionCurrentFrameRightHand;
        }

        if (!SAMscript.isActiveAndEnabled)
        {
            SAMscript.enabled = true;
        }    
    }

    public void ConsumeO2(SelectEnterEventArgs arg)
    {
        Debug.Log("Consuming O2");

        PositionCurrentFrameLeftHand = LeftHand.transform.position;
        PositionCurrentFrameRightHand = RightHand.transform.position;

        var leftHandDistanceMoved = Vector3.Distance(PositionPreviousFrameLeftHand, PositionCurrentFrameLeftHand);
        var rightHandDistanceMoved = Vector3.Distance(PositionPreviousFrameRightHand, PositionCurrentFrameRightHand);
    }
}
