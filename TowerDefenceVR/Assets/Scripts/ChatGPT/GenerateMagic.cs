using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMagic : MonoBehaviour
{
    private string attributeKey;
    private string powerKey;
    private string message;

    Dictionary<string, Action<string>> actionMap;

    private void Awake()
    {
        actionMap = new Dictionary<string, Action<string>>()
        {
            {"火", (powerKey) => {
                int powerGroup = int.Parse(powerKey);
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
                        message ="火の呪文";
                        break;
                    default:
                        message = "火でエラー";
                        break;
                }
            }},
            {"水", (powerKey) => {
                int powerGroup = int.Parse(powerKey);
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
                        message ="水の呪文";
                        break;
                    default:
                        message = "水でエラー";
                        break;
                }
            }},
            {"氷", (powerKey) => {
                int powerGroup = int.Parse(powerKey);
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
                        message ="氷の呪文";
                        break;
                    default:
                        message = "氷でエラー";
                        break;
                }
            }},
            {"風", (powerKey) => {
                int powerGroup = int.Parse(powerKey);
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
                        message ="風の呪文";
                        break;
                    default:
                        message = "風でエラー";
                        break;
                }
            }},
            {"雷", (powerKey) => {
                int powerGroup = int.Parse(powerKey);
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
                        message ="雷の呪文";
                        break;
                    default:
                        message = "雷でエラー";
                        break;
                }
            }},
            {"光", (powerKey) => {
                int powerGroup = int.Parse(powerKey);
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
                        message ="光の呪文";
                        break;
                    default:
                        message = "光でエラー";
                        break;
                }
            }},
            {"闇", (powerKey) => {
                int powerGroup = int.Parse(powerKey);
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
                        message ="闇の呪文";
                        break;
                    default:
                        message = "闇でエラー";
                        break;
                }
            }}
        };
    }

    public GenerateMagic(string key)
    {
        string[] keyArray = key.Split('-');
        this.attributeKey = keyArray[0];
        this.powerKey = keyArray[1];
    }

    public string Generate()
    {
        if(actionMap.ContainsKey(attributeKey))
        {
            actionMap[attributeKey](powerKey);
            if(string.IsNullOrEmpty(message)) message = "エラー1";

            return message;
        }
        else
        {
            message = "エラー2";
            return message;
        }
    }
}
