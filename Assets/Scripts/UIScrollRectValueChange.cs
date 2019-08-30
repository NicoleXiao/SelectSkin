using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScrollRectValueChange : MonoBehaviour
{
    public Vector2 m_contentPos = Vector2.zero;
    public GameObject m_preArrow;
    public GameObject m_nextArrow;
    private ScrollRect m_rect;
    private RectTransform m_content;
    private float m_leftPos;
    private float m_rightPos;
    private float m_offset = 2f;
    protected ScrollRect scrollRect
    {
        get
        {
            if (m_rect == null)
            {
                m_rect = this.GetComponent<ScrollRect>();
            }
            return m_rect;
        }
    }

    public RectTransform content
    {
        get
        {
            if (m_content == null)
            {
                m_content = scrollRect.content;
            }
            return m_content;
        }
    }

    public void InitScrollRectChangePos()
    {
        content.anchoredPosition = m_contentPos;
        if (scrollRect.horizontal)
        {
            var width = scrollRect.GetComponent<RectTransform>().sizeDelta.x;
            m_leftPos = content.anchoredPosition.x;
            m_rightPos = width - content.sizeDelta.x;
            if (content.sizeDelta.x <= width)
            {
                HideAllArrow();
            }
            else
            {
                ShowNextArrow();
            }
        }
        else if (scrollRect.vertical)
        {
            var height = scrollRect.GetComponent<RectTransform>().sizeDelta.y;
            m_leftPos = content.anchoredPosition.y;
            m_rightPos = height - content.sizeDelta.y;
            if (content.sizeDelta.y <= height)
            {
                HideAllArrow();
            }
            else
            {
                ShowNextArrow();
            }
        }

    }

    public void OnScrollRectValueChanged(Vector2 pos)
    {
        if (scrollRect.horizontal)
        {
            var x = content.anchoredPosition.x;
            if (x < m_leftPos  && x > (m_rightPos + m_offset))
            {
                ShowAllArrow();
            }
            else if (x <= (m_rightPos + m_offset))
            {
                ShowPreArrow();
            }
            else if (x >= m_leftPos)
            {
                ShowNextArrow();
            }
        }
        else if (scrollRect.vertical)
        {
            var y = content.anchoredPosition.x;
            if (y > m_leftPos  && y < (m_rightPos - m_offset))
            {
                ShowAllArrow();
            }
            else if (y <= (m_rightPos - m_offset))
            {
                ShowPreArrow();
            }
            else if (y >= m_leftPos)
            {
                ShowNextArrow();
            }
        }

    }

    private void HideAllArrow()
    {
        m_preArrow.SetActive(false);
        m_nextArrow.SetActive(false);
    }

    private void ShowPreArrow()
    {
        m_preArrow.SetActive(true);
        m_nextArrow.SetActive(false);
    }

    private void ShowNextArrow()
    {
        m_preArrow.SetActive(false);
        m_nextArrow.SetActive(true);
    }

    private void ShowAllArrow()
    {
        m_preArrow.SetActive(true);
        m_nextArrow.SetActive(true);
    }

    public void AddValueChangeListener()
    {
        scrollRect.onValueChanged.AddListener(OnScrollRectValueChanged);
    }

    public void RemoveValueChangeListener()
    {
        scrollRect.onValueChanged.RemoveListener(OnScrollRectValueChanged);
    }


}
