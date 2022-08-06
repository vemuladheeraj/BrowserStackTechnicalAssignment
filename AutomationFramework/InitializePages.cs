namespace AutomationFramework
{
    public class InitializePages
    {
        private static readonly InitializePages _instance = new InitializePages();

        static InitializePages()
        {
        }

        private InitializePages()
        {
        }

        public static InitializePages Instance
        {
            get
            {
                return _instance;
            }
        }
    }
}