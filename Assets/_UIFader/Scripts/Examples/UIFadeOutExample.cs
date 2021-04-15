using UnityEngine;
using UnityEngine.UI;

public class UIFadeOutExample : MonoBehaviour
{
    [SerializeField] Image m_Image;
    [SerializeField] float m_FadeDuration = 2;
    void Start()
    {
        UIFader.FadeOut(m_Image, () =>
        {
            Debug.Log("Fading in completed!");
        }, m_FadeDuration);
    }
}
