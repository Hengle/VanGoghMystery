using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Events;

public class UICycle : MonoBehaviour
{
    [SerializeField] Image m_Image;
    [SerializeField] List<Sprite> m_Sprites;

    [SerializeField] bool m_CycleOnStart = true;
    [SerializeField] bool m_Randomize = false;
    [SerializeField] float m_DisplayTime = 2f;
    [SerializeField] float m_TransitionTime = 1f;

    private int m_CurrentIndex = -1;

    private void Start()
    {
        if (m_CycleOnStart)
        {
            CycleImages();
        }
    }

    public void CycleImages()
    {
        if (m_Sprites == null)
        {
            Debug.LogError("m_Sprites == null, there's no sprites in list to cycle over");
            return;
        }

        if (m_Sprites.Count <= 1)
        {
            Debug.LogError("You need atleast a minimum of two sprites in the m_Sprites list, please ensure you have more than 1 sprite in list");
            return;
        }

        StartCoroutine(Delay(m_DisplayTime, () =>
        {
            Sprite nextSprite = null;

            if(!m_Randomize)
            {
                m_CurrentIndex++;

                if(m_CurrentIndex == m_Sprites.Count)
                {
                    m_CurrentIndex = 0;
                }

                nextSprite = m_Sprites[m_CurrentIndex];
            }

            if(m_Randomize)
            {
                while(true)
                {
                    int randomIndex = Random.Range(0, m_Sprites.Count);

                    if(m_CurrentIndex != randomIndex)
                    {
                        m_CurrentIndex = randomIndex;
                        nextSprite = m_Sprites[m_CurrentIndex];
                        break;
                    }
                }
            }

            UIFader.TransitionTo(m_Image, nextSprite, () =>
            {
                Debug.Log("Image transitioned");
                CycleImages();
            });
        }));
    }

    private System.Collections.IEnumerator Delay(float waitTime, UnityAction onComplete)
    {
        yield return new WaitForSeconds(waitTime);

        if (onComplete != null)
            onComplete();
    }
}
