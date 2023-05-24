
using static YannickSCF.TournamentDraw.CommonEventsDelegates;

namespace YannickSCF.TournamentDraw.Views.InitialPanel.Events {
    public static class InitialPanelViewEvents {

        // ------------------------------- Events -------------------------------

        public static event SimpleEvent OnNewDrawButtonPressed;
        public static void ThrowOnNewDrawButtonPressed() {
            OnNewDrawButtonPressed?.Invoke();
        }

        public static event SimpleEvent OnLoadDrawButtonPressed;
        public static void ThrowOnLoadDrawButtonPressed() {
            OnLoadDrawButtonPressed?.Invoke();
        }

        public static event SimpleEvent OnSettingsButtonPressed;
        public static void ThrowOnSettingsButtonPressed() {
            OnSettingsButtonPressed?.Invoke();
        }
    }
}
