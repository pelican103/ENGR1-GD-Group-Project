using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PathVisualState
{
    Locked,
    Available,
    Active
}

public class SkillPath : MonoBehaviour
{
    public List<SkillNode> fromNodes = new List<SkillNode>(); 
    public SkillNode toNode;                                  

    public Transform spriteContainer;              

    public Color lockedColor;
    public Color availableColor;
    public Color activeColor;

    private Image[] sprites;

    private void Awake()
    {
        if (spriteContainer == null)
            spriteContainer = transform; 

        sprites = spriteContainer.GetComponentsInChildren<Image>();

        UpdateVisual();
    }

    private void UpdateVisual()
    {
        PathVisualState displayState = GetState();
        Color chosenColor = lockedColor;

        switch (displayState)
        {
            case PathVisualState.Locked:
                chosenColor = lockedColor;
                break;
            case PathVisualState.Available:
                chosenColor = availableColor;
                break;
            case PathVisualState.Active:
                chosenColor = activeColor;
                break;
        }

        foreach (var sr in sprites)
        {

            //Debug.Log(displayState.ToString());
            sr.color = chosenColor;
        }
    }

    private PathVisualState GetState()
    {
        // If child is unlocked path is active
        if (toNode.state == SkillNodeState.Unlocked)
            return PathVisualState.Active;

        // Check if ALL prereqs for the node are unlocked
        bool allParentsUnlocked = true;

        foreach (var pre in fromNodes)
        {
            if (pre.state != SkillNodeState.Unlocked)
            {
                allParentsUnlocked = false;
                break;
            }
        }

        if (allParentsUnlocked)
            return PathVisualState.Available;

        return PathVisualState.Locked;
    }

    // Called when any node changes state
    public void Refresh()
    {
        UpdateVisual();
    }

}
