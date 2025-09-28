using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_TreeNode : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    private UI ui;
    private RectTransform rect;


    [Header("Unlock details")]
    public UI_TreeNode[] neededNodes;
    public UI_TreeNode[] conflictNodes;
    public bool isUnlocked;
    public bool isLocked;

    [Header("Skil details")]
    public Skill_DataSO skillData;
    [SerializeField] private string skillName;
    [SerializeField] private Image skillIcon;
    [SerializeField] private int skillCost;
    private Color lastColor;


    private void Awake()
    {
        ui = GetComponentInParent<UI>();
        rect = GetComponent<RectTransform>();

    }

    private void Start()
    {

    }


    private void Unlock()
    {
        isUnlocked = true;

    }

    private bool CanBeUnlocked()
    {
        if (isLocked || isUnlocked)
            return false;


        return true;
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        if (CanBeUnlocked())
            Unlock();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        if (isUnlocked || isLocked)
            return;

        ToggleNodeHighlight(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {

        if (isUnlocked || isLocked)
            return;

        ToggleNodeHighlight(false);
    }

    private void ToggleNodeHighlight(bool highlight)
    {
        Color highlightColor = Color.white * .9f; highlightColor.a = 1;
        Color colorToApply = highlight ? highlightColor : lastColor;

    }

    private Color GetColorByHex(string hexNumber)
    {
        ColorUtility.TryParseHtmlString(hexNumber, out Color color);

        return color;
    }


}
