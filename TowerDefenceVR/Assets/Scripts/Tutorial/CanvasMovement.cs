using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// チュートリアル中、音声テキストを表示するキャンバスを移動させる際に使うクラス
/// </summary>
public class CanvasMovement : MonoBehaviour
{
    public List<RectTransform> nextCanvasPositionList = new List<RectTransform>();
    [SerializeField] private Transform playerPos;
    private int counter = 0;

    private void Start() 
    {
        if(nextCanvasPositionList.Count == 0) return;

        Transform nextPos = nextCanvasPositionList[0];
        nextCanvasPositionList.RemoveAt(0);
        counter++;

        transform.position = nextPos.position;
        transform.rotation = nextPos.rotation;
        transform.localScale = nextPos.localScale;

    }

    public void MoveNext()
    {
        if(nextCanvasPositionList.Count == 0) return;

        Transform nextPos = nextCanvasPositionList[0];
        nextCanvasPositionList.RemoveAt(0);
        counter++;

        if(counter == 3)
        {
            MoveFrontOfPlayer(nextPos);
        }

        transform.position = nextPos.position;
        transform.rotation = nextPos.rotation;
        transform.localScale = nextPos.localScale;
    }

    private void MoveFrontOfPlayer(Transform nextPos)
    {
        //プレイヤーの前に移動させる
        nextPos.position = playerPos.position + playerPos.forward * 3.0f;
        if(nextPos.position.y < 2.0f)
        {
            nextPos.position = new Vector3(nextPos.position.x, 2.0f, nextPos.position.z);
        }
        nextPos.rotation = playerPos.rotation;
    }
}
