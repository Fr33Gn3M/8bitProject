namespace TH.ServerFramework.ServiceCatalog
{
    using System;
    using System.Collections.Concurrent;
    using System.Runtime.CompilerServices;

    public class ServiceLocker
    {
        private readonly ServiceConfigurationBase _serviceConfiguration;
        private static readonly ConcurrentDictionary<Guid, object> _serviceLockerObjects = new ConcurrentDictionary<Guid, object>();

        public ServiceLocker(ServiceConfigurationBase serviceConfiguration)
        {
            this._serviceConfiguration = serviceConfiguration;
        }

        public void LockAndExecute(Action act)
        {
            if (!_serviceLockerObjects.ContainsKey(this._serviceConfiguration.UniqueId))
            {
                _serviceLockerObjects.TryAdd(this._serviceConfiguration.UniqueId, RuntimeHelpers.GetObjectValue(new object()));
            }
            object locker = RuntimeHelpers.GetObjectValue(_serviceLockerObjects[this._serviceConfiguration.UniqueId]);
            lock (locker)
            {
                act.Invoke();
                object t;
                _serviceLockerObjects.TryRemove(_serviceConfiguration.UniqueId,out t);
            }
        }
    }
}

