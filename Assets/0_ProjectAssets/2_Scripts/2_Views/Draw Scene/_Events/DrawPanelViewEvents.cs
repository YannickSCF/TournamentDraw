using static YannickSCF.GeneralApp.CommonEventsDelegates;

namespace YannickSCF.TournamentDraw.Views.DrawScene.Events {
    public static class DrawPanelViewEvents {

        public static event SimpleEventDelegate OnStartButtonClicked;
        public static void ThrowOnStartButtonClicked() {
            OnStartButtonClicked?.Invoke();
        }

        public static event SimpleEventDelegate OnNextButtonClicked;
        public static void ThrowOnNextButtonClicked() {
            OnNextButtonClicked?.Invoke();
        }

        public static event SimpleEventDelegate OnSaveButtonClicked;
        public static void ThrowOnSaveButtonClicked() {
            OnSaveButtonClicked?.Invoke();
        }

        public static event SimpleEventDelegate OnSettingsButtonClicked;
        public static void ThrowOnSettingsButtonClicked() {
            OnSettingsButtonClicked?.Invoke();
        }
    }
}
