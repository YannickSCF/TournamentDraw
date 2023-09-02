using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YannickSCF.GeneralApp.Controller.Audio;
using YannickSCF.TournamentDraw.Views.CommonEvents.Settings;
using YannickSCF.TournamentDraw.Views.Draw.Panel.Poules.CompetitorRow;

namespace YannickSCF.TournamentDraw.MainManagers.Controllers {
    public class AudioController : BaseAudioController {

        #region Mono
        protected override void OnEnable() {
            base.OnEnable();

            SettingsViewsEvents.OnGeneralVolumeMuted += GeneralVolumeMuted;
            SettingsViewsEvents.OnGeneralVolumeChanged += GeneralVolumeChanged;
            SettingsViewsEvents.OnMusicVolumeMuted += MusicVolumeMuted;
            SettingsViewsEvents.OnMusicVolumeChanged += MusicVolumeChanged;
            SettingsViewsEvents.OnSFXVolumeMuted += SFXVolumeMuted;
            SettingsViewsEvents.OnSFXVolumeChanged += SFXVolumeChanged;

            BasicCompetitorRow.PouleNameRevealed += PlaySFX;
        }

        protected override void OnDisable() {
            base.OnDisable();

            SettingsViewsEvents.OnGeneralVolumeMuted -= GeneralVolumeMuted;
            SettingsViewsEvents.OnGeneralVolumeChanged -= GeneralVolumeChanged;
            SettingsViewsEvents.OnMusicVolumeMuted -= MusicVolumeMuted;
            SettingsViewsEvents.OnMusicVolumeChanged -= MusicVolumeChanged;
            SettingsViewsEvents.OnSFXVolumeMuted -= SFXVolumeMuted;
            SettingsViewsEvents.OnSFXVolumeChanged -= SFXVolumeChanged;

            BasicCompetitorRow.PouleNameRevealed -= PlaySFX;
        }
        #endregion

        #region Event listeners methods
        private void GeneralVolumeMuted(bool boolValue) {
            MuteSource(GeneralApp.AudioSources.General, boolValue);
        }

        private void GeneralVolumeChanged(float floatValue) {
            SetGeneralVolume(GeneralApp.AudioSources.General, floatValue);
        }

        private void MusicVolumeMuted(bool boolValue) {
            MuteSource(GeneralApp.AudioSources.Music, boolValue);
        }

        private void MusicVolumeChanged(float floatValue) {
            SetGeneralVolume(GeneralApp.AudioSources.Music, floatValue);
        }

        private void SFXVolumeMuted(bool boolValue) {
            MuteSource(GeneralApp.AudioSources.SFX, boolValue);
        }

        private void SFXVolumeChanged(float floatValue) {
            SetGeneralVolume(GeneralApp.AudioSources.SFX, floatValue);
        }
        #endregion
    }
}
