
namespace Stratum.Foundation.Logging.Seq
{
    using Basiscore.SeqLogger.Services;
    using log4net.spi;
    using System.Collections.Generic;

    public class StratumSeqLogger : SeqLogger
    {
        protected override void Append(LoggingEvent loggingEvent)
        {
            LogProperties = new List<KeyValuePair<string, string>>();
            LogProperties.Add(new KeyValuePair<string, string> ("Prop_Key_1", "Prop_Value_1"));
            LogProperties.Add(new KeyValuePair<string, string>("Prop_Key_2", "Prop_Value_2"));
            base.Append(loggingEvent);
        }
    }
}
