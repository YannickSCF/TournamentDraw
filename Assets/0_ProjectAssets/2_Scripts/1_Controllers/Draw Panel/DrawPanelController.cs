using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YannickSCF.TournamentDraw.Scriptables;
using YannickSCF.TournamentDraw.Views.Draw;

namespace YannickSCF.TournamentDraw.Controllers.Draw {
    public class DrawPanelController : MonoBehaviour {

        [SerializeField] private DrawPanelView drawPanelView;

        private DrawConfiguration _config;

        #region Mono
        #endregion

        public void Init(DrawConfiguration configuration) {
            _config = configuration;
            drawPanelView.Init(_config);
        }
    }
}
