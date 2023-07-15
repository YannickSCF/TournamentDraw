using static YannickSCF.GeneralApp.CommonEventsDelegates;

namespace YannickSCF.TournamentDraw.Views.CommonEvents.Settings {
    public static class SettingsViewsEvents {

        // General Volume
        public static event BooleanEventDelegate OnGeneralVolumeMuted;
        public static void ThrowOnGeneralVolumeMuted(bool isOn) {
            OnGeneralVolumeMuted?.Invoke(isOn);
        }

        public static event FloatEventDelegate OnGeneralVolumeChanged;
        public static void ThrowOnGeneralVolumeChanged(float volumeValue) {
            OnGeneralVolumeChanged?.Invoke(volumeValue);
        }

        // Music Volume
        public static event BooleanEventDelegate OnMusicVolumeMuted;
        public static void ThrowOnMusicVolumeMuted(bool isOn) {
            OnMusicVolumeMuted?.Invoke(isOn);
        }

        public static event FloatEventDelegate OnMusicVolumeChanged;
        public static void ThrowOnMusicVolumeChanged(float volumeValue) {
            OnMusicVolumeChanged?.Invoke(volumeValue);
        }

        // SFX Volume
        public static event BooleanEventDelegate OnSFXVolumeMuted;
        public static void ThrowOnSFXVolumeMuted(bool isOn) {
            OnSFXVolumeMuted?.Invoke(isOn);
        }

        public static event FloatEventDelegate OnSFXVolumeChanged;
        public static void ThrowOnSFXVolumeChanged(float volumeValue) {
            OnSFXVolumeChanged?.Invoke(volumeValue);
        }
    }
}
