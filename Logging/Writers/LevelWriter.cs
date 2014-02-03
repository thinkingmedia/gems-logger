using System.Collections.Generic;

namespace Logging.Writers
{
    /// <summary>
    /// Only writes log messages for a given log level (i.e. Error messages only).
    /// </summary>
    public class LevelWriter : FilterWriter
    {
        /// <summary>
        /// The accepted levels.
        /// </summary>
        private readonly HashSet<Logger.eLEVEL> _levels;

        /// <summary>
        /// Filters messages by their log level.
        /// </summary>
        private bool FilterByLevel(Logger.eLEVEL pLevel, string pPrefix, string pMsg)
        {
            return _levels.Contains(pLevel);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public LevelWriter(iLogWriter pWriter, IEnumerable<Logger.eLEVEL> pLevels)
            : base(pWriter)
        {
            _levels = new HashSet<Logger.eLEVEL>(pLevels);
            Filter = FilterByLevel;
        }
    }
}