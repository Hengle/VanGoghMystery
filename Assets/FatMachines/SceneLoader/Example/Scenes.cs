using UnityEngine;
using FM;

public class Scenes : MonoBehaviour {

    AsyncOperation async;

	public void ChangeSceneAsync(string sceneName){
        async = SceneLoader.instance.LoadSceneAsync(sceneName);
    }

    public void ChangeScene(string sceneName){
        SceneLoader.instance.LoadScene(sceneName);
    }

    public void ChangeSceneWithIndex(int index){
        SceneLoader.instance.LoadScene(index);
    }

    public void ChangeSceneAsynWithIndex(int index){
        async = SceneLoader.instance.LoadSceneAsync(index);
    }

    void Start(){
        Debug.Log(SceneLoader.GetActiveScene());
    }

    void Update(){
        if(async != null){
            Debug.Log(async.progress);
        }
    }

}
