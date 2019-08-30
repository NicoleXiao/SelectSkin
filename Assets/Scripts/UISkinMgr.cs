using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FancyScrollView;
using UnityEngine.UI;
using System;

public class UISkinMgr : FancyScrollView<SkinConfigDefine>
{
    public ScrollPositionController scrollPositionController;
    private bool m_isInit = false;
    private void InitSkin()
    {
        scrollPositionController.OnItemSelected(OnSkinItemSelected);
        scrollPositionController.OnUpdatePosition(p => UpdatePosition(p));
    }


 
    public void SetOnCenterEvent(Action<SkinConfigDefine> centerEvent)
    {
        this.OnCenterEvent = null;
        this.OnCenterEvent += centerEvent;
    }

    public void LoadSkin()
    {
        if (!m_isInit)
        {
            m_isInit = true;
            InitSkin();
        }
        var config = SkinDataMgr.instance.GetHeroSkin();
        if (config != null)
        {
            cellData.Clear();
            var data = new List<SkinConfigDefine>();
            for (int i = 0; i < config.Count; i++)
            {
                var skin = config[i];
                if (skin != null)
                {
                    data.Add(skin);
                }
            }
            cellData = data;
            scrollPositionController.SetDataCount(cellData.Count);
            UpdateContents();
            scrollPositionController.ScrollTo(0, 0.5f);
        }
    }

    private void OnSkinItemSelected(int index)
    {
        Debug.Log("当前Index : " + index);
        OnCenter(index);
    }

    public void DeSpawn()
    {
        DestoryCellGroups();
        cellData.Clear();
    }
}
