using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YannickSCF.TournamentDraw.Views.Draw.Events {
    public static class DrawPanelViewEvents {

        public static event CommonEventsDelegates.SimpleEvent OnNextButtonClicked;
        public static void ThrowOnNextButtonClicked() {
            OnNextButtonClicked?.Invoke();
        }
    }
}
