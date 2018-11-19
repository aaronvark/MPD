using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IServiceManager {
	bool RequestService<T>( out T service ) where T : class;
}

public class ServiceManager : IServiceManager {
	private static ServiceManager instance;
	public static IServiceManager Singleton {
		get {
			if ( instance == null ) {
				instance = new ServiceManager();
				instance.InitializeCoreServices();
			}
			return instance;
		}
	}

	private Dictionary<System.Type, object> serviceDictionary = new Dictionary<System.Type, object>();

	private void InitializeCoreServices() {
		//here we can control spawning core services in the correct order
		// this means constructors of systems relying on each other can find their dependencies
		//  or we could inject them here (Dependency Injection)
		serviceDictionary.Add( typeof(IInputSystem), InputManager.CreateInstance() );
	}

	public bool RequestService<T>( out T service ) where T : class {
		try {
			service = (T)serviceDictionary[typeof(T)];
			return true;
		}
		catch {
			service = null;
			return false;
		}
	}
}
