using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class svCtr : MonoBehaviour {

    // Use this for initialization
    void Start () {

        var panel = NGUITools.FindInParents<UIPanel>(gameObject);
        var allItem = new List<Item>();

        for (int i = 0; i < 7; i++)
        {
            var tTransform = transform.Find("Item" + (i + 1));
            var uITexture = transform.Find("Item" + (i + 1)+ "/Texture").gameObject.GetComponent<UITexture>();

            var item = new Item
            {
                transform = tTransform,
                uITexture = uITexture
            };


            allItem.Add(item);
        }

        panel.onClipMove = (tPanel) =>
        {
            var x = tPanel.clipOffset.x;
            for (int i = 0; i < allItem.Count; i++)
            {
                var item= allItem[i];
                var localX = item.transform.localPosition.x;
                var scale = 1f;

                if (Mathf.Abs(x - localX) > 200)
                    scale = 1f;
                else
                {
                    scale = 1 + Mathf.Cos(Mathf.Abs(x - localX) / 75) * 0.3f;//Mathf.Abs(allItem[i].localPosition.x - x) / 320;
                    scale = Mathf.Clamp(scale, 1, 1.3f);
                }

                item.transform.localScale = Vector3.one * scale;

                var cNum = scale / 1.3f;
                cNum = Mathf.Pow(cNum,2);
                item.uITexture.color =new Color(cNum, cNum, cNum, 1);

                if (scale > 1.2f)
                    item.uITexture.depth = 2;
                else
                    item.uITexture.depth = 1;
            }
        };

    }
	
}


class Item
{
    public Transform transform { get; set;}

    public UITexture uITexture { get; set;}
}