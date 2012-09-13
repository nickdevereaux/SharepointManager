using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;

namespace SPM2.SharePoint.Model
{
    /// <summary>
    /// Specialized ExportAttribute used for the SharePoint treeview object model.
    /// </summary>
    public class ExportToNodeAttribute : ExportAttribute
    {
        public bool AutoBind { get; set; }

        public ExportToNodeAttribute(Type contractType)
            : this(AttributedModelServices.GetContractName(contractType))
        {
        }

        public ExportToNodeAttribute(string contractName)
            : base(contractName, typeof(SPNode))
        {
            AutoBind = true;
        }
    }
}
