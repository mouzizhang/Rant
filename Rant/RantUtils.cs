﻿using System;
using System.Collections.Generic;
using System.Linq;

using Rant.Core.Framework;
using Rant.Metadata;

namespace Rant
{
    /// <summary>
    /// Contains miscellaneous utility methods that provide information about the Rant engine.
    /// </summary>
    public static class RantUtils
    {
        /// <summary>
        /// Determines whether a function with the specified name is defined in the current engine version.
        /// </summary>
        /// <param name="functionName">The name of the function to search for. Argument is not case-sensitive.</param>
        /// <returns></returns>
        public static bool FunctionExists(string functionName)
        {
            return RantFunctionRegistry.FunctionExists(functionName);
        }

        /// <summary>
        /// Returns the function with the specified name. The return value will be null if the function is not found.
        /// </summary>
        /// <param name="functionName">The name of the function to retrieve.</param>
        /// <returns></returns>
        public static IRantFunctionGroup GetFunction(string functionName) => RantFunctionRegistry.GetFunctionGroup(functionName);

        /// <summary>
        /// Enumerates the names of all available Rant functions.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<string> GetFunctionNames() => RantFunctionRegistry.GetFunctionNames();

        /// <summary>
        /// Enumerates all function names and their aliases.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<string> GetFunctionNamesAndAliases() => RantFunctionRegistry.GetFunctionNamesAndAliases(); 

        /// <summary>
        /// Enumerates the available functions.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IRantFunctionGroup> GetFunctions() => RantFunctionRegistry.GetFunctions();

        /// <summary>
        /// Returns the description for the function with the specified name.
        /// </summary>
        /// <param name="functionName">The name of the function to get the description for.</param>
        /// <param name="argc">The number of arguments in the overload to get the description for.</param>
        /// <returns></returns>
        public static string GetFunctionDescription(string functionName, int argc) => RantFunctionRegistry.GetFunctionDescription(functionName, argc);
        
        /// <summary>
        /// Enumerates the aliases assigned to the specified function name.
        /// </summary>
        /// <param name="functionName">The function name to retrieve aliases for.</param>
        /// <returns></returns>
        public static IEnumerable<string> GetFunctionAliases(string functionName) => RantFunctionRegistry.GetAliases(functionName);
		
    }
}
