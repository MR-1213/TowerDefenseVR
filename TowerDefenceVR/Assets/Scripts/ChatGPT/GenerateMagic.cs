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
    private int selectedMagicIndex = -1;
    private Dictionary<int, GameObject> savedMagicDictionary =  new Dictionary< int, GameObject>();

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
                            selectedMagic = Instantiate(magics[0], magicParent.position, magicParent.rotation, magicParent);
                            selectedMagicIndex = 0;
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
                            selectedMagic = Instantiate(magics[1], magicParent.position, magicParent.rotation, magicParent);
                            selectedMagicIndex = 1;
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
                            selectedMagic = Instantiate(magics[2], magicParent.position, magicParent.rotation, magicParent);
                            selectedMagicIndex = 2;
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
                            selectedMagic = Instantiate(magics[3], magicParent.position, magicParent.rotation, magicParent);
                            selectedMagicIndex = 3;
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
                            selectedMagic = Instantiate(magics[4], magicParent.position, magicParent.rotation, magicParent);
                            selectedMagicIndex = 4;
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
                            selectedMagic = Instantiate(magics[5], magicParent.position, magicParent.rotation, magicParent);
                            selectedMagicIndex = 5;
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
                            selectedMagic = Instantiate(magics[6], magicParent.position, magicParent.rotation, magicParent);
                            selectedMagicIndex = 6;
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
                            selectedMagic = Instantiate(magics[7], magicParent.position, magicParent.rotation, magicParent);
                            selectedMagicIndex = 7;
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
            //指定の魔法が存在する場合は威力に応じて取得する
            actionDictionary[attributeKey](powerKey);
            //魔法が取得できなかった場合はnullを返す
            if(selectedMagic == null)
            {
                return selectedMagic;
            }

            //魔法生成の際はコントローラーから手に変更する
            changeHandAndController.Switch();
            
            return selectedMagic;
        }
        else
        {
            //指定の魔法が存在しない場合はnullを返す
            selectedMagic = null;
            return selectedMagic;
        }
    }

    public void GenerateSavedMagic(int buttonIndexCount)
    {
        if(savedMagicDictionary.ContainsKey(buttonIndexCount) && selectedMagicIndex == -1)
        {
            Instantiate(savedMagicDictionary[buttonIndexCount], magicParent.position, magicParent.rotation, magicParent);
        }
        else
        {
            switch(selectedMagicIndex)
            {
                case 0:
                    savedMagicDictionary.Add(buttonIndexCount, magics[0]);
                    break;
                case 1:
                    savedMagicDictionary.Add(buttonIndexCount, magics[1]);
                    break;
                case 2:
                    savedMagicDictionary.Add(buttonIndexCount, magics[2]);
                    break;
                case 3:
                    savedMagicDictionary.Add(buttonIndexCount, magics[3]);
                    break;
                case 4:
                    savedMagicDictionary.Add(buttonIndexCount, magics[4]);
                    break;
                case 5:
                    savedMagicDictionary.Add(buttonIndexCount, magics[5]);
                    break;
                case 6:
                    savedMagicDictionary.Add(buttonIndexCount, magics[6]);
                    break;
                case 7:
                    savedMagicDictionary.Add(buttonIndexCount, magics[7]);
                    break;
            }

            Instantiate(savedMagicDictionary[buttonIndexCount], magicParent.position, magicParent.rotation, magicParent);
            selectedMagicIndex = -1;
        }
    }
}
