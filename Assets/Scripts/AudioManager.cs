using NodeCanvas.DialogueTrees;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public FMODUnity.StudioEventEmitter Ambience;
    public FMODUnity.StudioEventEmitter Music;

    private void OnEnable()
    {
        DialogueTree.OnDialogueFinished += OnDialogueFinished;
    }

    private void OnDisable()
    {
        DialogueTree.OnDialogueFinished -= OnDialogueFinished;
    }

    private void OnDialogueFinished(DialogueTree obj)
    {
        SetAmbienceVolume(0);
        SetMusicVolume(0);
    }

    public void SetAmbienceVolume(float value)
    {
        Ambience.SetParameter("Volume", value);
    }

    public void SetMusicVolume(float value)
    {
        Music.SetParameter("Volume", value);
    }
}
