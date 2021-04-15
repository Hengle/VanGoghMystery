using UnityEngine;
using UnityEngine.UI;

public class UIFadeInExample : MonoBehaviour
{
    [SerializeField] Image m_Image;
    [SerializeField] float m_FadeDuration = 2f;

    void Start()
    {
        UIFader.FadeIn(m_Image, () =>
        {
            Debug.Log("Fading in complete!");

        }, m_FadeDuration);    
    }

}
