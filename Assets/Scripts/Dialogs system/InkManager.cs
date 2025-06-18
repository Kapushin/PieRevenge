using UnityEngine;
using UnityEngine.UI;
using System;
using Ink.Runtime;
using System.Collections;
using TMPro;

public class InkManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _canvas;

	[SerializeField]
	public Story Story;
    private TextAsset _newStory;

	[SerializeField]
	private TextMeshProUGUI _textField = null;
    [SerializeField]
    private VerticalLayoutGroup _choiceButtonContainer;
    [SerializeField]
    private Button _choiceButtonPrefab;

    public bool BlockInteractions = false;

    [SerializeField]
    public TextMeshProUGUI NameField = null;

    [SerializeField]
    private TextWriterEffect _textWriter;
    [SerializeField]
    public float SpeedWriter = .1f;

    [SerializeField] private AudioSource _writingSFX;

    private void Awake()
    {
        Invoke("DisableCanvas", .05f);
    }

    private void DisableCanvas()
    {
        _canvas.SetActive(false);
    }

    //���������� ����� �� NPC, ������ �������
    public void NewStory(TextAsset text)
    {
        _newStory = text;
        StartStory(_newStory);
    }

    // ������ �������, ����������� ������ �������
    private void StartStory(TextAsset text)
    {
        Story = new Story(text.text);
        DisplayNextLine();
        GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterControl>().CanMove = false;
    }

    // ����������� ��������� ������
    public void DisplayNextLine()
    {
        if (Story.canContinue)
        {
            string text = Story.Continue();
            // �������� ������ ��������, ���� ����
            text = text?.Trim();
            _textWriter.AddWriter(_textField, text, SpeedWriter);
        }

        if (Story.currentChoices.Count > 0)
        {
            _writingSFX.pitch = UnityEngine.Random.Range(.4f, 1f);
            _writingSFX.Play();
            Invoke("DisplayChoices", _textWriter._textToWrite.Length / 10f);
        }

        else
        {
            _textField.text = null;
            // ���� ����� �������, �� ������� ������
            _canvas.SetActive(false);
            // �������������� ����������� ��������� ������
            BlockInteractions = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterControl>().CanMove = true;
            // ��������� ������ ���� ����
            EventManager.SendDestroyNPC();
            SpeedWriter = .1f;
        }
    }

    // �������� ����� �������
    public void DisplayChoices()
    {
        _writingSFX.Stop();
        // ������� ���� ��������� ������
        for (int i = 0; i < Story.currentChoices.Count; i++)
        {
            Choice choice = Story.currentChoices[i];
            // �������� ������ � �������
            Button button = CreateChoiceButton(choice.text.Trim());
            // ������� ������, ��� ������, ���� �� ��� ������
            button.onClick.AddListener(() => OnClickChoiceButton(choice));
        }
    }

    // �������� ������, ������������ ������� ������
    Button CreateChoiceButton(string text)
    {
        //�������� ������ ������ �� �������
        Button choiceButton = Instantiate(_choiceButtonPrefab);
        choiceButton.transform.SetParent(_choiceButtonContainer.transform, false);

        //��������� ������ �� ������ �� �������
        TextMeshProUGUI buttonText = choiceButton.GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = text;

        return choiceButton;
    }

    // ����� �������� �� ������ ������, ������� ����, ��� ��������� �������
    private void OnClickChoiceButton(Choice choice)
    {
        Story.ChooseChoiceIndex(choice.index);
        // ������� �������� ������ � ������
        RefreshChoiceView();
        Story.Continue();
        DisplayNextLine();
    }

    // ������� �������� ������ � ������
    private void RefreshChoiceView()
    {
        if (_choiceButtonContainer != null)
        {
            foreach (Button button in _choiceButtonContainer.GetComponentsInChildren<Button>())
            {
                Destroy(button.gameObject);
            }
        }
    }
}
