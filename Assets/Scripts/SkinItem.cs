using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FancyScrollView;


namespace DP.UI
{
    public class SkinItem : FancyScrollViewCell<SkinConfigDefine>
    {
        public float m_scale = 1f;
        public Text m_skinName;
        public Image m_skinItemHead;
        public Button m_lockBtn;
        public GameObject m_mask;
        public GameObject m_selectFlag;
        private float m_moveWidth;
        private float minScale;
        private float maxScale;
        private RectTransform m_rect;
        private SkinConfigDefine m_skinConfig;
        private bool m_isOwnSkin = false;
        static readonly int scrollTriggerHash = Animator.StringToHash("SkinAnimation");
        float currentPosition = 0;
        private void Awake()
        {
            m_rect = transform as RectTransform;
        }

        void OnEnable()
        {
            UpdatePosition(currentPosition);
        }

        public override void SetMoveWidth(float width)
        {
            m_moveWidth = width;
        }

        public override void SetContent(SkinConfigDefine itemData)
        {
            m_skinConfig = itemData;
            if (m_skinConfig != null)
            {
                m_skinName.text = m_skinConfig.Name;
                LoadSkin(itemData.Icon);
            }
        }

        /// <summary>
        /// 计算Item的位置和对应的缩放。
        /// scale 变化是个指数函数 位置是直线方程
        /// </summary>
        /// <param name="position"></param>
        public override void UpdatePosition(float position)
        {
            minScale = 0.8f;
            maxScale = 1.2f;
            float x = 0;
            float scale = 0;
            float center = m_moveWidth / 2;
            if (position == 0.5f)
            {
                x = center;
                scale = maxScale;
            }
            else
            {
                x = (m_rect.sizeDelta.x + m_moveWidth) * position - m_rect.sizeDelta.x / 2;
            }

            if (position < 0.5f)
            {
                float coefficient = 2 * Mathf.Log(maxScale, minScale) - 2;
                scale = Mathf.Pow(minScale, coefficient * position + 1);
            }
            else if (position > 0.5f)
            {
                float coefficient = 2 - 2 * Mathf.Log(maxScale, minScale);
                scale = Mathf.Pow(minScale, coefficient * position + 2 * Mathf.Log(maxScale, minScale) - 1);
            }
            m_rect.anchoredPosition = new Vector2(x, m_rect.anchoredPosition.y);
            m_rect.localScale = new Vector2(scale, scale);
            if (Mathf.Abs(position - 0.5f) < 0.08f)
            {
                transform.SetAsLastSibling();
            }
        }


        public void SetSkinState(bool isOwn)
        {
            m_isOwnSkin = isOwn;
            m_lockBtn.gameObject.SetActive(!isOwn);
        }

        protected override void OnItemCenter()
        {
            m_mask.SetActive(false);
            m_selectFlag.SetActive(true);
            m_lockBtn.gameObject.SetActive(false);
            this.transform.SetAsLastSibling();
        }

        public override void NotOnCenter()
        {
            m_mask.SetActive(true);
            m_selectFlag.SetActive(false);
            m_lockBtn.gameObject.SetActive(false);
        }


        public void LoadSkin(string iconPath)
        {
            string path = "skinIcon/" + iconPath;
            m_skinItemHead.sprite = Resources.Load<Sprite>(path);
            m_skinItemHead.enabled = true;
        }

        public override void DeSpawn()
        {
            m_skinItemHead.enabled = false;
            m_mask.SetActive(false);
            m_lockBtn.gameObject.SetActive(false);
        }

    }
}
