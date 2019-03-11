namespace TH.ServerFramework.WebClientPoint
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class MyRestClientFactory
    {

        private static Lazy<InnerMyRestClientFactory> _lzFactory = new Lazy<InnerMyRestClientFactory>(() => new InnerMyRestClientFactory());
        public static MyRestClient Create(Uri baseUrl, string profile = null)
        {
            var factory = _lzFactory.Value;
            return factory.Create(baseUrl, profile);
        }

        public static void RegisterProfile(IMyRestClientProfile profile)
        {
            _lzFactory.Value.RegisterProfile(profile);
        }

        public static long MaxAllowedGetUrlLength
        {
            get { return _lzFactory.Value.MaxAllowedGetUrlLength; }
        }

        private class InnerMyRestClientFactory
        {
            private readonly IDictionary<string, IMyRestClientProfile> _profiles;

            private readonly int _maxAllowedGetUrlLength;
            public long MaxAllowedGetUrlLength
            {
                get { return _maxAllowedGetUrlLength; }
            }

            public InnerMyRestClientFactory()
            {
                _profiles = new Dictionary<string, IMyRestClientProfile>();
                //var section = SettingsSection.GetSection();
                if (_maxAllowedGetUrlLength == 0)
                {
                    _maxAllowedGetUrlLength = 1024;
                }
                else
                {
                   // _maxAllowedGetUrlLength = section.MaxAllowedGetUrlLength;
                }
            }

            public void RegisterProfile(IMyRestClientProfile profile)
            {
                if (_profiles.ContainsKey(profile.Name))
                {
                    throw new ArgumentException();
                }
                _profiles.Add(profile.Name, profile);
            }

            public MyRestClient Create(Uri baseUrl, string profileName = null)
            {
                var client = new MyRestClient(baseUrl, MaxAllowedGetUrlLength);
                if (profileName != null)
                {
                    var profile = _profiles[profileName];
                    client.DefaultResourceHandle = profile.DefaultResourceHandle;
                    client.ResourceHandlers = profile.ResourceHandlers;
                    client.OnBeforeRequest = profile.BeforeExecute;
                }
                return client;
            }
        }
    }

}

