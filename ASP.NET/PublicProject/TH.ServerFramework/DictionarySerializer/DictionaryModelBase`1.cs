// VBConversions Note: VB project level imports
using System.Collections.Generic;
using System;
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.Collections;
using System.Linq;
// End of VB project level imports

using System.Linq.Expressions;


namespace TH.ServerFramework.DictionarySerializer
{
    public abstract class DictionaryModelBase<TModel>
    {

        private readonly System.Collections.Generic.IDictionary<string, object> _dictionary;

        protected DictionaryModelBase(System.Collections.Generic.IDictionary<string, object> dictionary)
        {
            _dictionary = dictionary;
        }

        private string GetPropertyName<TResult>(Expression<Func<TModel, TResult>> propExp)
        {
            var propertyName = ((MemberExpression)propExp.Body).Member.Name;
            return propertyName;
        }

        protected TResult GetPropertyValue<TResult>(Expression<Func<TModel, TResult>> propExp)
        {
            var propertyName = GetPropertyName(propExp);
            if (!_dictionary.ContainsKey(propertyName))
            {
                return default(TResult);
            }
            return (TResult)_dictionary[propertyName];
        }

        protected void AddOrUpdatePropertyValue<TResult>(Expression<Func<TModel, TResult>> propExp, TResult value)
        {
            var propName = GetPropertyName(propExp);
            if (!_dictionary.ContainsKey(propName))
            {
                _dictionary.Add(propName, value);
            }
            else
            {
                _dictionary[propName] = value;
            }
        }
    }
}
