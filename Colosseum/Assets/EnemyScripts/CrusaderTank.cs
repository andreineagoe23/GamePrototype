using UnityEngine;

public class CrusaderTank : Enemy
{
    public override void Attack()
    {
        base.Attack(); // Retain base attack behavior

        Debug.Log("CrusaderTank attacks the player with heavy damage!");
    }
}
