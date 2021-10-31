using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextController : MonoBehaviour
{
    [SerializeField] private string[] messages;
    [SerializeField] private bool[] messagesEnd;

    [SerializeField] private TMP_Text text;
    [SerializeField] private TMP_Text startText_Text;
    [SerializeField] private GameObject blockInput;
    [SerializeField] private GameObject leftclick;
    

    [SerializeField] private Camera c;
    [SerializeField] private float maxZoomout;
    [SerializeField] private float speedZoom;
    [SerializeField] private float writeTime;

    private float startZoom;

    private int currentIndex = 0;
    private static bool startText = false;
    private static bool init = false;

    private bool waitForInput = false;
    void Start()
    {
        startZoom = c.fieldOfView;
        StartText();
    }

    void Update()
    {
        if (startText) if (init)
            {
                Init();
            }


        if (waitForInput)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!messagesEnd[currentIndex - 1])
                {
                    waitForInput = false;
                    Init();
                }
                else
                {
                    End();
                }
            }
        }
        
        if (startText && c.fieldOfView < maxZoomout)
        {
            c.fieldOfView += Time.deltaTime * speedZoom;
        }
        else if (!startText && c.fieldOfView > startZoom)
        {
            c.fieldOfView -= Time.deltaTime * speedZoom;
        }

    }

    public static void StartText()
    {
        print("start");
        startText = true;
        init = true;
    }

    private IEnumerator WriteText(string message)
    {
        for (int i = 0; i < message.Length; i++)
        {
            text.text += message[i];
            yield return new WaitForSeconds(writeTime);
        }
        leftclick.SetActive(true);
        waitForInput = true;
    }
    private void Init()
    {
        init = false;
        leftclick.SetActive(false);
        text.gameObject.SetActive(true);
        blockInput.SetActive(true);
        text.text = "";
        StartCoroutine(WriteText(messages[currentIndex]));
        currentIndex++;
    }

    private void End()
    {
        text.text = "";
        startText = false;
        blockInput.SetActive(false);
        leftclick.SetActive(false);
        text.gameObject.SetActive(false);
    }
} 
