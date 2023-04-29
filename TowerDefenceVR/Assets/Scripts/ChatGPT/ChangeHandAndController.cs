using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeHandAndController : MonoBehaviour
{
    //index 0: left, 1: right
    [SerializeField] GameObject[] hands = new GameObject[2];
    //index 0: left, 1: right
    [SerializeField] GameObject[] controllers = new GameObject[2];
    public void Switch()
    {
        hands[0].SetActive(!hands[0].activeSelf);
        hands[1].SetActive(!hands[1].activeSelf);
        controllers[0].SetActive(!controllers[0].activeSelf);
        controllers[1].SetActive(!controllers[1].activeSelf);
    }

    public void SwitchToController()
    {
        if(hands[0].activeSelf && hands[1].activeSelf)
        {
            hands[0].SetActive(!hands[0].activeSelf);
            hands[1].SetActive(!hands[1].activeSelf);
            controllers[0].SetActive(!controllers[0].activeSelf);
            controllers[1].SetActive(!controllers[1].activeSelf);
        }
        
    }

    public void SwitchToHand()
    {
        if(controllers[0].activeSelf && controllers[1].activeSelf)
        {
            hands[0].SetActive(!hands[0].activeSelf);
            hands[1].SetActive(!hands[1].activeSelf);
            controllers[0].SetActive(!controllers[0].activeSelf);
            controllers[1].SetActive(!controllers[1].activeSelf);
        }
    }
}
