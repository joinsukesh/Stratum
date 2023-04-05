
namespace Stratum.Foundation.Forms.Repositories
{
    using System;
    using System.Collections.Generic;

    public interface ISitecoreFormsRepository
    {
        void SaveDataInSitecoreFormsDb(Guid formId, List<KeyValuePair<string, string>> formFieldNameValues, Guid? userId);
    }
}