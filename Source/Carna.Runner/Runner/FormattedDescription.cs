// Copyright (C) 2017 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Collections.Generic;
using System.Linq;

namespace Carna.Runner
{
    /// <summary>
    /// Represents a formatted description of a fixture.
    /// </summary>
    public class FormattedDescription
    {
        /// <summary>
        /// Gets or sets lines of a description.
        /// </summary>
        public IEnumerable<string> Lines { get; set; } = Enumerable.Empty<string>();

        /// <summary>
        /// Gets or sets a first line indent.
        /// </summary>
        public string FirstLineIndent { get; set; } = string.Empty;

        /// <summary>
        /// Gets or set a line indent.
        /// </summary>
        public string LineIndent { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets containing formatted description items.
        /// </summary>
        public IEnumerable<FormattedDescription> Items { get; set; } = Enumerable.Empty<FormattedDescription>();

        /// <summary>
        /// Joins lines of a description with <see cref="Environment.NewLine"/> and the specified indent.
        /// </summary>
        /// <param name="indent">The indent of a line.</param>
        /// <returns>
        /// The string representation of the lines of a description that is joined
        /// with <see cref="Environment.NewLine"/> and the specified indent.
        /// </returns>
        public string JoinLines(string indent = null)
        {
            if (!Lines.Any()) { return string.Empty; }

            var lineItems = new List<string>();
            lineItems.Add($"{indent}{FirstLineIndent}{Lines.FirstOrDefault()}");
            lineItems.AddRange(Lines.Skip(1).Select(line => $"{LineIndent}{line}"));
            return string.Join($"{Environment.NewLine}{indent}", lineItems);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>The formatted description of a fixture.</returns>
        public override string ToString()
        {
            var lines = new List<string>();
            if (Lines.Any()) { lines.Add(JoinLines()); }
            lines.AddRange(Items.Select(item => item.JoinLines("  ")));
            return string.Join(Environment.NewLine, lines);
        }
    }
}
