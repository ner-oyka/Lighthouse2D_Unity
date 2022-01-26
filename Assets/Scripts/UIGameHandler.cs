using NodeCanvas.DialogueTrees;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using EventBusSystem;

public class UIGameHandler : MonoBehaviour, InputActions.IUIActions, INoteHandler, IInventoryHandler, IWeaponHandler, IEntityHandler, IPlayerInputHandler
{
    [System.Serializable]
    public class SubtitleDelays
    {
        public float characterDelay = 0.05f;
        public float sentenceDelay = 0.5f;
        public float commaDelay = 0.1f;
        public float finalDelay = 1.2f;
    }

    [Header("Note")]
    public GameObject NoteCanvas;
    public Text NodeIdText;

    [Header("Inventory")]
    public GameObject InventoryCanvas;
    public RectTransform InventoryContent;

    [Header("Dialogue")]
    public GameObject DialogueCanvas;
    public SubtitleDelays subtitleDelays = new SubtitleDelays();
    public Text subtitlesText;
    public Button dialogueOptionButton;
    private Dictionary<Button, int> dialogueCachedButtons;
    private bool waitForInput;
    private bool isWaitingChoice;

    [Header("Time")]
    public Text TimeText;

    [Header("Weapon")]
    public Text AmmunitionAmountText;

    [Header("EntityActionsMenu")]
    public Transform EntityActionsMenuCanvas;
    public Button entityActionsMenuOptionButton;
    private Dictionary<Button, int> entityActionsMenuCachedButtons;

    [Header("EntityDescription")]
    public Transform EntityDescriptionCanvas;
    public Text EntityDescriptionText;

    [Header("DEBUG")]
    public GameObject GameEventsDebugCanvas;
    public Text GameEventsDebugText;

    private string currentEntityId;

    private InputActions m_InputActions;

    private Camera m_MainCamera;

    private void Awake()
    {
        m_InputActions = new InputActions();
        m_InputActions.UI.SetCallbacks(this);

        m_MainCamera = Camera.main;
    }

    private void Start()
    {
        NoteCanvas.SetActive(false);
        InventoryCanvas.SetActive(false);
        DialogueCanvas.SetActive(false);
        EntityActionsMenuCanvas.gameObject.SetActive(false);
        EntityDescriptionCanvas.gameObject.SetActive(false);

        dialogueOptionButton.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        m_InputActions.UI.Enable();

        EventBus.Subscribe(this);

        //Dialogue
        DialogueTree.OnDialogueStarted += OnDialogueStarted;
        DialogueTree.OnDialogueFinished += OnDialogueFinished;
        DialogueTree.OnDialoguePaused += OnDialoguePaused;
        DialogueTree.OnSubtitlesRequest += OnSubtitlesRequest;
        DialogueTree.OnMultipleChoiceRequest += OnMultipleChoiceRequest;

    }

    private void OnDisable()
    {
        m_InputActions.UI.Disable();

        EventBus.Unsubscribe(this);
        
        //Dialogue
        DialogueTree.OnDialogueStarted -= OnDialogueStarted;
        DialogueTree.OnDialogueFinished -= OnDialogueFinished;
        DialogueTree.OnDialoguePaused -= OnDialoguePaused;
        DialogueTree.OnSubtitlesRequest -= OnSubtitlesRequest;
        DialogueTree.OnMultipleChoiceRequest -= OnMultipleChoiceRequest;
    }

    void OnDialogueStarted(DialogueTree dlg)
    {
        DialogueCanvas.SetActive(true);
    }

    void OnDialogueFinished(DialogueTree dlg)
    {
        DialogueCanvas.gameObject.SetActive(false);
        if (dialogueCachedButtons != null)
        {
            foreach (var tempBtn in dialogueCachedButtons.Keys)
            {
                if (tempBtn != null)
                {
                    Destroy(tempBtn.gameObject);
                }
            }
            dialogueCachedButtons = null;
        }
    }

    void OnDialoguePaused(DialogueTree dlg)
    {
        //
    }

    void OnSubtitlesRequest(SubtitlesRequestInfo info)
    {
        StartCoroutine(Internal_OnSubtitlesRequestInfo(info));
    }

