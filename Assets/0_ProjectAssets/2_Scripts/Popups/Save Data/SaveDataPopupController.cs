using System;
using UnityEngine;
using YannickSCF.GeneralApp.Controller.UI.Popups;
using YannickSCF.TournamentDraw.FileManagement;
using YannickSCF.TournamentDraw.MainManagers.Controllers;
using YannickSCF.TournamentDraw.Scriptables;

namespace YannickSCF.TournamentDraw.Popups {
    public class SaveDataPopupData : PopupData {
        private Action _closePopupAction;

        public SaveDataPopupData(
            string popupId,
            Action closePopupAction) : base(popupId) {
            _closePopupAction = closePopupAction;
        }

        public Action ClosePopupAction { get => _closePopupAction; }
    }

    public class SaveDataPopupController : PopupController {

        private SaveDataPopupView _view;

        private Action _onClosePopup;

        #region Mono
        private void Awake() {
            _view = GetView<SaveDataPopupView>();
        }

        protected override void OnEnable() {
            base.OnEnable();

            _view.OnCloseButtonPressed += CloseButtonPressed;
            _view.OnSaveJSONButtonPressed += SaveJSON;
            _view.OnSavePDFButtonPressed += SavePDF;
        }

        protected override void OnDisable() {
            base.OnDisable();

            _view.OnCloseButtonPressed -= CloseButtonPressed;
            _view.OnSaveJSONButtonPressed -= SaveJSON;
            _view.OnSavePDFButtonPressed -= SavePDF;

        }
        #endregion

        #region Event listeners methods
        private void CloseButtonPressed() {
            _onClosePopup?.Invoke();
        }

        private void SaveJSON() {
            DrawConfiguration config = GameManager.Instance.Config;
            FileExporter.SaveFileBrowser(config.DrawName, JsonUtility.ToJson(config));
        }

        private void SavePDF() {
            // TODO
            throw new NotImplementedException();
        }
        #endregion

        public override void Init(PopupData popupData) {
            SaveDataPopupData saveDataPopupData = (SaveDataPopupData)popupData;
            _onClosePopup = saveDataPopupData.ClosePopupAction;

            base.Init(popupData);
        }
    }
}
