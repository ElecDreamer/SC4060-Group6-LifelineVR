using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HumanModeSceneController : MonoBehaviour
{
    private QuestHandControllersMenuController questHandControllersMenuController;

    // Start is called before the first frame update
    void Start()
    {
        // Find the Controllers in the scene
        questHandControllersMenuController = FindObjectOfType<QuestHandControllersMenuController>();
        if (questHandControllersMenuController == null)
            Debug.LogError("QuestHandControllersMenuController not found! Make sure it's in the scene.");

        questHandControllersMenuController.OpenQuestHandControllersMenu();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
