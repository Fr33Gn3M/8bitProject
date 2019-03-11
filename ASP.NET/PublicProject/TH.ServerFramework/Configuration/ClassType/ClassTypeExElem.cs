namespace TH.ServerFramework.Configuration
{
    using System;
    using System.Configuration;

    public class ClassTypeExElem : ClassTypeElem
    {
        private const string ArgumentsPropName = "Arguments";

        protected override Type GetClassType()
        {
            if ((this.Arguments == null) || (this.Arguments.Count == 0))
            {
                return base.GetClassType();
            }
            Type genType = base.GetClassType();
            Type[] argsTypes = this.Arguments.GetAllClassTypes();
            return genType.MakeGenericType(argsTypes);
        }

        protected override void SetClassType(Type t)
        {
            if (!t.IsGenericType)
            {
                base.SetClassType(t);
            }
            else
            {
                Type genDefType = t.GetGenericTypeDefinition();
                base.SetClassType(genDefType);
                Type[] argTypes = t.GetGenericArguments();
                ClassTypeCollElem argsElem = new ClassTypeCollElem();
                foreach (Type argType in argTypes)
                {
                    argsElem.AddType(argType);
                }
                this.Arguments = argsElem;
            }
        }

        [ConfigurationProperty(ArgumentsPropName, IsRequired = false)]
        public ClassTypeCollElem Arguments
        {
            get
            {
                return (ClassTypeCollElem)this[ArgumentsPropName];
            }
            set
            {
                this[ArgumentsPropName] = value;
            }
        }
    }
}

