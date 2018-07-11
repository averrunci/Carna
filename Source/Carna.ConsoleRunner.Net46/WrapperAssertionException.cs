// Copyright (C) 2018 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Runtime.Serialization;

namespace Carna.ConsoleRunner
{
    [Serializable]
    internal class WrapperAssertionException : Exception
    {
        public override string StackTrace { get; }

        private string ExceptionTypeName { get; }

        public WrapperAssertionException(string exceptionTypeName, string message, string stackTrace) : base(message)
        {
            ExceptionTypeName = exceptionTypeName;
            StackTrace = stackTrace;
        }

        public WrapperAssertionException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            ExceptionTypeName = info.GetString(nameof(ExceptionTypeName));
            StackTrace = info.GetString(nameof(StackTrace));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue(nameof(ExceptionTypeName), ExceptionTypeName);
            info.AddValue(nameof(StackTrace), StackTrace);
        }

        public override string ToString() => $"{ExceptionTypeName}: {Message}{Environment.NewLine}{StackTrace}";
    }
}
