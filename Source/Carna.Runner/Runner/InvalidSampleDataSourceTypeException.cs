// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;

namespace Carna.Runner
{
    /// <summary>
    /// Represents errors that occur when the type of the sample data source is invalid.
    /// </summary>
    public class InvalidSampleDataSourceTypeException : Exception
    {
        /// <summary>
        /// Gets the invalid type of the sample data source.
        /// </summary>
        public Type SampleDataSourceType { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidSampleDataSourceTypeException"/> class
        /// with the specified invalid type of the sample data source.
        /// </summary>
        /// <param name="sampleDataSourceType">The invalid type of the sample data source.</param>
        public InvalidSampleDataSourceTypeException(Type sampleDataSourceType) : this(sampleDataSourceType, $"{sampleDataSourceType} is invalid type of the sample data source")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidSampleDataSourceTypeException"/> class
        /// with the specified invalid type of the sample data source and error message.
        /// </summary>
        /// <param name="sampleDataSourceType">The invalid type of the sample data source.</param>
        /// <param name="message">The message that describes the error.</param>
        public InvalidSampleDataSourceTypeException(Type sampleDataSourceType, string message) : this(sampleDataSourceType, message, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidSampleDataSourceTypeException"/> class
        /// with the specified invalid type of the sample data source, error message and
        /// reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="sampleDataSourceType">The invalid type of the sample data source.</param>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception.
        /// </param>
        public InvalidSampleDataSourceTypeException(Type sampleDataSourceType, string message, Exception innerException) : base(message, innerException)
        {
            SampleDataSourceType = sampleDataSourceType;
        }
    }
}
