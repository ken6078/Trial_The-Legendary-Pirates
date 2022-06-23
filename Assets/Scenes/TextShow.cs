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
    [SerializeField] GameObject bar; // 陪審團信心bar，需要換圖表示狀態

    public Texture2D mtrl_bar_30; //我看文章做到一半，換圖的部分沒有很理解
    // https://home.gamer.com.tw/creationDetail.php?sn=4877145
    public Texture2D mtrl_bar;

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
        int count = 0; //題目計數器(從0開始)
        ArrayList ans_dict = new ArrayList(); //回答紀錄
        int score = 40; //評分%數，假設一開始戰況4:6
        bar.SetActive(false); //我不想讓bar一直出現&消失

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
                bar.SetActive(true); //這裡可以考慮bar進場時機是哪裡
                string name = testList[index].Substring(3, testList[index].IndexOf(":") - 3);
                string positiveAns = "1." + testList[index].Substring(testList[index].IndexOf(":")+1, testList[index].IndexOf("|") - testList[index].IndexOf(":")-1);
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
                foreach (char letter in positiveAns.ToCharArray()) {
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
                    (!Input.GetKey(KeyCode.LeftArrow)) && (!Input.GetKey(KeyCode.RightArrow)) &&
                    (!Input.GetKey(KeyCode.Keypad1)) && (!Input.GetKey(KeyCode.Keypad2)) &&
                    (!Input.GetKey(KeyCode.Alpha1)) && (!Input.GetKey(KeyCode.Alpha2))
                ){
                    yield return null;
                }
                // 判斷使用者輸入的選項
                if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.Keypad1) || Input.GetKey(KeyCode.Alpha1)){
                    Debug.Log("number 1");
                    ans_dict.Add("number 1"); //記錄下回答

                    if(count%2==0){ //判斷是誰在說話
                        score += 10;
                        Debug.Log("Johnny's round");}
                    else{
                        score -= 10;
                        Debug.Log("Amber's round");}         
                    Debug.Log("score:"+score);
                    // bar.GetComponent<RawImage>().texture = mtrl_bar_30; //這裡是壞的，texture無法設定成功
                    }

                if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.Keypad2) || Input.GetKey(KeyCode.Alpha2)){
                    Debug.Log("number 2");
                    ans_dict.Add("number 2"); //記錄下回答
                    if(count%2==0){ //判斷是誰在說話
                        score -= 10;
                        Debug.Log("Johnny's round");}
                    else{
                        score += 10;
                        Debug.Log("Amber's round");}
                    Debug.Log("score:"+score);
                    // bar.GetComponent<RawImage>().texture = mtrl_bar; //這裡是壞的，texture無法設定成功
                    }

                Debug.Log("cur ans:" + ans_dict[count]); //目前的答題記錄
                Debug.Log("cur count:" + count); //第幾題了
                count++;
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

                // 一個字一個字跳出來的效果s
                foreach (char letter in chate.ToCharArray()) {
                    CharTest.text += letter;
                    yield return new WaitForSeconds(typingSpeed);
                }
                
                //等待使用者按下空白
                while(!Input.GetKeyDown(KeyCode.Space)){
                    yield return null;
                }
            }            
        } 
    }
}
