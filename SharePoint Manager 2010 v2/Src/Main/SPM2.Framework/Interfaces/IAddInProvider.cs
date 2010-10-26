using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SPM2.Framework.Collections;
using System.IO;
using SPM2.Framework.Configuration;

namespace SPM2.Framework
{
    [Provider(DefaultType="SPM2.Framework.AddInProvider", ProviderType=ProviderTypes.AddIn)]
    public interface IAddInProvider
    {
        TypeAttachmentDictionary TypeAttachments { get; set; }
        ClassDescriptorLookupDictionary ClassDescriptorLookup { get; set; }

        IList<T> CreateAttachments<T>(string id, InitializerDelegate<T> initializer);
    }
}
