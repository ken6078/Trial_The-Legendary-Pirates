using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextShow : MonoBehaviour
{
    //劇本
    [SerializeField] TextAsset txt;
    //人名跟對話內容
    [SerializeField] TextMeshProUGUI NameTest;
    [SerializeField] TextMeshProUGUI CharTest;
    //UI
    [SerializeField] GameObject reporter;
    [SerializeField] GameObject judge;
    [SerializeField] GameObject amber;
    [SerializeField] GameObject jonny;
    //要顯示的字&跳出來的速度
    [SerializeField] List<string> testList = new List<string>();
    [SerializeField] float typingSpeed = 0.05f;
    // Start is called before the first frame update
    void Start()
    {
        testList = new List<string>(txt.text.Split('\n'));
        StartCoroutine(Type());
    }

    IEnumerator Type() {
        //讀劇本
        for (int index = 0; index < testList.Count; index++){
            CharTest.text = "";
            reporter.SetActive(false);
            judge.SetActive(false);
            amber.SetActive(false);
            jonny.SetActive(false);
            //讀到#就判定為註解直接跳過
            if (testList[index][0] == '#')
                continue;
            //這一行是選項
            if (testList[index][1] == '-'){
                string name = testList[index].Substring(3, testList[index].IndexOf(":") - 3);
                string posstiveAns = "1." + testList[index].Substring(testList[index].IndexOf(":")+1, testList[index].IndexOf("|") - testList[index].IndexOf(":")-1);
                string negitiveAns = "2." + testList[index].Substring(testList[index].IndexOf("|")+1);
                NameTest.text = name;
                // 根據名稱顯示頭像
                if (name == "記者")
                    reporter.SetActive(true);
                else if (name == "法官")
                    judge.SetActive(true);
                else if (name == "安柏赫德")
                    amber.SetActive(true);
                else
                    jonny.SetActive(true);
                // 一個字一個字跳出來的效果
                foreach (char letter in posstiveAns.ToCharArray()) {
                    CharTest.text += letter;
                    yield return new WaitForSeconds(typingSpeed);
                }
                CharTest.text += '\n';
                // 一個字一個字跳出來的效果
                foreach (char letter in negitiveAns.ToCharArray()) {
                    CharTest.text += letter;
                    yield return new WaitForSeconds(typingSpeed);
                }
                // 等使用者輸入 數字鍵 上下 數字鍵盤得數字 其中一個
                while( 
                    (!Input.GetKey(KeyCode.UpArrow)) && (!Input.GetKey(KeyCode.DownArrow)) &&
                    (!Input.GetKey(KeyCode.Keypad1)) && (!Input.GetKey(KeyCode.Keypad2)) &&
                    (!Input.GetKey(KeyCode.Alpha1)) && (!Input.GetKey(KeyCode.Alpha2))
                ){
                    yield return null;
                }
                // 判斷使用者輸入的選項
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
                // 根據名稱顯示頭像
                if (name == "記者")
                    reporter.SetActive(true);
                else if (name == "法官")
                    judge.SetActive(true);
                else if (name == "安柏赫德")
                    amber.SetActive(true);
                else
                    jonny.SetActive(true);
                // 一個字一個字跳出來的效果
                foreach (char letter in chate.ToCharArray()) {
                    CharTest.text += letter;
                    yield return new WaitForSeconds(typingSpeed);
                }
                //等待使用者按下空白
                while(! Input.GetKeyDown(KeyCode.Space)){
                    yield return null;
                }
            }
            
        } 
    }
}
