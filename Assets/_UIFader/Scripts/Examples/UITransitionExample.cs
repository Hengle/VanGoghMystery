using UnityEngine;
using UnityEngine.UI;

public class UITransitionExample : MonoBehaviour
{
    [SerializeField] Image m_Image;
    [SerializeField] Sprite m_SpriteToTransitionTo;
    [SerializeField] float m_FadeDuration = 2f;

    private void Start()
    {
        UIFader.TransitionTo(m_Image, m_SpriteToTransitionTo, () =>
        {

            Debug.Log("Transition complete");

        }, m_FadeDuration);
    }
}
