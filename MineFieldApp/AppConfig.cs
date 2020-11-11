namespace MineFieldApp
{
    public class AppConfig
    {
        public MineSettings MineSettings { get; set; }
    }

    public class MineSettings
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        public int Lifeline { get; set; }
    }
}
