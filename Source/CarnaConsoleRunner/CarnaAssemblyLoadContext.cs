// Copyright (C) 2020 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Reflection;
using System.Runtime.Loader;

namespace Carna.ConsoleRunner
{
    internal class CarnaAssemblyLoadContext : AssemblyLoadContext
    {
        private readonly AssemblyDependencyResolver assemblyDependencyResolver;

        public CarnaAssemblyLoadContext(string assemblyPath)
        {
            assemblyDependencyResolver = new AssemblyDependencyResolver(assemblyPath);
        }

        protected override Assembly Load(AssemblyName assemblyName)
        {
            var assemblyPath = assemblyDependencyResolver.ResolveAssemblyToPath(assemblyName);
            return assemblyPath == null ? null : LoadFromAssemblyPath(assemblyPath);
        }

        protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
        {
            var unmanagedDllPath = assemblyDependencyResolver.ResolveUnmanagedDllToPath(unmanagedDllName);
            return unmanagedDllPath == null ? IntPtr.Zero : LoadUnmanagedDllFromPath(unmanagedDllPath);
        }
    }
}
