using TestApplication.Interfaces;

namespace TestApplication.Services {
    public class TimeOfTheDay: TimeOfTheDayInterface {

        private string? DayAppearance { get; set; }
        private string? DayTime { get; set; }

        public TimeOfTheDay() {
            DateTime currentTime = DateTime.Now;

            int currentHour = currentTime.Hour;
            if (currentHour >= 12 && currentHour < 18) {
                DayTime = "Good Afternoon!";
                DayAppearance = "purple";
            } else if (currentHour >= 18 && currentHour < 24) {
                DayTime = "Good evening!";
                DayAppearance = "brown";
            } else if (currentHour >= 0 && currentHour < 6) {
                DayTime = "Good Night!";
                DayAppearance = "black";
            } else {
                DayTime = "Good Morning!";
                DayAppearance = "yellow";
            }
        }
        public string GetDayTime() {
            if (DayTime == null) {
                throw new NullReferenceException("Server Time Error.");
            }
            return DayTime;
        }

        public string GetAppearance() {
            if (DayAppearance == null) {
                throw new NullReferenceException("Server Time Error.");
            }
            return DayAppearance;
        }
    }
}