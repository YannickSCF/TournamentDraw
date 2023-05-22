using TMPro;
using UnityEngine;

namespace YannickSCF.TournamentDraw {

    public enum Ranks { Novizio, Iniziato, Accademico, Cavaliere, MaestroDiScuola }

    public enum Styles {
        Form1, Form2, CourseY,
        Form3Long, Form4Long, Form5Long,
        Form3Dual, Form4Dual, Form5Dual,
        Form3Staff, Form4Staff, Form5Staff,
        Form6, Form7
    }
    public enum ParticipantBasicInfo { Country, Surname, Name, Rank, Styles, Academy, School, Tier };

    public enum PouleAssignType { OneByOne, PouleByPoule, Custom }

    public enum ParticipantSelectionType { Random, ByLevel, ByRank }

    public static class AppConstants {
        public const string LocalizationEmpty = "Empty";

        public static Color BlackLS = new Color(0.1529412f, 0.1647059f, 0.1490196f, 1f);
        public static Color OrangeLS = new Color(0.827451f, 0.4509804f, 0.1607843f, 1f);
        public static Color OrangeLSNotInteractable = new Color(0.827451f, 0.4509804f, 0.1607843f, 0.5f);
        public static Color WhiteLS = new Color(0.9254902f, 0.9254902f, 0.9254902f, 1f);

        public static Color Red = new Color(0.7098039f, 0.08876519f, 0.04313726f, 1f);

        public static void SetColorText(TextMeshProUGUI textComponent, bool isError) {
            if (isError) {
                textComponent.color = Red;
            } else {
                textComponent.color = OrangeLS;
            }
        }
    }
}
