using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeveloperConsoleBehaviour : MonoBehaviour
{
    [SerializeField] private string _prefix = string.Empty;
    [SerializeField] private ConsoleCommand[] _commands = new ConsoleCommand[0];

    [Header("UI")]
    [SerializeField] private GameObject uiCanvas = null;
    [SerializeField] private RectTransform logContent = null;
    [SerializeField] private TextMeshProUGUI logEntryTemplate = null;
    [SerializeField] private TMP_InputField inputField = null;

    private static DeveloperConsoleBehaviour _instance;

    private DeveloperConsole _devConsole;
    
    public DeveloperConsole DeveloperConsole
    {
        get
        {
            if(_devConsole != null) { return _devConsole; }

            return _devConsole = new DeveloperConsole(_prefix, _commands);
        }
    }

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }

    private void LateUpdate()
    {
        if(Input.GetKeyDown(KeyCode.BackQuote))
        {
            Toggle();
        }
    }

    public void Toggle()
    {
        if(uiCanvas.activeSelf)
        {
            uiCanvas.SetActive(false);
        }
        else
        {
            uiCanvas.SetActive(true);
            inputField.ActivateInputField();
        }
    }

    public void ProcessCommand(string inputValue)
    {
        DeveloperConsole.ProcessCommand(inputValue);

        inputField.text = string.Empty;

        AddLog(inputValue);
    }

    private void AddLog(string text)
    {
        TextMeshProUGUI log = Instantiate(logEntryTemplate, logContent);
        log.text = text;

        LayoutRebuilder.ForceRebuildLayoutImmediate(log.transform as RectTransform);
        LayoutRebuilder.ForceRebuildLayoutImmediate(logContent);
    }
}
