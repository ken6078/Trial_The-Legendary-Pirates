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
    [SerializeField] float typingSpeed = 0.05f;
    // Start is called before the first frame update
    void Start()
    {
        testList = new List<string>(txt.text.Split('\n'));
        StartCoroutine(Type());
    }

    IEnumerator Type() {
        for (int index = 0; index < testList.Count; index++){
            CharTest.text = "";
            reporter.SetActive(false);
            judge.SetActive(false);
            amber.SetActive(false);
            jonny.SetActive(false);
            if (testList[index][0] == '#')
                continue;
            //這一行是選項
            if (testList[index][1] == '-'){
                string name = testList[index].Substring(3, testList[index].IndexOf(":") - 3);
                string posstiveAns = "1." + testList[index].Substring(testList[index].IndexOf(":")+1, testList[index].IndexOf("|") - testList[index].IndexOf(":")-1);
                string negitiveAns = "2." + testList[index].Substring(testList[index].IndexOf("|")+1);
                NameTest.text = name;
                
                if (name == "記者")
                    reporter.SetActive(true);
                else if (name == "法官")
                    judge.SetActive(true);
                else if (name == "安柏赫德")
                    amber.SetActive(true);
                else
                    jonny.SetActive(true);

                foreach (char letter in posstiveAns.ToCharArray()) {
                    CharTest.text += letter;
                    yield return new WaitForSeconds(typingSpeed);
                }
                CharTest.text += '\n';
                foreach (char letter in negitiveAns.ToCharArray()) {
                    CharTest.text += letter;
                    yield return new WaitForSeconds(typingSpeed);
                }
                while( 
                    (!Input.GetKey(KeyCode.UpArrow)) && (!Input.GetKey(KeyCode.DownArrow)) &&
                    (!Input.GetKey(KeyCode.Keypad1)) && (!Input.GetKey(KeyCode.Keypad2)) &&
                    (!Input.GetKey(KeyCode.Alpha1)) && (!Input.GetKey(KeyCode.Alpha2))
                ){
                    yield return null;
                }
                if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Keypad1) || Input.GetKey(KeyCode.Alpha1))
                    Debug.Log("number 1");
                if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.Keypad2) || Input.GetKey(KeyCode.Alpha2))
                    Debug.Log("number 2");

                    
            }
            //這一行是台詞
            else{
                string name = testList[index].Substring(0, testList[index].IndexOf(":"));
                string chate = testList[index].Substring(testList[index].IndexOf(":")+1);
                NameTest.text = name;
                
                if (name == "記者")
                    reporter.SetActive(true);
                else if (name == "法官")
                    judge.SetActive(true);
                else if (name == "安柏赫德")
                    amber.SetActive(true);
                else
                    jonny.SetActive(true);

                foreach (char letter in chate.ToCharArray()) {
                    CharTest.text += letter;
                    yield return new WaitForSeconds(typingSpeed);
                }
                while(! Input.GetKeyDown(KeyCode.Space)){
                    yield return null;
                }
            }
            
        } 
    }
}
