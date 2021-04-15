using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
 
public class UIFaderManager : MonoBehaviour
{
    public static UIFaderManager Instance { get; private set; }

    private List<FadeItem> m_FadeItems = new List<FadeItem>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this);
    }

    public void Transition(Image image, Sprite to, UnityAction onComplete, float fadeDuration)
    { 
        GameObject cloneObj = Instantiate(image.gameObject, image.gameObject.transform.parent, false);

        Image cloneIMG = cloneObj.GetComponent<Image>();
        cloneIMG.sprite = to;
        cloneIMG.color = Color.clear;
        cloneIMG.transform.SetSiblingIndex(image.gameObject.transform.GetSiblingIndex() + 1);
        
        Fade(image, Color.clear, null, fadeDuration);
        Fade(cloneIMG, Color.white, () =>
        {
            image.sprite = to;
            image.color = Color.white;
            Destroy(cloneIMG.gameObject);

            onComplete?.Invoke();
        },
        fadeDuration);
    }

    public void Fade(Image image, Color32 to, UnityAction onComplete, float fadeDuration)
    {
        // check if object is already fading
        if (!IsItemCurrentlyFading(image))
        {
            int instanceId = image.gameObject.GetInstanceID();

            m_FadeItems.Add(new FadeItem(image.color, to, image, () =>
            {
                m_FadeItems.RemoveAll(x => x.Image.gameObject.GetInstanceID() == instanceId);

                onComplete?.Invoke();
            },
            fadeDuration));
        }
    }

    private bool IsItemCurrentlyFading(Image image)
    {
        bool foundItem = false;
        int newImageId = image.GetInstanceID();

        foreach(FadeItem fadeItem in m_FadeItems)
        {
            if(fadeItem.Image.GetInstanceID() == newImageId)
            {
                Debug.Log("Item is already fading");
                return true;
            }
        }

        return false;
    }

    private void Update()
    {
        if(m_FadeItems != null)
        {
            for (int i = 0; i < m_FadeItems.Count; i++)
            {
                m_FadeItems[i].Update(Time.deltaTime);
            }
        }
    }
}

public class FadeItem
{
    public enum State
    {
        Fade,
        Sleep
    }

    public Image Image { get; }

    private Color32 m_FromColor;
    private Color32 m_TargetColor;
    private float m_Time = 0.0f;
    private float m_FadeDuration = 2f;
    private State m_CurrentState = State.Sleep;
    private UnityAction m_OnCompleteCallback;

    public FadeItem(Color32 from, Color32 to, Image image, UnityAction onComplete, float fadeDuration = 2)
    {
        m_FromColor = from;
        m_TargetColor = to;
        Image = image;
        m_OnCompleteCallback = onComplete;
        m_CurrentState = State.Fade;
        m_Time = 0.0f;
        m_FadeDuration = fadeDuration;
    }

    public void Update(float deltaTime)
    {
        if(m_CurrentState  == State.Fade)
        {
            m_Time += deltaTime / m_FadeDuration;

            Image.color = Color32.Lerp(m_FromColor, m_TargetColor, m_Time);

            if(m_Time >= 1f)
            {
                m_CurrentState = State.Sleep;

                m_OnCompleteCallback?.Invoke();
            }
        }
    }
}

public static class UIFader
{

    /// <summary>
    /// Generic Fade from image 'current' color to fadeTo color
    /// </summary>
    /// <param name="image">UI Image that will fade</param>
    /// <param name="fadeTo">Fade to color</param>
    /// <param name="onComplete"></param>
    public static void Fade(Image image, Color32 fadeTo,  UnityAction onComplete, float fadeDuration = 2)
    {
        UIFaderManager.Instance.Fade(image, fadeTo, onComplete, fadeDuration);
    }

    /// <summary>
    /// Fades image color from Color.clear to Color.White
    /// </summary>
    /// <param name="image"></param>
    /// <param name="onComplete"></param>
    public static void FadeIn(Image image, UnityAction onComplete, float fadeDuration = 2)
    {
        UIFaderManager.Instance.Fade(image, Color.white, onComplete, fadeDuration);
    }

    /// <summary>
    /// Fades image color from Color.clear to Color.white
    /// </summary>
    /// <param name="image"></param>
    /// <param name="onComplete"></param>
    public static void FadeOut(Image image, UnityAction onComplete, float fadeDuration = 2)
    {
        UIFaderManager.Instance.Fade(image, Color.clear, onComplete, fadeDuration);
    }

    public static void TransitionTo(Image image, Sprite spriteIMGTarget, UnityAction onComplete, float fadeDuration = 2)
    {
        UIFaderManager.Instance.Transition(image, spriteIMGTarget, onComplete, fadeDuration);
    }
}
