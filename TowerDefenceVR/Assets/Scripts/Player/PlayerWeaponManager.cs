using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーの武器を管理するクラス
/// </summary>
public class PlayerWeaponManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> allWeaponList;
    [SerializeField] private Transform weaponShowPos;
    private List<Rigidbody> allWeaponRigidbodyList = new List<Rigidbody>();
    private List<GameObject> getWeaponList = new List<GameObject>();
    private int weaponNum = 0; //表示させる武器を管理するインデックス
    private GameObject showedWeapon; //表示させている武器

    private void Start() 
    {
        //各武器のRigidbodyコンポーネントを取得し無効化する
        foreach(var weapon in allWeaponList)
        {
            allWeaponRigidbodyList.Add(weapon.GetComponent<Rigidbody>());
            allWeaponRigidbodyList[allWeaponRigidbodyList.Count - 1].isKinematic = true;
        }
    }

    //武器を入手する
    public void AddWeapon(GameObject newWeapon)
    {
        //入手した武器をリストに追加する
        foreach(var weapon in allWeaponList)
        {
            if(newWeapon.name == weapon.name)
            {
                getWeaponList.Add(weapon);
            }
        }
    }

    //武器をしまう
    public void PutawayWeapon(GameObject grabWeapon)
    {
        Destroy(grabWeapon);
    }

    //武器を取り出す
    public void TakeoutWeapon()
    {
        if(getWeaponList.Count == 0) return; //入手済みの武器がない場合は処理を終了する
        if(showedWeapon != null) Destroy(showedWeapon); //表示させている武器がある場合は削除する

        //入手済みの武器を順番に表示させる
        showedWeapon = Instantiate(getWeaponList[weaponNum], weaponShowPos.position, weaponShowPos.rotation);

        weaponNum++;
        if(weaponNum >= getWeaponList.Count)
        {
            weaponNum = 0;
        }
    }
}
