
namespace YannickSCF.TournamentDraw {
    public static class LocalizationKeys {

        //#region --------------------------------- ERROR MESSAGES ---------------------------------
        //public const string MIN_OF_PARTICIPANTS = "MIN_OF_PARTICIPANTS";
        //public const string X_PARTICIPANT_INCOMPLETE_NAME = "X_PARTICIPANT_INCOMPLETE_NAME";
        //// TODO: Future posible checkers
        //public const string X_PARTICIPANT_INCOMPLETE_COUNTRY = "X_PARTICIPANT_INCOMPLETE_OTHER";
        //public const string X_PARTICIPANT_INCOMPLETE_RANK = "X_PARTICIPANT_INCOMPLETE_OTHER";
        //public const string X_PARTICIPANT_INCOMPLETE_STYLES = "X_PARTICIPANT_INCOMPLETE_OTHER";
        //public const string X_PARTICIPANT_INCOMPLETE_ACADEMY = "X_PARTICIPANT_INCOMPLETE_OTHER";
        //public const string X_PARTICIPANT_INCOMPLETE_SCHOOL = "X_PARTICIPANT_INCOMPLETE_OTHER";
        //public const string X_PARTICIPANT_INCOMPLETE_TIER = "X_PARTICIPANT_INCOMPLETE_OTHER";
        //public const string X_PARTICIPANT_INCOMPLETE_SABER = "X_PARTICIPANT_INCOMPLETE_OTHER";

        //public const string DRAW_NAME_NEEDED = "DRAW_NAME_NEEDED";
        //public const string INVALID_POULES_MIN = "INVALID_POULES_MIN";
        //public const string INVALID_POULES_MAX = "INVALID_POULES_MAX";
        //public const string INVALID_POULES_EQUAL = "INVALID_POULES_EQUAL";
        //public const string NUMBER_OF_POULES_NEEDED = "NUMBER_OF_POULES_NEEDED";
        //public const string MAX_POULE_SIZE_NEEDED = "MAX_POULE_SIZE_NEEDED";

        //public const string INCORRECT_VALUE_FOR_INPUT = "INCORRECT_VALUE_FOR_INPUT";
        //#endregion

        //#region --------------------------------- SUCCESS MESSAGES ---------------------------------
        //public const string ALL_READY = "ALL_READY";
        //#endregion

        // TEMP: Replace when localization is implemented
        #region --------------------------------- ERROR MESSAGES ---------------------------------
        public const string MIN_OF_PARTICIPANTS = "Se requieren al menos 4 participantes";
        public const string X_PARTICIPANT_INCOMPLETE_NAME = "El nombre de un participante está incompleto";
        // TODO: Future posible checkers
        public const string X_PARTICIPANT_INCOMPLETE_COUNTRY = "Falta el PAÍS de al menos un participante";
        public const string X_PARTICIPANT_INCOMPLETE_RANK = "Falta el RANGO de al menos un participante";
        public const string X_PARTICIPANT_INCOMPLETE_STYLES = "Falta asignar algún ESTILO a al menos un participante";
        public const string X_PARTICIPANT_INCOMPLETE_ACADEMY = "Falta la ACADEMIA de al menos un participante";
        public const string X_PARTICIPANT_INCOMPLETE_SCHOOL = "Falta la ESCUELA de al menos un participante";
        public const string X_PARTICIPANT_INCOMPLETE_TIER = "Falta el NIVEL de al menos un participante";
        public const string X_PARTICIPANT_INCOMPLETE_SABER = "Falta el COLOR DE SABLE de al menos un participante";

        public const string DRAW_NAME_NEEDED = "Se necesita el NOMBRE DEL SORTEO";
        public const string INVALID_POULES_MIN = "El número de participantes es muy pequeño para las poules asignadas";
        public const string INVALID_POULES_MAX = "El número de participantes es demasiado grande para las poules asignadas";
        public const string INVALID_POULES_EQUAL = "El número de participantes no coincide con el tamaño de la poule";
        public const string NUMBER_OF_POULES_NEEDED = "Se necesita asignar un NUMERO DE POULES";
        public const string MAX_POULE_SIZE_NEEDED = "Se necesita asignar un TAMAÑO (máximo) DE POULES";

        public const string INCORRECT_VALUE_FOR_INPUT = "Valor incorrecto";
        #endregion

        #region --------------------------------- SUCCESS MESSAGES ---------------------------------
        public const string ALL_READY = "¡TODO LISTO! Puedes comenzar el sorteo";
        #endregion
    }
}
