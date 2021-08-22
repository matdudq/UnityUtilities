namespace Utilities
{
    /// <summary>
    /// A singleton for basic classes.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Singleton<T> where T : Singleton<T>
    {
        #region Properties

        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    GameConsole.LogError(typeof(T), "Inst doesn't exist!");
                }

                return instance;
            }
        }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Default constructor, which automatically assigns the <see cref="Instance"/>.
        /// </summary>
        public Singleton()
        {
            instance = (T)this;
        }

        #endregion Constructors
    }
}
