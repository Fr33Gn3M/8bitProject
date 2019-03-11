namespace TH.ServerFramework.Configuration
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;

    [ConfigurationCollection(typeof(ClassTypeElem))]
    public class ClassTypeCollElem : ConfigurationElementCollection
    {
        public void AddType(Type type)
        {
            ClassTypeElem elem = (ClassTypeElem) this.CreateNewElement();
            elem.ClassType = type;
            base.BaseAdd(elem);
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ClassTypeElem();
        }

        public Type[] GetAllClassTypes()
        {
            List<Type> ls = new List<Type>();
            int count = this.Count - 1;
            for (int i = 0; i <= count; i++)
            {
                ClassTypeElem elem = (ClassTypeElem) this.BaseGet(i);
                Type classType = elem.ClassType;
                ls.Add(classType);
            }
            return ls.ToArray();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            ClassTypeElem elem = (ClassTypeElem) element;
            return elem.ClassTypeName;
        }
    }
}

