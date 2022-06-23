using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextShow : MonoBehaviour
{
    // Text mText;
    // TextMesh mText;
    [SerializeField] TextMeshProUGUI mText;
    public List<string> testList = new List<string>();
    public float typingSpeed = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        // mText = GetComponent<Text>();
        // mText = gameObject.GetComponent<TextMesh>();
        StartCoroutine(Type());
        
    }

    IEnumerator Type() {
        for (int index = 0; index < testList.Count; index++){
            mText.text = "";
            foreach (char letter in testList[index].ToCharArray()) {
                mText.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }
            while(! Input.GetKeyDown (KeyCode.W))
                yield return null;

        }
       
    }
}
