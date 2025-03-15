using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NotificationsController : MonoBehaviour
{
    public GameObject DefaultNotificationPanel;
    public GameObject ErrorNotificationPanel;
    public GameObject InfoNotificationPanel;
    public GameObject SuccessNotificationPanel;
    public GameObject WarningNotificationPanel;

    public TMP_Text DefaultNotificationText;
    public TMP_Text ErrorNotificationText;
    public TMP_Text InfoNotificationText;
    public TMP_Text SuccessNotificationText;
    public TMP_Text WarningNotificationText;

    private void Start()
    {
        ClearNotificationMessages();
    }

    /// <summary>
    /// Note: Each notification type can only be displayed once at a time, with a default 5s auto-disappear feature.
    /// </summary>
    /// <param name="notificationType"></param>
    /// <param name="message"></param>
    /// <param name="duration"></param>
    public void DisplayNotification(Enums.NotificationType notificationType, string message, float duration = 5f)
    {
        switch (notificationType)
        {
            case Enums.NotificationType.Default:
                if (DefaultNotificationPanel.activeSelf == true)
                {
                    Debug.LogWarning("Default Notification already sent. Please wait till the previous notification to disappear...");
                    return;
                }
                DefaultNotificationText.SetText(message);
                DefaultNotificationPanel.SetActive(true);
                break;
            case Enums.NotificationType.Error:
                if (ErrorNotificationPanel.activeSelf == true)
                {
                    Debug.LogWarning("Error Notification already sent. Please wait till the previous notification to disappear...");
                    return;
                }
                ErrorNotificationText.SetText(message);
                ErrorNotificationPanel.SetActive(true);
                break;
            case Enums.NotificationType.Info:
                if (InfoNotificationPanel.activeSelf == true)
                {
                    Debug.LogWarning("Info Notification already sent. Please wait till the previous notification to disappear...");
                    return;
                }
                InfoNotificationText.SetText(message);
                InfoNotificationPanel.SetActive(true);
                break;
            case Enums.NotificationType.Success:
                if (SuccessNotificationPanel.activeSelf == true)
                {
                    Debug.LogWarning("Success Notification already sent. Please wait till the previous notification to disappear...");
                    return;
                }
                SuccessNotificationText.SetText(message);
                SuccessNotificationPanel.SetActive(true);
                break;
            case Enums.NotificationType.Warning:
                if (WarningNotificationPanel.activeSelf == true)
                {
                    Debug.LogWarning("Warning Notification already sent. Please wait till the previous notification to disappear...");
                    return;
                }
                WarningNotificationText.SetText(message);
                DefaultNotificationPanel.SetActive(true);
                break;
            default:
                if (DefaultNotificationPanel.activeSelf == true)
                {
                    Debug.LogWarning("Default Notification already sent. Please wait till the previous notification to disappear...");
                    return;
                }
                DefaultNotificationText.SetText(message);
                WarningNotificationPanel.SetActive(true);
                break;
        }

        StartCoroutine(AutoCloseNotification(notificationType, duration)); // Auto close notification modal
    }

    private IEnumerator AutoCloseNotification(Enums.NotificationType notificationType, float delay)
    {
        yield return new WaitForSecondsRealtime(delay);

        switch (notificationType)
        {
            case Enums.NotificationType.Default:
                DefaultNotificationText.SetText("");
                DefaultNotificationPanel.SetActive(false);
                break;
            case Enums.NotificationType.Error:
                ErrorNotificationText.SetText("");
                ErrorNotificationPanel.SetActive(false);
                break;
            case Enums.NotificationType.Info:
                InfoNotificationText.SetText("");
                InfoNotificationPanel.SetActive(false);
                break;
            case Enums.NotificationType.Success:
                SuccessNotificationText.SetText("");
                SuccessNotificationPanel.SetActive(false);
                break;
            case Enums.NotificationType.Warning:
                WarningNotificationText.SetText("");
                DefaultNotificationPanel.SetActive(false);
                break;
            default:
                DefaultNotificationText.SetText("");
                WarningNotificationPanel.SetActive(false);
                break;
        }
    }

    private void ClearNotificationMessages()
    {
        DefaultNotificationText.SetText("");
        ErrorNotificationText.SetText("");
        InfoNotificationText.SetText("");
        SuccessNotificationText.SetText("");
        WarningNotificationText.SetText("");
    }

    /**
     * For testing
     */
    public void TestDisplayDefaultNotification()
    {
        DisplayNotification(
            Enums.NotificationType.Default, 
            "[Default] Hi Player! We hope you are enjoying the game so far!",
            5f);
    }

    public void TestDisplayErrorNotification()
    {
        DisplayNotification(
            Enums.NotificationType.Error, 
            "[Error] Hi Player! We hope you are enjoying the game so far!",
            5f);
    }
    public void TestDisplayInfoNotification()
    {
        DisplayNotification(
            Enums.NotificationType.Info, 
            "[Info] Hi Player! We hope you are enjoying the game so far!",
            5f);
    }
    public void TestDisplaySuccessNotification()
    {
        DisplayNotification(
            Enums.NotificationType.Success, 
            "[Success] Hi Player! We hope you are enjoying the game so far!",
            5f);
    }
    public void TestDisplayWarningNotification()
    {
        DisplayNotification(
            Enums.NotificationType.Warning, 
            "[Warning] Hi Player! We hope you are enjoying the game so far!",
            5f);
    }
}
