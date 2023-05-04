using System.Collections;
using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InstantiateDialogue : MonoBehaviour
{

    bool firstCall = true;
    public GameObject dialogueMessage;
    public GameObject Player;
    //public Sprite Npc;

    public GameObject mainDialogueImage;
    public TextAsset ta;
    public List<GameObject> answerButtons = new List<GameObject>();

    public string spriteLink;
    public Image npcImage;

    public string audioLink;
    public AudioSource mainAudio;
    public string savedAudio; //Чтобы при отсутствии смены музыки она не начиналась снова.

    public TMP_Text npcText;
    public TMP_Text npcName;


    [SerializeField]
    public int currentNode = 0;
    int Number = -1;
    Node[] nd;
    Dialogue dialogue;

    void Start()
    {
        mainDialogueImage.SetActive(false);

        Dialogue dialogue = Dialogue.Load(ta);
        nd = dialogue.nodes;

        npcText.text = nd[0].Npctext;
        npcName.text = dialogue.npcname;

        var sprite = Resources.Load<Sprite>("Sprites/Sprite1");
        npcImage.sprite = sprite;

        var audioLink = Resources.Load<AudioClip>("Audio/Audio1");
        savedAudio = "Audio/Audio1";
        mainAudio.clip = audioLink;
        mainAudio.Play();
        
        //AnswerClicked(14);  //14 - для присвоения начальных значений в диалоге, чтобы не создавать новую функцию
    }

    private void universalListener(int idButton) //Универсальный Листенер для каждой кнопки. Отвечает за вариант ответа(0,1,2 и т.п.)
    {
        Debug.Log(idButton);
        AnswerClicked(idButton);
    }

    public void AnswerClicked(int numberOfButton)
    {
        mainDialogueImage.SetActive(true);
        Player.GetComponent<PlayerMovement>().enabled = false; //Запрещаем игроку двигаться

        if (numberOfButton == 14)
        {
            currentNode = 0;
        }
        else
        {
            foreach (Button child in mainDialogueImage.GetComponentsInChildren<Button>())//Удаляем все созданные ранее кнопки, чтобы при изменении количества ответов все норм менялось
            {
                int index = Convert.ToInt32(child.name.Substring(12)); //Получаем индекс кнопки из названия
                Debug.Log("Until destroy index: "+ index.ToString());
                Debug.Log("Until destroy number: " + Number.ToString());
                Destroy(mainDialogueImage.transform.GetChild(index).gameObject);//(index).gameObject); //Уничтожаем ее
                answerButtons.Clear(); //Убираем из списка кнопок 
                Number--; //чтобы для новой кнопки был зарезервирован нужный индекс в названии
            }

            if (nd[currentNode].answers[numberOfButton].end == "true")
            {
                mainDialogueImage.SetActive(false); //конец диалога. т.к. все элементы(кнопки ответов, текст-элементы с нпс) привязаны к этому изображению, все пропадает с экрана, типа все закончено.
                Player.GetComponent<PlayerMovement>().enabled = true;
                //Destroy(Npc);
               // Npc.SetActive(false);
            }

            currentNode = nd[currentNode].answers[numberOfButton].nextNode;
        }
        npcText.text = nd[currentNode].Npctext;

        spriteLink = nd[currentNode].Npcspritelink;
        string link = "Sprites/" + spriteLink;
        var sprite = Resources.Load<Sprite>(link);
        npcImage.sprite = sprite;

        audioLink = nd[currentNode].Audio;
        string link2 = "Audio/" + audioLink;
        if (!link2.Equals(savedAudio))
        {
            savedAudio = link2;
            var audio = Resources.Load<AudioClip>(link2);
            mainAudio.clip = audio;
            mainAudio.Play();
        }

        for (int i = 0; i < nd[currentNode].answers.Length; i++)
        {
            if (!answerButtons.Exists(x => x.name == "AnswerButton" + i)) //если на сцене мало кнопок
            { //Сейчас кнопки в цикле создаются горизонтально. Настраивай в зависимости от интерфейса.
                Debug.Log("Until create button number: " + Number.ToString()+" i is: "+ i.ToString());
                CreateButton(130+360*i, (float)3, -240, (float)0.3); //x, width, y, hеight
            }

            answerButtons[i].GetComponentInChildren<Text>().text = nd[currentNode].answers[i].text;
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && firstCall && dialogueMessage.activeSelf) //Первый вызов диалога, потом можно запилить условия с последующими
        {
            Debug.Log("init dial");
            dialogueMessage.SetActive(false);
            firstCall = false;
            AnswerClicked(14);
        }

        if (nd[currentNode].answers.Length == 1 & Input.GetKeyDown(KeyCode.Space)) //Одна кнопка "Продолжить". По нажатию пробела скипается
        {
            Debug.Log("update");
            AnswerClicked(0);
        }
    }

    public void CreateButton(float x1, float y1, float x2, float y2)
    {
        Number++;//увеличение индекса в названии будущей кнопки

        GameObject newButton = new GameObject("AnswerButton" + Number, typeof(Image), typeof(Button), typeof(LayoutElement));
        newButton.transform.SetParent(mainDialogueImage.transform);

        newButton.GetComponent<Image>().color = new Color(0, 2, 0, 1);
        int x = Number;
        newButton.GetComponent<Button>().onClick.AddListener(delegate { universalListener(x); });
        newButton.transform.SetSiblingIndex(Number);
        RectTransform rect = newButton.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(0, 1);
        rect.anchorMax = new Vector2(0, 1);
        rect.anchoredPosition = new Vector2(x1, x2);
        rect.sizeDelta = new Vector2(y1, y2);



        GameObject newText = new GameObject("AnswerText" + Number, typeof(Text),typeof(LayoutElement));
        newText.transform.SetParent(newButton.transform);
        newText.layer = 5;
        newText.GetComponent<Text>().text = Number.ToString();
        RectTransform rt = newText.GetComponent<RectTransform>();
        rt.anchorMin = new Vector2(0, 0);
        rt.anchorMax = new Vector2(1, 1);
        rt.anchoredPosition = new Vector2(0, 0);
        rt.sizeDelta = new Vector2(0, 0);
        //rt.anchoredPosition = new Vector2(x1, x2);
        //rt.sizeDelta = new Vector2(y1, y2);
        Font font = (Font)Resources.GetBuiltinResource(typeof(Font), "Arial.ttf");
        newText.GetComponent<Text>().font = font;
        newText.GetComponent<Text>().resizeTextForBestFit = false; //////////////
        //newText.GetComponent<Text>().fontSize = newText.GetComponent<Text>().cachedTextGenerator.fontSizeUsedForBestFit;
        newText.GetComponent<Text>().color = new Color(0,0,0);
        newText.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;

        answerButtons.Add(newButton); 
    }
}

