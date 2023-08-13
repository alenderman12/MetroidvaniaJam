using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Create/EnemyData")]

public class EnemyType : ScriptableObject
{
    public float enemyLife, enemyDamage, knockbackForce, enemySpeed;

    public void Move()
    {

    }
}
