using DG.Tweening;
using UnityEngine;

public class MousePosPoint : MonoBehaviour
{
	[SerializeField] private InputReaderSO _inputReaderSO;

	private void Awake()
	{
		_inputReaderSO.OnLeftClickEvnet += HandleMouseClick;
	}

	private void OnDestroy()
	{
		_inputReaderSO.OnLeftClickEvnet -= HandleMouseClick;
	}

	private void HandleMouseClick(bool value)
	{
		if (value == true) return;

		Vector2 pos = Camera.main.ScreenToWorldPoint(_inputReaderSO.MousePosition);
		transform.position = pos;
	}
}
