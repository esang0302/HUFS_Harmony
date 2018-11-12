using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class LoginScript : MonoBehaviour {
    public InputField ID;
    public InputField PW;

	// Use this for initialization
	void Start () {
        //StartCoroutine();
    }

    public void Login()
    {
        string url = "http://www.ServerToAndroid-env.b3ivtwm3mt.ap-northeast-2.elasticbeanstalk.com";

        Dictionary<string,string> post = new Dictionary<string, string>();
        post.Add(ID.text, PW.text);
        POST(url, post);
    }

    public WWW POST(string url, Dictionary<string, string> post)
    {
        WWWForm form = new WWWForm();
        foreach (KeyValuePair<string, string> post_arg in post) 
            { 
                form.AddField(post_arg.Key, post_arg.Value); 
            } 
    
            WWW www = new WWW(url, form); 
            StartCoroutine(WaitForRequest(www)); 
            return www; 
    }
    private IEnumerator WaitForRequest(WWW www)
    {
        yield return www;
        if(www.error == null)
        {
            Debug.Log("WWW OK! : " + www.text);
            if(www.text == "OK")
            {
                //맞으면 씬넘어가게 , 악기 구매정보도 같이 넘겨야됨
                SceneManager.LoadScene("3Playing");
            }
            else
            {
                //아이디 비번 틀릴때
                Debug.Log(www.text);
            }
        }
        else
        {
            Debug.Log("WWW Error! : " + www.error);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}