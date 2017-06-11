using UnityEngine;
using System.Collections;

public class Life : MonoBehaviour
{

    RectTransform rt;

    void Start()
    {
        rt = GetComponent<RectTransform>();
        Debug.Log("LifeSize="+rt);
    }

    public void LifeDown(int ap)
    {
        //RectTransformのサイズを取得し、マイナスする
        rt.sizeDelta -= new Vector2(0, ap);
    }

    public void LifeUp(int hp)
    {
        //RectTransformのサイズを取得し、プラスする
        if (rt.sizeDelta.y <240f) {
            rt.sizeDelta += new Vector2(0, hp);
        }
    }
}