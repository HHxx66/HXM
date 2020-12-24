using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class communicating : MonoBehaviour
{
    // Start is called before the first frame update

    ScrollRect rect;
    GameObject text;
    GameObject panel;
    public GameObject other;
    public int role;

    string[] Lines1;
    string[] Lines2;
    int it;
    bool isSpeaking;
    float t;
    float dt;

    void Start()
    {
        panel = this.transform.GetChild(0).gameObject;
        rect = panel.transform.GetChild(0).GetComponent<ScrollRect>();
        rect.horizontalNormalizedPosition = 0;
        text = rect.transform.GetChild(0).gameObject;
        Lines1 = new string[4]{
            "第一天去上学啊！笫一天去上学啊！ 醒醒，醒醒！快点，第一天去上学唉!",
            "不是你，爸爸，是我去上学！",
            "起床！起床！该去学校了！该去学校了！该去学校了！真是太好了！",
            "喔，太好了！"};
        Lines2 = new string[3]{
            "我不想去上学，让我再睡五分钟吧",
            "好吧，嗯？",
            "好的、好的，起来了"};
        if(role == 1){
            panel.SetActive(true);
            it = 0;
            text.GetComponent<Text>().text = "      " + Lines1[it];
            isSpeaking = true;
        }
        else{
            it = -1;
            isSpeaking = false;
        }
        dt = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(isSpeaking){
            string[] temp;
            if(role == 1) temp = Lines1;
            else temp = Lines2;
            if(temp[it].Length * 15 < 180){
                t = 3.0f;
            }
            else{
                t = 3.0f + Mathf.Log((temp[it].Length * 15.0f - 180.0f) / 10 + 1);
            }
            if(dt > t)
            {
                isSpeaking = false;
                panel.SetActive(false);
                rect.horizontalNormalizedPosition = 0;
                dt = 0;
                other.GetComponent<communicating>().callback();
            }
            else{
                rect.horizontalNormalizedPosition = rect.horizontalNormalizedPosition + Mathf.Pow(2,(t - 3.0f)) * ((t - 3.0f) / 250) * Time.deltaTime;
                dt = dt + Time.deltaTime;
            }
        }
    }

    public void callback()
    {
        string[] temp;
        if(role == 1) temp = Lines1;
        else temp = Lines2;
        if(it + 1 < temp.Length){
            it++;
            panel.SetActive(true);
            text.GetComponent<Text>().text = "      " + temp[it];
            isSpeaking = true;
        }
    }

}
