using UnityEngine;

public class WolfBoss : Enemy
{
    public float specialAttackCooldown = 10f;
    private float nextSpecialAttackTime;

    public override void Attack()
    {
        base.Attack(); // Retain base attack behavior

        if (Time.time >= nextSpecialAttackTime)
        {
            PerformSpecialAttack();
            nextSpecialAttackTime = Time.time + specialAttackCooldown;
        }
    }

    private void PerformSpecialAttack()
    {
        Debug.Log("WolfBoss performs a devastating special attack!");
    }
}
