using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using DG.Tweening;

namespace FM{
    public class SceneLoader : MonoBehaviour {

        public static SceneLoader instance;

        [Header("System")]
        [SerializeField] private GameObject canvasPrefab;
        [SerializeField] private GameObject loadingPanelPrefab;
        [Header("User")]
        [SerializeField] private float fadeDuration;
        [SerializeField] private float holdDuration;
        [SerializeField] Ease ease;

        private Transform canvas;
        private Image loadingPanel;

        private AsyncOperation asyncLoad;
        private bool animDone;

        void Awake(){
            instance = this;
        }

        void Start(){
            if(GameObject.FindObjectOfType<Canvas>() == null){
                canvas = Instantiate(canvasPrefab).transform;
            }else{
                canvas = GameObject.FindObjectOfType<Canvas>().transform;
            }

            if(loadingPanel == null){
                RectTransform go = Instantiate(loadingPanelPrefab).GetComponent<RectTransform>();
                go.SetParent(canvas);
                go.offsetMin = new Vector2(loadingPanelPrefab.GetComponent<RectTransform>().offsetMin.x, loadingPanelPrefab.GetComponent<RectTransform>().offsetMin.y);
                go.offsetMax = new Vector2(loadingPanelPrefab.GetComponent<RectTransform>().offsetMax.x, loadingPanelPrefab.GetComponent<RectTransform>().offsetMax.y);
                go.anchoredPosition = new Vector2(loadingPanelPrefab.GetComponent<RectTransform>().anchoredPosition.x, loadingPanelPrefab.GetComponent<RectTransform>().anchoredPosition.y);
                go.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Screen.width);
                go.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Screen.height);
                loadingPanel = go.GetComponent<Image>();
            }

            loadingPanel.gameObject.SetActive(true);
            loadingPanel.DOFade(0f, fadeDuration).SetEase(ease).OnComplete(() => loadingPanel.gameObject.SetActive(false));
            
        }

        public void LoadScene(int sceneIndex){
            ChangeScene("", sceneIndex);
        }

        public void LoadScene(string sceneName){
            ChangeScene(sceneName);
        }

        public AsyncOperation LoadSceneAsync(int sceneIndex){
            return ChangeSceneAsync("", sceneIndex);
        }

        public AsyncOperation LoadSceneAsync(string sceneName){
            return ChangeSceneAsync(sceneName);
        }

        void ChangeScene(string sceneName = "", int sceneIndex = -1){
            loadingPanel.gameObject.SetActive(true);
            loadingPanel.DOFade(1f, fadeDuration).SetEase(ease).OnComplete(() => {
                StartCoroutine(Delay(holdDuration, () => {
                    if(sceneName != ""){
                        SceneManager.LoadScene(sceneName);
                    }else if(sceneIndex != -1){
                        SceneManager.LoadScene(sceneIndex);
                    }
                }));
            });
        }

        AsyncOperation ChangeSceneAsync(string sceneName = "", int sceneIndex = -1){
            StartCoroutine(SceneAsync(sceneName, sceneIndex));
            loadingPanel.gameObject.SetActive(true);
            loadingPanel.DOFade(1f, fadeDuration).SetEase(ease).OnComplete(() => {
                StartCoroutine(Delay(holdDuration, () => {
                    animDone = true;
                    if (asyncLoad != null && asyncLoad.progress >= 0.9f){
                        asyncLoad.allowSceneActivation = true;
                    }
                }));
            });
            return asyncLoad;
        }

        IEnumerator SceneAsync(string sceneName = "", int sceneIndex = -1){
            if(sceneName != ""){
                asyncLoad = SceneManager.LoadSceneAsync(sceneName);
            }else if(sceneIndex != -1){
                asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);
            }
            if(asyncLoad == null){
                yield return null;
            }
            asyncLoad.allowSceneActivation = false;
            yield return asyncLoad;
        }

        void Update(){
            if(asyncLoad != null && animDone && asyncLoad.progress >= 0.9f){
                asyncLoad.allowSceneActivation = true;
            }
        }

        public static Scene GetActiveScene(){
            return SceneManager.GetActiveScene();
        }

        IEnumerator Delay(float delay, System.Action OnComplete){
            yield return new WaitForSeconds(delay);
            if(OnComplete != null) OnComplete();
        }
        
    }
}
