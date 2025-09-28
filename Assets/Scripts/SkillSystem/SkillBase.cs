using UnityEngine;

public class SkillBase : MonoBehaviour
{
    [Header("General details")]
    [SerializeField] protected float cooldown;
    private float lastTimeUsed;

    protected virtual void Awake()
    {
        lastTimeUsed = lastTimeUsed - cooldown;
    }
    public bool CanUseSkill()
    {

        if (OnCooldown())
        {
            Debug.Log("On Cooldown");
            return false;
        }

        return true;
    }

    protected bool OnCooldown() => Time.time < lastTimeUsed + cooldown;
    public void SetSkillOnCooldown() => lastTimeUsed = Time.time;
    public void ResetCooldownBy(float cooldownReduction) => lastTimeUsed = lastTimeUsed + cooldownReduction;
    public void ResetCooldown() => lastTimeUsed = Time.time;
}
