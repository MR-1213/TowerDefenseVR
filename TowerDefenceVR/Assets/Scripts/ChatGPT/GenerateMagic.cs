using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMagic : MonoBehaviour
{
    [SerializeField] ChangeHandAndController changeHandAndController;
    [SerializeField] Transform magicParent;
    [SerializeField, Tooltip("0:火, 1:水, 2:氷, 3:風, 4:雷, 5:土, 6:光, 7:闇")] GameObject[] magics = new GameObject[8];
    private string attributeKey;
    private string powerKey;
    private string message;
    private GameObject selectedMagic;

    Dictionary<string, Action<string>> actionDictionary = new Dictionary<string, Action<string>>();

    private void Start()
    {
        actionDictionary = new Dictionary<string, Action<string>>()
        {
            //炎魔法の場合
            {"火", (powerKey) => {
                int powerGroup = int.Parse(powerKey);
                if(powerGroup < 0 || powerGroup > 100)
                {
                    selectedMagic = null;
                }
                else
                {
                    switch(powerGroup)
                    {
                        case 0:
                        case 10:
                        case 20:     
                        case 30:
                        case 40:
                        case 50:
                        case 60:
                        case 70:
                        case 80:
                        case 90:
                        case 100:
                            selectedMagic = Instantiate(magics[0], magicParent);
                            break;
                        default:
                            selectedMagic = null;
                            break;
                    }
                }
            }},
            //水魔法の場合
            {"水", (powerKey) => {
                int powerGroup = int.Parse(powerKey);
                if(powerGroup < 0 || powerGroup > 100)
                {
                    selectedMagic = null;
                }
                else
                {
                    switch(powerGroup)
                    {
                        case 0:
                        case 10:
                        case 20:
                        case 30:
                        case 40:
                        case 50:
                        case 60:
                        case 70:
                        case 80:
                        case 90:
                        case 100:
                            selectedMagic = Instantiate(magics[1], magicParent);
                            break;
                        default:
                            selectedMagic = null;
                            break;
                    }
                }
            }},
            //氷魔法の場合
            {"氷", (powerKey) => {
                int powerGroup = int.Parse(powerKey);
                if(powerGroup < 0 || powerGroup > 100)
                {
                    selectedMagic = null;
                }
                else
                {
                    switch(powerGroup)
                    {
                        case 0:
                        case 10:
                        case 20:
                        case 30:
                        case 40:
                        case 50:
                        case 60:
                        case 70:
                        case 80:
                        case 90:
                        case 100:
                            selectedMagic = Instantiate(magics[2], magicParent);
                            break;
                        default:
                            selectedMagic = null;
                            break;
                    }
                }
            }},
            //風魔法の場合
            {"風", (powerKey) => {
                int powerGroup = int.Parse(powerKey);
                if(powerGroup < 0 || powerGroup > 100)
                {
                    selectedMagic = null;
                }
                else
                {
                    switch(powerGroup)
                    {
                        case 0:
                        case 10:
                        case 20:
                        case 30:
                        case 40:
                        case 50:
                        case 60:
                        case 70:
                        case 80:
                        case 90:
                        case 100:
                            selectedMagic = Instantiate(magics[3], magicParent);
                            break;
                        default:
                            selectedMagic = null;
                            break;
                    }
                }
            }},
            //雷魔法の場合
            {"雷", (powerKey) => {
                int powerGroup = int.Parse(powerKey);
                if(powerGroup < 0 || powerGroup > 100)
                {
                    selectedMagic = null;
                }
                else
                {
                    switch(powerGroup)
                    {
                        case 0:
                        case 10:
                        case 20:
                        case 30:
                        case 40:
                        case 50:
                        case 60:
                        case 70:
                        case 80:
                        case 90:
                        case 100:
                            selectedMagic = Instantiate(magics[4], magicParent);
                            break;
                        default:
                            selectedMagic = null;
                            break;
                    }
                }
            }},
            {"土", (powerKey) => {
                int powerGroup = int.Parse(powerKey);
                if(powerGroup < 0 || powerGroup > 100)
                {
                    selectedMagic = null;
                }
                else
                {
                    switch(powerGroup)
                    {
                        case 0:
                        case 10:
                        case 20:
                        case 30:
                        case 40:
                        case 50:
                        case 60:
                        case 70:
                        case 80:
                        case 90:
                        case 100:
                            selectedMagic = Instantiate(magics[5], magicParent);
                            break;
                        default:
                            selectedMagic = null;
                            break;
                    }
                }
            }},
            //光魔法の場合
            {"光", (powerKey) => {
                int powerGroup = int.Parse(powerKey);
                if(powerGroup < 0 || powerGroup > 100)
                {
                    selectedMagic = null;
                }
                else
                {
                    switch(powerGroup)
                    {
                        case 0:
                        case 10:
                        case 20:
                        case 30:
                        case 40:
                        case 50:
                        case 60:
                        case 70:
                        case 80:
                        case 90:
                        case 100:
                            selectedMagic = Instantiate(magics[6], magicParent);
                            break;
                        default:
                            selectedMagic = null;
                            break;
                    }
                }
            }},
            //闇魔法の場合
            {"闇", (powerKey) => {
                int powerGroup = int.Parse(powerKey);
                if(powerGroup < 0 || powerGroup > 100)
                {
                    selectedMagic = null;
                }
                else
                {
                    switch(powerGroup)
                    {
                        case 0:
                        case 10:
                        case 20:
                        case 30:
                        case 40:
                        case 50:
                        case 60:
                        case 70:
                        case 80:
                        case 90:
                        case 100:
                            selectedMagic = Instantiate(magics[7], magicParent);
                            break;
                        default:
                            selectedMagic = null;
                            break;
                    }
                }
            }}
        };
    }

    public GameObject Generate(string key)
    {
        string[] keyArray = key.Split('-');
        attributeKey = keyArray[0];
        powerKey = keyArray[1];

        if(string.IsNullOrEmpty(attributeKey) || string.IsNullOrEmpty(powerKey))
        {
            selectedMagic = null;
            return selectedMagic;
        }
        
        if(actionDictionary.ContainsKey(attributeKey))
        {
            actionDictionary[attributeKey](powerKey);
            if(selectedMagic == null)
            {
                return selectedMagic;
            }
            //魔法生成の際はコントローラーから手に変更する
            changeHandAndController.Switch();
            //手のアンカーオブジェクトの子オブジェクトとして生成する
            selectedMagic.transform.SetParent(magicParent.transform);
            return selectedMagic;
        }
        else
        {
            selectedMagic = null;
            return selectedMagic;
        }
    }
}
