using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextLanguage : MonoBehaviour {
    [TextArea(3, 10)]
    public string FrText;
    [TextArea(3, 10)]
    public string EnText;

    private Text text;
    // Use this for initialization
    private void Awake()
    {
        text = this.GetComponent<Text>();
    }

    private void Start()
    {
        updateText(GlobalControl.Instance.getLanguage());
    }

    public void updateText(Language l)
    {
        if (l == Language.fr)
        {
            text.text = FrText;
        }
        else if (l == Language.en)
        {
            text.text = EnText;
        }
    }
}
