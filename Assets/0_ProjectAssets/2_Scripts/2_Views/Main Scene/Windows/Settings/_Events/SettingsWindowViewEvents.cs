namespace YannickSCF.TournamentDraw.Views.MainScene.Windows.Settings {
    public static class SettingsWindowViewEvents {

        // General Volume
        public static event CommonEventsDelegates.BooleanEvent OnGeneralVolumeMuted;
        public static void ThrowOnGeneralVolumeMuted(bool isOn) {
            OnGeneralVolumeMuted?.Invoke(isOn);
        }

        public static event CommonEventsDelegates.FloatEvent OnGeneralVolumeChanged;
        public static void ThrowOnGeneralVolumeChanged(float volumeValue) {
            OnGeneralVolumeChanged?.Invoke(volumeValue);
        }

        // Music Volume
        public static event CommonEventsDelegates.BooleanEvent OnMusicVolumeMuted;
        public static void ThrowOnMusicVolumeMuted(bool isOn) {
            OnMusicVolumeMuted?.Invoke(isOn);
        }

        public static event CommonEventsDelegates.FloatEvent OnMusicVolumeChanged;
        public static void ThrowOnMusicVolumeChanged(float volumeValue) {
            OnMusicVolumeChanged?.Invoke(volumeValue);
        }

        // SFX Volume
        public static event CommonEventsDelegates.BooleanEvent OnSFXVolumeMuted;
        public static void ThrowOnSFXVolumeMuted(bool isOn) {
            OnSFXVolumeMuted?.Invoke(isOn);
        }

        public static event CommonEventsDelegates.FloatEvent OnSFXVolumeChanged;
        public static void ThrowOnSFXVolumeChanged(float volumeValue) {
            OnSFXVolumeChanged?.Invoke(volumeValue);
        }
    }
}
