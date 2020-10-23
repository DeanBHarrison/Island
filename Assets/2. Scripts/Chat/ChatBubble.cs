using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Security.Cryptography;

public class ChatBubble : MonoBehaviour
{

    private SpriteRenderer background;
    public TextMeshPro textMeshPro;
    // Start is called before the first frame update
    private void Awake()
    {
        background = transform.Find("Background").GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Debug.Log("textsize :" + textMeshPro.GetRenderedValues(true));   
    }

    private void Start()
    {
        Setup("howdy");
    }

    public static void Create(Transform parent, Vector3 localPosition, string text)
    {
        Transform chatBubbleTransform = Instantiate(GameAssets.i.chatBubble, parent);
      chatBubbleTransform.transform.localPosition = localPosition;

        chatBubbleTransform.GetComponent<ChatBubble>().Setup(text);

        Destroy(chatBubbleTransform.gameObject, 4f);
    }
    public  void Setup(string text)
    {
        // set the text to be whatever we pass in
        textMeshPro.SetText(text);
        // force mesh update to make sure the text is rendered before trying to find what size it is
        textMeshPro.ForceMeshUpdate();
        // returns a vector2 of the size of the text
        Vector2 textSize = textMeshPro.GetRenderedValues(false);
        Vector2 padding = new Vector2(0.5f, 0.5f);
        background.size = textSize + padding;

        // start the coroutine that spells the words out 1 by 1
        StopAllCoroutines();
        StartCoroutine(TypeSentence(text));
    }

    IEnumerator TypeSentence(string sentence)
    {
        //to begin with the text needs to be empty
        textMeshPro.text = "";

        // looping through each char in sentence, to chararray converts a string into an array of chars
        foreach (char letter in sentence.ToCharArray())
        {
            textMeshPro.text += letter;
            yield return new WaitForSeconds(0.05f);
        }

    }
}
