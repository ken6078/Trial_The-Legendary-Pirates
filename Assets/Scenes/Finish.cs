using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class Finish : MonoBehaviour
{
    //UI
    [SerializeField] GameObject WinBG;
    [SerializeField] GameObject LoseBG;
    // Start is called before the first frame update
    void Start()
    {
        if (Save.score > 50){
            WinBG.SetActive(true);
            LoseBG.SetActive(false);
        }else{
            LoseBG.SetActive(true);
            WinBG.SetActive(false);
        }
    }

    public void jumpScene(int id){
        SceneManager.LoadScene(id);
        Save.ans_dict = new List<int>();//回答紀錄
        Save.score = 50;
    }
}
