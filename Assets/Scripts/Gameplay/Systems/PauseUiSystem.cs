using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PauseUiSystem
{
    public event Action ContinueEvent, RestartEvent, QuitEvent;
    private readonly ProgressData _progressData;
    private PauseView _view;

    public PauseUiSystem(ProgressData progressData) {
        _progressData = progressData;
    }

    public void Initialize(PauseView view) {
        _view = view;
        _view.ContinueEvent += OnContinueSelected;
        _view.RestartEvent += OnRestartSelected;
        _view.QuitEvent += OnQuitSelected;
    }

    public void Open() {
        _view.SetCurrentLevelNumber(_progressData.levelsHolder.current, _progressData.levelsHolder.levelsCount);
        _view.Open();
    }

    public void Close() {
        _view.Close();
    }

    private void OnContinueSelected() =>
        ContinueEvent?.Invoke();

    private void OnQuitSelected() =>
        QuitEvent?.Invoke();

    private void OnRestartSelected() =>
        RestartEvent?.Invoke();
}