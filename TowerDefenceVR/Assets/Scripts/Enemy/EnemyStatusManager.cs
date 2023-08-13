using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵のステータスを管理するクラス
/// </summary>
public class EnemyStatusManager
{
    public float HP { get; private set; }

    private float normalDamage = 10.0f;

    public EnemyStatusManager(float hp)
    {
        HP = hp;
    }

    public void NormalDamage()
    {
        HP -= normalDamage;
        if(HP <= 0)
        {
            HP = 0;
        }
    }
}
