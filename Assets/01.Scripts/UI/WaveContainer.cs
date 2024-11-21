using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

public class WaveContainer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _waveNumText;
    [SerializeField] private CanvasGroup _waveLogContent;
	[SerializeField] private TextMeshProUGUI _waveLogText;
	Sequence seq;

	//Managers
	private WaveManager _waveManager;

	private void Awake()
	{
		_waveLogContent.alpha = 0.0f;
	}

	private void Start()
	{
		seq = DOTween.Sequence();
		_waveManager = WaveManager.Instance;
		_waveManager.OnGenerateStartEvent += HandleOnGenerateStart;
		_waveManager.OnGenerateEndEvent += HandleOnGenerateEnd;
	}


	private void HandleOnGenerateStart()
	{
		seq.Kill();
		_waveNumText.text = (_waveManager.WaveNumber+1).ToString();
		_waveLogContent.DOFade(1, 0.2f);
	}

	private void HandleOnGenerateEnd()
	{
		seq.AppendCallback(() => _waveLogText.text = "모든 적 처치 완료");
		seq.AppendInterval(1);
		seq.Append(_waveLogContent.DOFade(0, 0.2f));
	}

	private void Update()
	{
		_waveLogText.text = $"남은 적 : {_waveManager.EnemyCount.ToString()}";
	}
}
