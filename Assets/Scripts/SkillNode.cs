using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum SkillNodeState
{
    Locked,
    Available,
    Unlocked
}

public class SkillNode : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string nodeID;

    public void OnPointerEnter(PointerEventData eventData)
    {
        SkillManager.Instance.ShowNodeTooltip(nodeID);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SkillManager.Instance.ShowDefaultTooltip();
    }

    public void SelectNode()
    {
        // Your existing selection logic here…

        SkillManager.Instance.RefreshPlayerStats();
    }

    public List<SkillNode> prerequisites = new List<SkillNode>(); 
    public List<SkillNode> children = new List<SkillNode>();      

    public SkillNodeState state = SkillNodeState.Locked;

    public Button button;
    public Image icon;
    public GameObject lockOverlay;

    public Color lockedColor = Color.gray;
    public Color availableColor = Color.white;
    public Color unlockedColor = Color.yellow;

    private void Start()
    {
        button = GetComponent<Button>();
        icon = GetComponent<Image>();

        if (button != null)
        {
            button.onClick.RemoveAllListeners();   // Ensures the listener is clean
            button.onClick.AddListener(OnClick);
        }

        RecalculateState();
        UpdateVisuals();
    }


    private void OnClick()
    {
        RecalculateState();
        switch (state)
        {
            case SkillNodeState.Available:
                if (!SkillManager.Instance.CanAffordSkill())
                {
                    Debug.Log("Not enough skill points to unlock " + nodeID);
                    return;
                }
                Unlock();
                break;
                    
            case SkillNodeState.Unlocked: // allow unselecting
                LockRecursive();
                break;
        }
    }


    public void RecalculateState()
    {
        if (state == SkillNodeState.Unlocked)
            return;

        if (prerequisites.Count == 0)
        {
            state = SkillNodeState.Available;
            return;
        }

        foreach (var pre in prerequisites)
        {
            if (pre.state != SkillNodeState.Unlocked)
            {
                state = SkillNodeState.Locked;
                return;
            }
        }

        // All prereqs unlocked → Available
        state = SkillNodeState.Available;
    }


    public void Unlock()
    {
        SkillManager.Instance.SpendSkillPoint(nodeID);
        state = SkillNodeState.Unlocked;
        UpdateVisuals();


        foreach (var child in children)
            child.RecalculateAndUpdate();
    }


    private void LockRecursive()
    {
        SkillManager.Instance.RefundSkillPoint(nodeID);
        state = SkillNodeState.Available;
        UpdateVisuals();

        foreach (var child in children)
            child.ResetSubtreeToLocked();
    }


    public void RecalculateAndUpdate()
    {
        RecalculateState();
        UpdateVisuals();

        foreach (var child in children)
            child.RecalculateAndUpdate();
    }

    private void ResetSubtreeToLocked()
    {
        SkillManager.Instance.RefundSkillPoint(nodeID);
        state = SkillNodeState.Locked;
        UpdateVisuals();

        foreach (var child in children)
            child.ResetSubtreeToLocked();
    }

    [System.Obsolete]
    private void UpdateVisuals()
    {
        foreach (var path in FindObjectsOfType<SkillPath>())
            path.Refresh();

        switch (state)
        {
            case SkillNodeState.Locked:
                if (lockOverlay) lockOverlay.SetActive(true);
                if (button) button.interactable = false;
                if (icon) icon.color = lockedColor;
                break;

            case SkillNodeState.Available:
                if (lockOverlay) lockOverlay.SetActive(false);
                if (button) button.interactable = true;
                if (icon) icon.color = availableColor;
                break;

            case SkillNodeState.Unlocked:
                if (lockOverlay) lockOverlay.SetActive(false);
                if (button) button.interactable = true;
                if (icon) icon.color = unlockedColor;
                break;
        }
    }
}
