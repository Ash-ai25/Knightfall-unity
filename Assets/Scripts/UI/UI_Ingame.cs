using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Ingame : MonoBehaviour
{
    [SerializeField]private Player player;

    [SerializeField] private RectTransform healthRect;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TextMeshProUGUI healthText;

    private void Start()
    {
        player = FindFirstObjectByType<Player>();
        player.health.OnHealthUpdate += UpdateHealthBar;
    }
    private void UpdateHealthBar()
    {
        float currentHealth = Mathf.RoundToInt(player.health.GetCurrentHealth());
        float maxHealth = player.stats.GetMaxHealth();
        
        healthText.text = currentHealth + "/ " + maxHealth;
        healthSlider.value = player.health.GetHealthPercent();
    }
}
