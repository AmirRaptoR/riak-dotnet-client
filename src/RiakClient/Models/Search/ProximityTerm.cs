// <copyright file="ProximityTerm.cs" company="Basho Technologies, Inc.">
// Copyright 2011 - OJ Reeves & Jeremiah Peschka
// Copyright 2014 - Basho Technologies, Inc.
//
// This file is provided to you under the Apache License,
// Version 2.0 (the "License"); you may not use this file
// except in compliance with the License.  You may obtain
// a copy of the License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing,
// software distributed under the License is distributed on an
// "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
// KIND, either express or implied.  See the License for the
// specific language governing permissions and limitations
// under the License.
// </copyright>

namespace RiakClient.Models.Search
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a Lucene "proximity" search term.
    /// </summary>
    public class ProximityTerm : Term
    {
        private readonly List<Token> words;
        private readonly double proximity;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProximityTerm"/> class.
        /// </summary>
        /// <param name="search">The fluent search to add this term to.</param>
        /// <param name="field">The field to search.</param>
        /// <param name="proximity">The maximum distance the words can be from each other.</param>
        /// <param name="words">The set of words to find within a certain distance of each other.</param>
        internal ProximityTerm(RiakFluentSearch search, string field, double proximity, params string[] words)
            : base(search, field)
        {
            this.words = new List<Token>(words.Select(Token.Is));
            this.proximity = proximity;
        }

        /// <summary>
        /// Returns the term in a Lucene query string format.
        /// </summary>
        /// <returns>
        /// A string that represents the query term.</returns>
        public override string ToString()
        {
            return Prefix() + Field() + "\"" + string.Join(" ", words.Select(w => w.ToString()).ToArray()) + "\"~" + proximity + Suffix();
        }
    }
}
