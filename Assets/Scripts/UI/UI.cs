using UnityEngine;

public class UI : MonoBehaviour
{
    public UI_SkillTree skillTreeUI { get; private set; }


    private bool skillTreeEnabled;
    private bool inventoryEnabled;

    private void Awake()
    {
        skillTreeUI = GetComponentInChildren<UI_SkillTree>(true);
    }

    public void ToggleSkillTreeUI()
    {
        return;


    }


}
