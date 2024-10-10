using UnityEngine;

public class Sword : MeleeAttackBase
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {

        damage = 5f;
        attackRadius = 1f;
        enemyLayer = LayerMask.GetMask("Enemy");

    }

    public override void Attack()
    {
        base.Attack();
        Debug.Log("Sword attack!");
    }

}
