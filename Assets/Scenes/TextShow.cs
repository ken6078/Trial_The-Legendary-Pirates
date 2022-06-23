using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextShow : MonoBehaviour
{
    Text mText;
    public string showStr = "今天天氣很好, \n我覺得我該寫一下程式 可是我不會寫程式";
    public float typingSpeed = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        mText = GetComponent<Text>();
        StartCoroutine(Type());
    }
    IEnumerator Type() {
       foreach (char letter in showStr.ToCharArray()) {
           mText.text += letter;
           yield return new WaitForSeconds(typingSpeed);
       }
    }
}
