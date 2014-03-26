﻿using System;
using Dev2.Runtime.ServiceModel.Data;

namespace Dev2.Runtime.ServiceModel.Esb.Brokers.Plugin
{
    /// <summary>
    /// Used to execute plugins properly ;)
    /// INFO : http://stackoverflow.com/questions/2008691/pass-and-execute-delegate-in-separate-appdomain
    /// </summary>
    public static class PluginServiceExecutionFactory
    {
        #region Private Methods

        private static IRuntime CreateInvokeAppDomain(out AppDomain childDomain)
        {
            // Construct and initialize settings for a second AppDomain.
            AppDomainSetup domainSetup = new AppDomainSetup
            {
                ApplicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
                ConfigurationFile = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile,
                ApplicationName = AppDomain.CurrentDomain.SetupInformation.ApplicationName,
                LoaderOptimization = LoaderOptimization.MultiDomainHost
            };

            // Create the child AppDomain used for the service tool at runtime.
            childDomain = AppDomain.CreateDomain(Guid.NewGuid().ToString(), null, domainSetup);

            // Create an instance of the runtime in the second AppDomain. 
            // A proxy to the object is returned.
            IRuntime runtime = (PluginRuntimeHandler)childDomain.CreateInstanceAndUnwrap(typeof(PluginRuntimeHandler).Assembly.FullName, typeof(PluginRuntimeHandler).FullName);

            return runtime;
        }

        #endregion

        #region Public Interface

        public static object InvokePlugin(PluginInvokeArgs args)
        {
            AppDomain childDomain = null;

            try
            {
                var runtime = CreateInvokeAppDomain(out childDomain);

                // start the runtime.  call will marshal into the child runtime app domain
                return runtime.Run(args);
            }
            finally
            {
                if(childDomain != null)
                {
                    AppDomain.Unload(childDomain);
                }
            }
        }

        /// <summary>
        /// Gets the methods.
        /// </summary>
        /// <param name="assemblyLocation">The assembly location.</param>
        /// <param name="assemblyName">Name of the assembly.</param>
        /// <param name="fullName">The full name.</param>
        /// <returns></returns>
        public static ServiceMethodList GetMethods(string assemblyLocation, string assemblyName, string fullName)
        {
            AppDomain childDomain = null;
            try
            {
                var runtime = CreateInvokeAppDomain(out childDomain);

                // start the runtime.  call will marshal into the child runtime app domain
                return runtime.ListMethods(assemblyLocation, assemblyName, fullName);
            }
            finally
            {
                if(childDomain != null)
                {
                    AppDomain.Unload(childDomain);
                }
            }
        }

        /// <summary>
        /// Validates the plugin.
        /// </summary>
        /// <param name="toLoad">The automatic load.</param>
        /// <returns></returns>
        public static string ValidatePlugin(string toLoad)
        {
            AppDomain childDomain = null;
            try
            {
                var runtime = CreateInvokeAppDomain(out childDomain);

                // start the runtime.  call will marshal into the child runtime app domain
                return runtime.ValidatePlugin(toLoad);
            }
            finally
            {
                if(childDomain != null)
                {
                    AppDomain.Unload(childDomain);
                }
            }
        }

        public static NamespaceList GetNamespaces(PluginSource pluginSource)
        {
            AppDomain childDomain = null;
            try
            {
                var runtime = CreateInvokeAppDomain(out childDomain);

                // start the runtime.  call will marshal into the child runtime app domain
                return runtime.FetchNamespaceListObject(pluginSource);
            }
            finally
            {
                if(childDomain != null)
                {
                    AppDomain.Unload(childDomain);
                }
            }
        }

        #endregion

    }
}
