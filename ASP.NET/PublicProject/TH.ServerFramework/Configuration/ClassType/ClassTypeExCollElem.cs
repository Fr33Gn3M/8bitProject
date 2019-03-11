namespace TH.ServerFramework.Configuration
{
    using System.Configuration;

    [ConfigurationCollection(typeof(ClassTypeExElem))]
    public class ClassTypeExCollElem : ClassTypeCollElem
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ClassTypeExElem();
        }
    }
}

