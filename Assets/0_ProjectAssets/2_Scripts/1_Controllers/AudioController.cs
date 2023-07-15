using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YannickSCF.GeneralApp.Controller.Audio;
using YannickSCF.TournamentDraw.Views.CommonEvents.Settings;

public class AudioController : BaseAudioController
{
    private string _backgroundMusicName;

    #region Mono
    protected override void OnEnable() {
        base.OnEnable();

        SettingsViewsEvents.OnGeneralVolumeMuted += GeneralVolumeMuted;
        SettingsViewsEvents.OnGeneralVolumeChanged += GeneralVolumeChanged;
        SettingsViewsEvents.OnMusicVolumeMuted += MusicVolumeMuted;
        SettingsViewsEvents.OnMusicVolumeChanged += MusicVolumeChanged;
        SettingsViewsEvents.OnSFXVolumeMuted += SFXVolumeMuted;
        SettingsViewsEvents.OnSFXVolumeChanged += SFXVolumeChanged;
    }

    protected override void OnDisable() {
        base.OnDisable();

        SettingsViewsEvents.OnGeneralVolumeMuted -= GeneralVolumeMuted;
        SettingsViewsEvents.OnGeneralVolumeChanged -= GeneralVolumeChanged;
        SettingsViewsEvents.OnMusicVolumeMuted -= MusicVolumeMuted;
        SettingsViewsEvents.OnMusicVolumeChanged -= MusicVolumeChanged;
        SettingsViewsEvents.OnSFXVolumeMuted -= SFXVolumeMuted;
        SettingsViewsEvents.OnSFXVolumeChanged -= SFXVolumeChanged;
    }
    #endregion

    #region Event listeners methods
    private void GeneralVolumeMuted(bool boolValue) {
        MuteSource(YannickSCF.GeneralApp.AudioSources.General, boolValue);
    }

    private void GeneralVolumeChanged(float floatValue) {
        SetGeneralVolume(YannickSCF.GeneralApp.AudioSources.General, floatValue);
    }

    private void MusicVolumeMuted(bool boolValue) {
        MuteSource(YannickSCF.GeneralApp.AudioSources.Music, boolValue);
    }

    private void MusicVolumeChanged(float floatValue) {
        SetGeneralVolume(YannickSCF.GeneralApp.AudioSources.Music, floatValue);
    }

    private void SFXVolumeMuted(bool boolValue) {
        MuteSource(YannickSCF.GeneralApp.AudioSources.SFX, boolValue);
    }

    private void SFXVolumeChanged(float floatValue) {
        SetGeneralVolume(YannickSCF.GeneralApp.AudioSources.SFX, floatValue);
    }
    #endregion
}
