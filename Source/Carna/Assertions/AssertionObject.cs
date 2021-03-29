// Copyright (C) 2019-2021 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Carna.Assertions
{
    /// <summary>
    /// Represents an object that asserts properties that are specified by the <see cref="AssertionPropertyAttribute"/>.
    /// </summary>
    public class AssertionObject
    {
        private static readonly BindingFlags AssertionPropertyBindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString() => ToString(new StringOptions("{", "}"));

        /// <summary>
        /// Returns a string that represents the current object with the specified options.
        /// </summary>
        /// <param name="options">The options to represent the current object.</param>
        /// <returns>A string that represents the current object.</returns>
        public string ToString(StringOptions options)
        {
            var assertionProperties = string.Join(
                options.Separator,
                GetAssertionProperties().Select(p =>
                {
                    var assertionObject = p.Value as AssertionObject;
                    return $"{p.Description}: {(assertionObject == null ? p.Value ?? "<null>" : assertionObject.ToString(options.ToNextLevel()))}";
                })
            );
            return $"{options.Prefix}{assertionProperties}{options.Suffix}";
        }

        /// <summary>
        /// Returns a string that represents the current object for the fixture description.
        /// </summary>
        /// <returns>A string that represents the current object for the fixture description.</returns>
        public string ToDescription() => ToString(
            new StringOptions(
                level => $"{Environment.NewLine}{new string(' ', (level + 1) * 2)}",
                null,
                level => $"{Environment.NewLine}{new string(' ', (level + 1) * 2)}"
            )
        );

        /// <summary>
        /// Determines whether two specified assertion objects have the same value.
        /// </summary>
        /// <param name="actual">The first assertion object that is actual value to compare, or null.</param>
        /// <param name="expected">The second assertion object that is expected value to compare, or null.</param>
        /// <returns>
        /// <c>true</c> if the value of <paramref name="actual">actual</paramref> is the same as the value of <paramref name="expected">expected</paramref>;
        /// otherwise, <c>false</c>.
        /// </returns>
        public static bool operator ==(AssertionObject actual, AssertionObject expected)
        {
            if (ReferenceEquals(actual, expected)) return true;
            if (ReferenceEquals(actual, null) || ReferenceEquals(expected, null)) return false;

            return actual.GetAssertionProperties()
                .All(p =>
                {
                    var actualValue = p.Value;
                    var expectedValue = expected.GetType().GetProperty(p.Name, AssertionPropertyBindingFlags)?.GetValue(expected);
                    return (expectedValue as IAssertionProperty)?.Assert(actualValue as IAssertionProperty) ?? Equals(actualValue, expectedValue);
                });
        }

        /// <summary>
        /// Determines whether two specified assertion objects have different values.
        /// </summary>
        /// <param name="actual">The first assertion object to compare, or null.</param>
        /// <param name="expected">The second assertion object to compare, or null.</param>
        /// <returns>
        /// <c>true</c> if the value of <paramref name="actual">actual</paramref> is different from the value of <paramref name="expected">expected</paramref>;
        /// otherwise, <c>false</c>.
        /// </returns>
        public static bool operator !=(AssertionObject actual, AssertionObject expected) => !(actual == expected);

        /// <summary>
        /// Determines whether this instance and a specified object, which must also be a <see cref="AssertionObject"></see> object, have the same value.
        /// </summary>
        /// <param name="obj">The string to compare to this instance.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="obj">obj</paramref> is a <see cref="AssertionObject"></see> and its value is the same as this instance; otherwise, <c>false</c>.
        /// If <paramref name="obj">obj</paramref> is null, the method returns <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (GetType() != obj.GetType()) return false;
            return this == (AssertionObject)obj;
        }

        /// <summary>
        /// Gets the hash code for this assertion object.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
            => GetAssertionProperties()
                .Select(p => p.Value?.GetHashCode() ?? 0)
                .Aggregate(17, (hashCode, h) => hashCode ^ h);

        /// <summary>
        /// Gets properties specified by the <see cref="AssertionPropertyAttribute"/>.
        /// </summary>
        /// <returns>The properties specified by the <see cref="AssertionPropertyAttribute"/>.</returns>
        protected IEnumerable<AssertionPropertyContext> GetAssertionProperties()
            => GetType().GetProperties(AssertionPropertyBindingFlags)
                .Where(p => p.GetCustomAttribute<AssertionPropertyAttribute>() != null || typeof(IAssertionProperty).IsAssignableFrom(p.PropertyType))
                .Select(p => new AssertionPropertyContext(p.Name, p.GetValue(this), p.GetCustomAttribute<AssertionPropertyAttribute>()?.Description ?? p.Name));

        /// <summary>
        /// Represents options to represent an assertion object.
        /// </summary>
        public class StringOptions
        {
            /// <summary>
            /// Gets a prefix of a string expression of an assertion object.
            /// </summary>
            public string Prefix => prefixGenerator?.Invoke(Level);

            /// <summary>
            /// Gets a suffix of a string expression of an assertion object.
            /// </summary>
            public string Suffix => suffixGenerator?.Invoke(Level);

            /// <summary>
            /// Gets a separator to separate of each property.
            /// </summary>
            public string Separator => separatorGenerator?.Invoke(Level);

            /// <summary>
            /// Gets a nested level of an assertion object.
            /// </summary>
            public int Level { get; }

            private readonly Func<int, string> prefixGenerator;
            private readonly Func<int, string> suffixGenerator;
            private readonly Func<int, string> separatorGenerator;

            /// <summary>
            /// Initializes a new instance of the <see cref="StringOptions"/> class
            /// with the specified separator.
            /// </summary>
            /// <param name="separator">The separator to separate of each property.</param>
            public StringOptions(string separator) : this(null, null, separator)
            {
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="StringOptions"/> class
            /// with the specified prefix and suffix.
            /// </summary>
            /// <param name="prefix">The prefix of a string expression of an assertion object.</param>
            /// <param name="suffix">The suffix of a string expression of an assertion object.</param>
            public StringOptions(string prefix, string suffix) : this(prefix, suffix, ", ")
            {
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="StringOptions"/> class
            /// with the prefix, suffix, and separator.
            /// </summary>
            /// <param name="prefix">The prefix of a string expression of an assertion object.</param>
            /// <param name="suffix">The suffix of a string expression of an assertion object.</param>
            /// <param name="separator">The separator to separate of each property.</param>
            public StringOptions(string prefix, string suffix, string separator) : this(prefix, suffix, separator, 0)
            {
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="StringOptions"/> class
            /// with the prefix, suffix, separator, and nested level.
            /// </summary>
            /// <param name="prefix">The prefix of a string expression of an assertion object.</param>
            /// <param name="suffix">The suffix of a string expression of an assertion object.</param>
            /// <param name="separator">The separator to separate of each property.</param>
            /// <param name="level">The nested level of an assertion object.</param>
            public StringOptions(string prefix, string suffix, string separator, int level) : this(l => prefix, l => suffix, l => separator, level)
            {
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="StringOptions"/> class
            /// with the separator generator.
            /// </summary>
            /// <param name="separatorGenerator">The generator that generates a separator to separate of each property.</param>
            public StringOptions(Func<int, string> separatorGenerator) : this(null, null, separatorGenerator)
            {
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="StringOptions"/> class
            /// with the prefix generator and suffix generator.
            /// </summary>
            /// <param name="prefixGenerator">The generator that generates a prefix of a string expression of an assertion object.</param>
            /// <param name="suffixGenerator">The generator that generates a suffix of a string expression of an assertion object.</param>
            public StringOptions(Func<int, string> prefixGenerator, Func<int, string> suffixGenerator) : this(prefixGenerator, suffixGenerator, null)
            {
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="StringOptions"/> class
            /// with the prefix generator, suffix generator, and separator generator.
            /// </summary>
            /// <param name="prefixGenerator">The generator that generates a prefix of a string expression of an assertion object.</param>
            /// <param name="suffixGenerator">The generator that generates a suffix of a string expression of an assertion object.</param>
            /// <param name="separatorGenerator">The generator that generates a separator to separate of each property.</param>
            public StringOptions(Func<int, string> prefixGenerator, Func<int, string> suffixGenerator, Func<int, string> separatorGenerator) : this(prefixGenerator, suffixGenerator, separatorGenerator, 0)
            {
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="StringOptions"/> class
            /// with the prefix generator, suffix generator, separator generator, and the nested level.
            /// </summary>
            /// <param name="prefixGenerator">The generator that generates a prefix of a string expression of an assertion object.</param>
            /// <param name="suffixGenerator">The generator that generates a suffix of a string expression of an assertion object.</param>
            /// <param name="separatorGenerator">The generator that generates a separator to separate of each property.</param>
            /// <param name="level">The nested level of an assertion object.</param>
            public StringOptions(Func<int, string> prefixGenerator, Func<int, string> suffixGenerator, Func<int, string> separatorGenerator, int level)
            {
                this.prefixGenerator = prefixGenerator;
                this.suffixGenerator = suffixGenerator;
                this.separatorGenerator = separatorGenerator;
                Level = level;
            }

            /// <summary>
            /// Gets the <see cref="StringOptions"/> that has the next nested level of this options.
            /// </summary>
            /// <returns>The <see cref="StringOptions"/> that has the next nested level of this options.</returns>
            public StringOptions ToNextLevel() => new StringOptions(prefixGenerator, suffixGenerator, separatorGenerator, Level + 1);
        }

        /// <summary>
        /// Represents a context of an assertion property.
        /// </summary>
        protected class AssertionPropertyContext
        {
            /// <summary>
            /// Gets a name of an assertion property.
            /// </summary>
            public string Name { get; }

            /// <summary>
            /// Gets a value of an assertion property.
            /// </summary>
            public object Value { get; }

            /// <summary>
            /// Gets a description of an assertion property.
            /// </summary>
            public string Description { get; }

            /// <summary>
            /// Initializes a new instance of the <see cref="AssertionPropertyContext"/> class
            /// with the specified name, value, and description.
            /// </summary>
            /// <param name="name">The name of the assertion property.</param>
            /// <param name="value">The value of the assertion property.</param>
            /// <param name="description">The description of the assertion property.</param>
            public AssertionPropertyContext(string name, object value, string description)
            {
                Name = name;
                Value = value;
                Description = description;
            }
        }
    }
}
