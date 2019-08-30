using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowSkin : MonoBehaviour
{
    public UISkinMgr m_skinMgr;

    private void Start()
    {
        m_skinMgr.SetOnCenterEvent(SkinOnCenter);
        m_skinMgr.LoadSkin();
    }

    /// <summary>
    /// 皮肤居中显示事件
    /// </summary>
    /// <param name="define"></param>
    public void SkinOnCenter(SkinConfigDefine define)
    {
        if (define != null)
        {
            Debug.Log("选择皮肤ID为 ：" + define.ID);
        }
    }
}
