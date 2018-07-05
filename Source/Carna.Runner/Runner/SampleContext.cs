// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Carna.Runner
{
    /// <summary>
    /// Represents a context of a sample.
    /// </summary>
    public class SampleContext
    {
        /// <summary>
        /// Gets a description of a sample.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Gets items of a sample.
        /// </summary>
        public object[] Data { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SampleContext"/> class
        /// with the specified description and items.
        /// </summary>
        /// <param name="description">The description of a sample.</param>
        /// <param name="items">The items of a sample.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="items"/> is <c>null</c>.
        /// </exception>
        public SampleContext(string description, IEnumerable<Item> items)
        {
            var sampleItems = items?.ToList() ?? new List<Item>();
            Description = description ?? string.Join(", ", sampleItems.Select(item => item.ToString()));
            Data = sampleItems.Select(item => item.Value).ToArray();
        }

        /// <summary>
        /// Represents an item of a sample context.
        /// </summary>
        public class Item
        {
            /// <summary>
            /// Gets a name of an item.
            /// </summary>
            public string Name { get; }

            /// <summary>
            /// Gets a value of an item.
            /// </summary>
            public object Value { get; set; }

            /// <summary>
            /// Initializes a new instance of the <see cref="SampleContext"/> class
            /// with the specified name.
            /// </summary>
            /// <param name="name">The name of an item.</param>
            public Item(string name)
            {
                Name = name;
            }

            /// <summary>
            /// Returns a string that represents the current object.
            /// </summary>
            /// <returns>A string that represents the current object.</returns>
            public override string ToString()
                => $"{Name}={ResolveValue()}";

            private object ResolveValue()
                => Value == null || !typeof(Array).GetTypeInfo().IsAssignableFrom(Value.GetType().GetTypeInfo()) ? Value :
                    $"[{string.Join(",", ((Array)Value).OfType<object>())}]";
        }
    }
}
