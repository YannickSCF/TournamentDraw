using System;
using UnityEngine;
using YannickSCF.GeneralApp.Controller.UI.Popups;
using YannickSCF.TournamentDraw.FileManagement;
using YannickSCF.TournamentDraw.MainManagers.Controllers;
using YannickSCF.TournamentDraw.Scriptables;

namespace YannickSCF.TournamentDraw.Popups {
    public class SaveDataPopupController : PopupController<SaveDataPopupView> {

        private Action _onClosePopup;

        #region Mono
        protected override void OnEnable() {
            base.OnEnable();

            View.OnCloseButtonPressed += CloseButtonPressed;
            View.OnSaveJSONButtonPressed += SaveJSON;
            View.OnSavePDFButtonPressed += SavePDF;
        }

        protected override void OnDisable() {
            base.OnDisable();

            View.OnCloseButtonPressed -= CloseButtonPressed;
            View.OnSaveJSONButtonPressed -= SaveJSON;
            View.OnSavePDFButtonPressed -= SavePDF;

        }
        #endregion

        public void SetClosePopupCallback(Action onClosePopup) {
            _onClosePopup = onClosePopup;
        }

        public override void Close() {
            base.Close();
            // TODO: Algun mensaje (?)
        }

        private void CloseButtonPressed() {
            _onClosePopup?.Invoke();
        }

        private void SaveJSON() {
            DrawConfiguration config = GameManager.Instance.Config;
            FileExporter.SaveFileBrowser(config.DrawName, JsonUtility.ToJson(config));
        }

        private void SavePDF() {
            throw new NotImplementedException();
        }
    }
}
