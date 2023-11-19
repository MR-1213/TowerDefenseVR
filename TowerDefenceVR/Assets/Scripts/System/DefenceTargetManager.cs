using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Cysharp.Threading.Tasks;

public class DefenceTargetManager : MonoBehaviour
{
    private static float currentHP = 600.0f;

    [SerializeField] private Slider hpSlider;

    private void Start()
    {
        hpSlider.minValue = 0.0f;
        hpSlider.maxValue = currentHP;
        currentHP -= 100.0f;
        hpSlider.value = currentHP;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemySword"))
        {
            // 敵の剣に当たった場合
            DecreaseHP();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("EnemyMagic"))
        {
            // 敵の魔法に当たった場合
            DecreaseHP();
        }
    }

    private void DecreaseHP()
    {
        StartCoroutine(DecreaseHPWithDelay());
    }

    private IEnumerator DecreaseHPWithDelay()
    {
        yield return new WaitForSeconds(0.3f);

        // HPを減少
        currentHP -= 1.0f;
        if(currentHP <= 5.0f)
        {
            currentHP = 5.0f;
        }

        hpSlider.DOValue(currentHP, 0.2f);

    }
}
