using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SkinConfigDefine
{
    /// <summary> 
    /// 皮肤ID
    /// </summary>
    public int ID = 0;

    /// <summary> 
    /// 名称
    /// </summary>
    public string Name = "";

    /// <summary> 
    /// 皮肤名称
    /// </summary>
    public string Icon = "";
}
public class SkinDataMgr :Singleton<SkinDataMgr>
{
    private List<SkinConfigDefine> m_skinData = new List<SkinConfigDefine>();

    public override void Init()
    {
        CreateSkinData();
    }

    

    private void CreateSkinData()
    {
        for(int i = 1; i < 7; i++)
        {
            SkinConfigDefine cfg = new SkinConfigDefine();
            cfg.ID = i;
            cfg.Name = "皮肤" + i;
            cfg.Icon = "skin" + i;
            m_skinData.Add(cfg);
        }
    }

    public List<SkinConfigDefine> GetHeroSkin()
    {
        return m_skinData;
    }
}
