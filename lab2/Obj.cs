using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    [Serializable]
    internal class Obj : IDataErrorInfo
    {
        public string Name { get; set; }

        public string Error
        {
            get
            {
                return "ObjValidationFails";
            }
        }
    

    public virtual string this[string columnName]
        {
            get
            {
                string err = null;
                if (columnName == "Name")
                {
                    if (string.IsNullOrWhiteSpace(Name)) err = "Name must not be empty";
                }
                return err;
            }
        }

        public virtual Obj NextObj(int index) {
            return null;
        }
        public Obj(string name)
        {
            Name = name;
        }
        public virtual void DeleteObj() { }
    }
}
