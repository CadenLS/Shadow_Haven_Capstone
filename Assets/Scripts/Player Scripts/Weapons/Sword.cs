using UnityEngine;

public class Sword : MeleeAttackBase
{

    private void Start()
    {

        damage = 5f;
        attackRadius = 0.6f;
        enemyLayer = LayerMask.GetMask("Enemy");

    }

    public override void Attack()
    {
        base.Attack();
    }

}
