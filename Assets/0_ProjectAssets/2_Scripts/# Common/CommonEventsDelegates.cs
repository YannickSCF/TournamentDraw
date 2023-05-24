
namespace YannickSCF.TournamentDraw {
    public class CommonEventsDelegates {
        public delegate void SimpleEvent();
        public delegate void StringEvent(string strValue);
        public delegate void IntegerEvent(int intValue);
        public delegate void FloatEvent(float floatValue);
        public delegate void BooleanEvent(bool boolValue);
    }
}
