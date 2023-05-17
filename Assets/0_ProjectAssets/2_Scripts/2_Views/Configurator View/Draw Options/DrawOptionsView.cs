using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace YannickSCF.TournamentDraw.Settings.View {
    public class DrawOptionsView : MonoBehaviour {

        [Header("Tournament draw Name")]
        [SerializeField] private TMP_InputField drawNameInputField;

        [Header("Total competitors (auto filled)")]
        [SerializeField] private TextMeshProUGUI totalCompetitorsTMP;

        [Header("Number of poules and max poule size")]
        [SerializeField] private TMP_InputField numberOfPoulesInputField;
        [SerializeField] private TMP_InputField maxPouleSizeInputField;

        [Header("Draw options")]
        [SerializeField] private TMP_Dropdown pouleAssignDropdown;
        [SerializeField] private TMP_Dropdown participantSelectionDropdown;

        [Header("Participants information")]
        [SerializeField] private List<Toggle> allInfoCheckmarks;


    }
}
