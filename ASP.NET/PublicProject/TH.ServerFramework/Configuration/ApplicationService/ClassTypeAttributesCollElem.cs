namespace TH.ServerFramework.Configuration
{
    using System.Configuration;

    [ConfigurationCollection(typeof(ClassTypeAttributesElem))]
    public class ClassTypeAttributesCollElem : ClassTypeExCollElem
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ClassTypeAttributesElem();
        }
    }
}

