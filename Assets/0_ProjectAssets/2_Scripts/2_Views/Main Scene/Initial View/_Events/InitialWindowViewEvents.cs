namespace YannickSCF.TournamentDraw.Views.MainScene.Initial {
    public static class InitialWindowViewEvents {

        public delegate void SideButtonClicked(ButtonType buttonType);

        public static event SideButtonClicked OnSideButtonClicked;
        public static void ThrowOnSideButtonClicked(ButtonType buttonType) {
            OnSideButtonClicked?.Invoke(buttonType);
        }
    }
}
