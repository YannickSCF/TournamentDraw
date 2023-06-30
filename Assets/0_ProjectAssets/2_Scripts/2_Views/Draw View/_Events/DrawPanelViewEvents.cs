
namespace YannickSCF.TournamentDraw.Views.DrawScene.Events {
    public static class DrawPanelViewEvents {

        public static event CommonEventsDelegates.SimpleEvent OnStartButtonClicked;
        public static void ThrowOnStartButtonClicked() {
            OnStartButtonClicked?.Invoke();
        }

        public static event CommonEventsDelegates.SimpleEvent OnNextButtonClicked;
        public static void ThrowOnNextButtonClicked() {
            OnNextButtonClicked?.Invoke();
        }
    }
}
