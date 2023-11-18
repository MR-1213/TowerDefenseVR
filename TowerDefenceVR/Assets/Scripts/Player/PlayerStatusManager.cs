using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusManager : MonoBehaviour
{
    [SerializeField] private Image image1;
    [SerializeField] private Image image2;


    private void Update()
    {
 
        // Fill Amountによってゲージの色を変える
        if (image2.fillAmount > 0.7f)
        {
            image1.color = Color.red;
        }
        else if (image2.fillAmount > 0.5f)
        {
            image1.color = new Color(1f, 0.67f, 0f);
        }
        else 
        {
            image1.color = Color.green;
        }
        

        if(image2.fillAmount > 0f) 
        {
            image2.fillAmount -= Time.deltaTime / 20f;
        }
 
    }

    private void Damage()
    {
        image2.fillAmount += 0.2f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy") && other.gameObject.name == "AttackCollider")
        {
            Damage();
        }
    }
}