    IEnumerator Internal_OnSubtitlesRequestInfo(SubtitlesRequestInfo info)
    {
        var text = info.statement.text;

        subtitlesText.text = "";

        var tempText = "";

        for (int i = 0; i < text.Length; i++)
        {

            char c = text[i];
            tempText += c;
            yield return StartCoroutine(DelayPrint(subtitleDelays.characterDelay));
            if (c == '.' || c == '!' || c == '?')
            {
                yield return StartCoroutine(DelayPrint(subtitleDelays.sentenceDelay));
            }
            if (c == ',')
            {
                yield return StartCoroutine(DelayPrint(subtitleDelays.commaDelay));
            }

            subtitlesText.text = tempText;
        }
        waitForInput = true;

        while (waitForInput)
        {
            yield return null;
        }
        yield return null;
        info.Continue();
    }

    IEnumerator DelayPrint(float time)
    {
        var timer = 0f;
        while (timer < time)
        {
            timer += Time.deltaTime;
            yield return null;
        }
    }

    void OnMultipleChoiceRequest(MultipleChoiceRequestInfo info)
    {
        dialogueCachedButtons = new Dictionary<Button, int>();
        int i = 0;

        foreach (KeyValuePair<IStatement, int> pair in info.options)
        {
            var btn = (Button)Instantiate(dialogueOptionButton, dialogueOptionButton.transform.parent);
            btn.gameObject.SetActive(true);
            btn.GetComponent<Text>().text = pair.Key.text;
            dialogueCachedButtons.Add(btn, pair.Value);
            btn.onClick.AddListener(() => { Finalize(info, dialogueCachedButtons[btn]); });
            i++;
        }

        if (info.availableTime > 0)
        {
            StartCoroutine(CountDown(info));
        }
    }

    IEnumerator CountDown(MultipleChoiceRequestInfo info)
    {
        isWaitingChoice = true;
        var timer = 0f;
        while (timer < info.availableTime)
        {
            if (isWaitingChoice == false)
            {
                yield break;
            }
            yield return null;
        }

        if (isWaitingChoice)
        {
            Finalize(info, info.options.Values.Last());
        }
    }

    void Finalize(MultipleChoiceRequestInfo info, int index)
    {
        isWaitingChoice = false;
        if (info.showLastStatement)
        {
            //
        }
        foreach (var tempBtn in dialogueCachedButtons.Keys)
        {
            Destroy(tempBtn.gameObject);
        }
        info.SelectOption(index);
    }

    private void ShowNote(string noteId)
    {
        NoteCanvas.SetActive(true);
        NodeIdText.text = noteId;
        currentEntityId = noteId;

        GameEventsDebugText.text += "\n PLAYER EVENT: Show note";
    }

    public void CloseNotePanel()
    {
        NoteCanvas.SetActive(false);

        GameEventsDebugText.text += "\n PLAYER EVENT: Close note";
    }

    private void OpenInventory()
    {
        InventoryCanvas.SetActive(true);
        GameEventsDebugText.text += "\n PLAYER EVENT: Open Inventory";
    }

    private void CloseInventory()
    {
        InventoryCanvas.SetActive(false);
        GameEventsDebugText.text += "\n PLAYER EVENT: Close Inventory";
    }

    private void ChangeVisibilityInventory()
    {
        if (InventoryCanvas.activeSelf)
        {
            EventBus.RaiseEvent<IInventoryHandler>(h => h.OnCloseInventory());
            return;
        }
        EventBus.RaiseEvent<IInventoryHandler>(h => h.OnOpenInventory());
    }

    private void Update()
    {
        TimeText.text = TimeOfDay.GetParseTime();
    }

    public void OnLeftClick(InputAction.CallbackContext context)
    {
        if (context.action.phase == InputActionPhase.Started)
        {
            waitForInput = false;
        }

        if (context.action.phase == InputActionPhase.Canceled)
        {
            StartCoroutine(EDeactivateEntityMenuActions());
        }
    }

    private void ChangeAmmunitionText(in BaseWeapon weapon, int ammunitionAmount, int magazineAmount)
    {
        AmmunitionAmountText.text = weapon.name + ": ["+ magazineAmount .ToString()+ "] Ammunition: " + ammunitionAmount.ToString();
    }

