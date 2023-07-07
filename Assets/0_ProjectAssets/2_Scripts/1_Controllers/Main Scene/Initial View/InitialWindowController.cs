using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YannickSCF.GeneralApp.Controller.UI.Windows;
using YannickSCF.TournamentDraw.Importers;
using YannickSCF.TournamentDraw.MainManagers.Controllers;
using YannickSCF.TournamentDraw.Popups;
using YannickSCF.TournamentDraw.Views.MainScene.Initial;

namespace YannickSCF.TournamentDraw.Controllers.MainScene.Initial {
    public class InitialWindowController : WindowController<InitialWindowView> {

        public enum InitialButtonsStates { Main, StartDraw, Settings, Exit, ConfigurationDraw }

        private Stack<InitialButtonsStates> _breadcrumbs;

        private Action _configurationCallback;
        private Action _settingsCallback;

        private GameManager _gameManager;

        #region Mono
        protected override void OnEnable() {
            base.OnEnable();

            InitialWindowViewEvents.OnSideButtonClicked += SideButtonPressed;
        }

        protected override void OnDisable() {
            base.OnDisable();

            InitialWindowViewEvents.OnSideButtonClicked -= SideButtonPressed;
        }
        #endregion

        #region Event listeners methods
        private void SideButtonPressed(ButtonType buttonType) {
            switch (buttonType) {
                case ButtonType.StartDraw:
                    SwitchState(InitialButtonsStates.StartDraw);
                    break;
                case ButtonType.Settings:
                    SwitchState(InitialButtonsStates.Settings);
                    break;
                case ButtonType.Exit:
                    SwitchState(InitialButtonsStates.Exit);
                    break;
                case ButtonType.ContinueDraw:
                    _gameManager.SwitchState(States.Draw);
                    break;
                case ButtonType.NewDraw:
                    SwitchState(InitialButtonsStates.ConfigurationDraw);
                    break;
                case ButtonType.LoadDraw:
                    string filePath = FileImporter.SelectFileWithBrowser();
                    if (!string.IsNullOrEmpty(filePath)) {
                        _gameManager.Config = FileImporter.ImportDrawFormJSON(filePath);
                        SwitchState(InitialButtonsStates.ConfigurationDraw);
                    } else {
                        ErrorPopupController errorPopup = _gameManager.BaseUIController.ShowPopup<ErrorPopupController, ErrorPopupView>("Error");
                        errorPopup.SetErrorTexts("Error: Carga fallida", "El formato del fichero seleccionado no es válido");
                        errorPopup.SetCallback(CloseErrorPopup);
                    }
                    break;
                case ButtonType.Back: GoBack(); break;
            }
        }
        #endregion

        public override void Init(string windowId) {
            _gameManager = GameManager.Instance;

            base.Init(windowId);

            _breadcrumbs = new Stack<InitialButtonsStates>();
            SwitchState(InitialButtonsStates.Main);
        }

        public void SetCallbacks(Action configurationCallback, Action settingsCallback) {
            _configurationCallback = configurationCallback;
            _settingsCallback = settingsCallback;
        }

        public void SwitchState(InitialButtonsStates newState) {
            View.SetViewState((int)newState);

            switch (newState) {
                case InitialButtonsStates.Main:
                default:
                    break;
                case InitialButtonsStates.StartDraw:
                    View.SetContinueInteractable(_gameManager.IsConfigToContinue());
                    break;
                case InitialButtonsStates.Settings:
                    _settingsCallback?.Invoke();
                    break;
                case InitialButtonsStates.Exit:
                    ExitAppPopupController exitPopup = _gameManager.BaseUIController.ShowPopup<ExitAppPopupController, ExitAppPopupView>("AskExit");
                    exitPopup.SetCallbacks(CloseExitPopup, ExitApp);
                    exitPopup.SetSaveAndExitOption(false);
                    break;
                case InitialButtonsStates.ConfigurationDraw:
                    _configurationCallback?.Invoke();
                    break;
            }

            _breadcrumbs.Push(newState);
        }

        private void CloseExitPopup() {
            _gameManager.BaseUIController.ClosePopup<ExitAppPopupController, ExitAppPopupView>("AskExit");
            GoBack();
        }

        private void CloseErrorPopup() {
            _gameManager.BaseUIController.ClosePopup<ErrorPopupController, ErrorPopupView>("Error");
        }

        private void ExitApp(bool saveAndExit) {
            _gameManager.SaveAndExit(saveAndExit);
        }

        public void GoBack() {
            _ = _breadcrumbs.Pop();
            SwitchState(_breadcrumbs.Pop());
        }
    }
}
