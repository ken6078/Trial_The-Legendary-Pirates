using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextShow : MonoBehaviour
{
    //劇本
    [SerializeField] TextAsset txt;
    [SerializeField] TextMeshProUGUI NameTest;
    [SerializeField] TextMeshProUGUI CharTest;
    //UI
    [SerializeField] GameObject reporter;
    [SerializeField] GameObject judge;
    [SerializeField] GameObject amber;
    [SerializeField] GameObject jonny;

    [SerializeField] List<string> testList = new List<string>();
    [SerializeField] float typingSpeed = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        testList = new List<string>(txt.text.Split('\n'));
        StartCoroutine(Type());
    }

    IEnumerator Type() {
        for (int index = 0; index < testList.Count; index++){
            //這一行是選項
            if (testList[index][1] == '-'){
                
            }
            else{
                string name = testList[index].Substring(0, testList[index].IndexOf(":"));
                string chate = testList[index].Substring(testList[index].IndexOf(":")+1);
                NameTest.text = name;
                reporter.SetActive(false);
                judge.SetActive(false);
                amber.SetActive(false);
                jonny.SetActive(false);
                if (name == "記者")
                    reporter.SetActive(true);
                else if (name == "法官")
                    judge.SetActive(true);
                else if (name == "安柏赫德")
                    amber.SetActive(true);
                else
                    jonny.SetActive(true);
                CharTest.text = "";
                foreach (char letter in chate.ToCharArray()) {
                    CharTest.text += letter;
                    yield return new WaitForSeconds(typingSpeed);
                }
                while(! Input.GetKeyDown(KeyCode.Space))
                    yield return null;
            }
        } 
    }
}
