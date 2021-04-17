using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class hint_script : MonoBehaviour
{
    // Start is called before the first frame update
    public bool show;
    TextMeshProUGUI text_messege;
    public GameObject text_obj;
    void Start()
    {
        show = true;
        text_messege = text_obj.GetComponent<TextMeshProUGUI>();
        if (text_messege == null) Debug.Log("fail");
    }

    // Update is called once per frame
    void Update()
    {

        if (show)
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    public void setMessege(string a) {

        text_messege.text = a;
        this.gameObject.SetActive(true);
    }
}
