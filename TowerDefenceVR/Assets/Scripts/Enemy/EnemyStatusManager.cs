using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵のステータスを管理するクラス
/// </summary>
public class EnemyStatusManager
{
    public float HP { get; private set; }

    private float swordDamage = 10.0f;
    private float magicDamage = 25.0f;

    public EnemyStatusManager(float hp)
    {
        HP = hp;
    }

    public void SwordDamage()
    {
        HP -= swordDamage;
        if(HP <= 0)
        {
            HP = 0;
        }
    }

    public void MagicDamage()
    {
        HP -= magicDamage;
        if (HP <= 0)
        {
            HP = 0;
        }
    }
}
