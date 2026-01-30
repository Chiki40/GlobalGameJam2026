using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

public delegate string TextIDToStringTranslationDelegate(string textID);
public delegate void OnGameDecisionOptionChosen(uint optionIndex);
public delegate void OnConversationEnded();

public class ConversationManager : MonoBehaviour
{
	private const char kOpenRichTextSectionChar = '<';
	private const char kCloseRichTextSectionChar = '>';

	private static ConversationManager _instance = null;
	public static ConversationManager Instance => _instance;

	[SerializeField]
	private float _defaultTextAppearTimeBetweenChars = 0.05f;
	[SerializeField]
	private float _defaultTextAppearTimeBetweenCharsFastModeMultiplier = 0.25f;
	[SerializeField]
	private UnityEvent<string> _onConversationTextChanged = null;
	[SerializeField]
	private UnityEvent _onConversationStarted = null;
	[SerializeField]
	private UnityEvent _onConversationEnded = null;

	private float _textAppearTimeBetweenChars = 0.0f;
	private float _textAppearTimeBetweenCharsFastModeMultiplier = 0.0f;

	private ConversationData _conversationTextsData = null;
	private int _conversationTextIndex = 0;
	private OnConversationEnded _onConversationEndedInternalCallback = null;

	private readonly StringBuilder _stringBuilder = new StringBuilder();
	private bool _waitingForUserProceed = false;
	private bool _isConversationInProgress = false;

	public bool IsConversationInProgress => _isConversationInProgress;

	private void Awake()
	{
		if (_instance == null)
		{
			_instance = this;
			DontDestroyOnLoad(gameObject);
			Init();
		}
		else
		{
			Destroy(gameObject);
		}
	}

	private void Init()
	{
		SetTextAppearTimeBetweenCharsToDefault();
		SetTextAppearTimeBetweenCharsFastModeMultiplierToDefault();
	}

	#region Public Methods
	public void SetTextAppearTimeBetweenChars(float textAppearTimeBetweenChars)
	{
		_textAppearTimeBetweenChars = textAppearTimeBetweenChars;
	}

	public void SetTextAppearTimeBetweenCharsToDefault()
	{
		_textAppearTimeBetweenChars = _defaultTextAppearTimeBetweenChars;
	}

	public void SetTextAppearTimeBetweenCharsFastModeMultiplier(float textAppearTimeBetweenCharsFastModeMultiplier)
	{
		_textAppearTimeBetweenCharsFastModeMultiplier = textAppearTimeBetweenCharsFastModeMultiplier;
	}

	public void SetTextAppearTimeBetweenCharsFastModeMultiplierToDefault()
	{
		_textAppearTimeBetweenCharsFastModeMultiplier = _defaultTextAppearTimeBetweenCharsFastModeMultiplier;
	}

	public void StartConversation(ConversationData texts, OnConversationEnded onConversationEndedInternalCallback = null)
	{
		if (texts == null || texts.Texts.Length == 0)
		{
			Debug.LogError("[ConversationManager.StartConversation] ERROR. Conversation has no texts");
			return;
		}

		_conversationTextsData = texts;
		_conversationTextIndex = 0;
		_onConversationEndedInternalCallback = onConversationEndedInternalCallback;
		StartCoroutine(StartConversationCoroutine());
	}

	public void ConversationProceed()
	{
		_waitingForUserProceed = false;
	}
	#endregion

	private IEnumerator StartConversationCoroutine()
	{
		_onConversationStarted?.Invoke();
		_isConversationInProgress = true;
		do
		{
			ConversationPieceData conversationData = _conversationTextsData.Texts[_conversationTextIndex];
			yield return ShowConversationText(conversationData);

			Debug.Log("Conversation waiting for input");
			// While we wait for input, we register to new language changes
			_waitingForUserProceed = true;
			while (_waitingForUserProceed)
			{
				yield return null;
			}

			++_conversationTextIndex;
		}
		while (_conversationTextIndex < _conversationTextsData.Texts.Length);
		_isConversationInProgress = false;
		_onConversationEnded?.Invoke();
		_onConversationEndedInternalCallback?.Invoke();
	}

	private IEnumerator ShowConversationText(ConversationPieceData conversationDataPiece)
	{
		string text = conversationDataPiece.Text;
		if (string.IsNullOrEmpty(text))
		{
			Debug.LogError(string.Format("[ConversationManager.ShowConversationText] ERROR. ConversationPieceData {0} is null or empty", conversationDataPiece.name));
		}

#if GAMEPLAY_LOGS
		Debug.Log(text);
#endif

		_onConversationTextChanged?.Invoke(string.Empty);

		float currentTextAppearTimeBetweenChars = _textAppearTimeBetweenChars * conversationDataPiece.TextAppearTimeBetweenCharsMultiplier;
		int charIndex = 0;
		_stringBuilder.Clear();

		bool isOnRichTextTag = false;
		bool justWentOutFromRichTextTag;

		_waitingForUserProceed = true;
		bool fastMode = false;
		// For each character
		do
		{
			justWentOutFromRichTextTag = false;

			char nextChar = text[charIndex++];
			if (!isOnRichTextTag && nextChar == kOpenRichTextSectionChar)
			{
				isOnRichTextTag = true;
			}
			else if (isOnRichTextTag && nextChar == kCloseRichTextSectionChar)
			{
				isOnRichTextTag = false;
				justWentOutFromRichTextTag = true;
			}

			_stringBuilder.Append(nextChar);

			if (!isOnRichTextTag && !justWentOutFromRichTextTag)
			{
				// Only if we are outside of a rich text tag, we show the text and wait for next character
				_onConversationTextChanged?.Invoke(_stringBuilder.ToString());
				float timeRemainingForLastChar = currentTextAppearTimeBetweenChars;

				// Wait time until next character
				do
				{
					yield return null;
					timeRemainingForLastChar -= Time.deltaTime;

					// If a Continue key is pressed, this text should appear faster (if not already happening).
					// We need to read it in the internal loop because we need to check every frame so we don't skip the "triggered" event (when the button is pressed).
					// However, we modify currentTextAppearTimeBetweenChars, which will not apply until the next char.
					if (!_waitingForUserProceed && !fastMode)
					{
						currentTextAppearTimeBetweenChars *= _textAppearTimeBetweenCharsFastModeMultiplier;
						fastMode = true;
					}
				}
				while (timeRemainingForLastChar > 0.0f);
			}
		}
		while (charIndex < text.Length);

		// As proceed input at this stage is optional, we need to be sure we always clear this flag.
		// Otherwise, it will cause the conversation is skipped automatically after the text
		// finishes the typing animation
		_waitingForUserProceed = false;

		if (isOnRichTextTag)
		{
			Debug.LogError(string.Format("[ConversationManager.ShowConversationText] ERROR. ConversationPieceData {0} ended while on a rich text tag", conversationDataPiece.name));
		}
	}
}
