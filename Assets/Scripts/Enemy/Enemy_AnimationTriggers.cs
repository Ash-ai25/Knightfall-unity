using UnityEngine;

public class Enemy_AnimationTriggers : Entity_AnimationTrigger
{
    private Enemy enemy;
    private Enemy_Vfx enemyVfx;

    protected override void Awake()
    {
        base.Awake();
        enemy = GetComponentInParent<Enemy>();
        enemyVfx = GetComponentInParent<Enemy_Vfx>();
    }

    private void EnableCounterWindow()
    {
        enemyVfx.EnableAttackAlert(true);
        enemy.EnableCounterWindow(true);
    }

    private void DisableCounterWindow()
    {
        enemyVfx.EnableAttackAlert(false);
        enemy.EnableCounterWindow(false);
    }

}
