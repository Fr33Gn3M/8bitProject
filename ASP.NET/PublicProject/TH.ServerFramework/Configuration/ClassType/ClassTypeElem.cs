namespace TH.ServerFramework.Configuration
{
    
    using System;
    using System.Configuration;

    public class ClassTypeElem : ConfigurationElement
    {
        private const string ClassTypePropName = "classType";

        protected virtual Type GetClassType()
        {
            return Type.GetType(this.ClassTypeName);
        }

        protected virtual void SetClassType(Type t)
        {
            this.ClassTypeName = t.AssemblyQualifiedName;
        }

        public override string ToString()
        {
            return this.ClassTypeName;
        }

        public Type ClassType
        {
            get
            {
                return this.GetClassType();
            }
            set
            {
                this.SetClassType(value);
            }
        }

        [ConfigurationProperty(ClassTypePropName, IsKey = true)]
        public string ClassTypeName
        {
            get
            {
                return (string)this[ClassTypePropName];
            }
            set
            {
                this[ClassTypePropName] = value;
            }
        }
    }
}

