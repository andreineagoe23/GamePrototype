using UnityEngine;

public class Wolf : Enemy
{
    protected override void Start()
    {
        base.Start();
        moveSpeed = 6f; // Wolves are faster than default enemies
        maxHealth = 50; // Lower health for a basic enemy type
    }
}
