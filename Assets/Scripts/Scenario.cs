using UnityEngine;

[CreateAssetMenu(
    menuName = "Create Scenario", 
    fileName = "Scenario", 
    order = 120)]
public class Scenario : ScriptableObject
{
    [SerializeField] private Step[] _steps;

    public void Start()
    {
        
    }

    public void Break()
    {
        
    }
}


public abstract class UIElement: MonoBehaviour
{
    public abstract void LoadData(Step step);
}

public class DialogMenu : UIElement
{
    public override void LoadData(Step step)
    {
        
    }
}

public class InformationMenu : UIElement
{
    public override void LoadData(Step step)
    {
        
    }
}