    private void ActivateEntityActionsMenu(in Entity entity)
    {
        HideEntityDescription();

        Vector3 menuPos = m_MainCamera.ScreenToWorldPoint(PlayerInputManager.instance.MousePosition);
        menuPos.z = 0;
        EntityActionsMenuCanvas.position = menuPos;
        EntityActionsMenuCanvas.gameObject.SetActive(true);
        entityActionsMenuOptionButton.gameObject.SetActive(false);

        entityActionsMenuCachedButtons = new Dictionary<Button, int>();
        int i = 0;

        foreach (EntityAction action in entity.GetComponents<EntityAction>())
        {
            var btn = (Button)Instantiate(entityActionsMenuOptionButton, entityActionsMenuOptionButton.transform.parent);
            btn.gameObject.SetActive(true);
            btn.GetComponentInChildren<Text>().text = action.ActionName;
            entityActionsMenuCachedButtons.Add(btn, i);
            btn.onClick.AddListener(() => { action.StartUseAction(); });
            i++;
        }
    }

    public void OnRightClick(InputAction.CallbackContext context)
    {
        if (context.action.phase == InputActionPhase.Started)
        {
            StartCoroutine(EDeactivateEntityMenuActions());
        }
    }

    IEnumerator EDeactivateEntityMenuActions()
    {
        if (EntityActionsMenuCanvas.gameObject.activeSelf)
        {
            yield return new WaitForSeconds(0.01f);
            foreach (var tempBtn in entityActionsMenuCachedButtons.Keys)
            {
                Destroy(tempBtn.gameObject);
            }
            EntityActionsMenuCanvas.gameObject.SetActive(false);
        }

        yield break;

    }

    private void TakeToInventory(in Entity entity)
    {
        GameObject item = Instantiate(InventoryContent.GetChild(0).gameObject, InventoryContent);

        GameEventsDebugText.text += "\n PLAYER EVENT: Take to Inventory '" + entity.Name + "'";
    }

    private void ShowEntityDescription(string description)
    {
        Vector3 pos = m_MainCamera.ScreenToWorldPoint(PlayerInputManager.instance.MousePosition);
        pos.z = 0;
        EntityDescriptionCanvas.position = pos;

        if (EntityDescriptionCanvas.gameObject.activeSelf)
            return;

        EntityDescriptionCanvas.gameObject.SetActive(true);
        EntityDescriptionText.text = description;
    }

    private void HideEntityDescription()
    {
        EntityDescriptionText.text = "";
        EntityDescriptionCanvas.gameObject.SetActive(false);
    }

    private void KillEntity(in Entity entity)
    {
        if (EntityDescriptionCanvas.gameObject.activeSelf)
        {
            HideEntityDescription();
        }

        GameEventsDebugText.text += "\n GLOBAL EVENT: Kill Entity '" + entity.Name + "'";
    }

    public void OnDevDebug(InputAction.CallbackContext context)
    {
        if (context.action.phase == InputActionPhase.Started)
        {
            GameEventsDebugCanvas.SetActive(!GameEventsDebugCanvas.activeSelf);
        }
    }

    public void OnOpenNote(string noteId)
    {
        ShowNote(noteId);
    }

    public void OnCloseNote()
    {

    }

    public void OnOpenInventory()
    {
        OpenInventory();
    }

    public void OnCloseInventory()
    {
        CloseInventory();
    }

    public void OnUpdateAmountAmmunition(in BaseWeapon weapon, int ammunitionAmount, int magazineAmount)
    {
        ChangeAmmunitionText(weapon, ammunitionAmount, magazineAmount);
    }

    public void OnTakeToInventory(Entity entity)
    {
    }

    public void OnWeaponHit(in Entity entity, float value)
    {

    }

    public void OnKillEntity(Entity entity)
    {
        KillEntity(entity);
    }

    public void OnShowDescription(string description)
    {
        ShowEntityDescription(description);
    }

    public void OnHideDescription()
    {
        HideEntityDescription();
    }

    public void OnShowActionMenu(in Entity entity)
    {
        ActivateEntityActionsMenu(entity);
    }

    public void OnStartSprint()
    {

    }

    public void OnStopSprint()
    {

    }

    public void OnInventory()
    {
        ChangeVisibilityInventory();
    }

    public void OnFlashlight()
    {

    }
}
