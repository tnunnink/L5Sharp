using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Components
{
    /// <summary>
    /// 
    /// </summary>
    public class Rll : Routine, IEnumerable<Rung>
    {
        private readonly List<Rung> _rungs = new();

        /// <summary>
        /// 
        /// </summary>
        public Rll() : base(string.Empty)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public Rll(string name, Scope? scope = null, string? description = null) 
            : base(name, scope, description)
        {
        }

        /// <inheritdoc />
        public override RoutineType Type => RoutineType.Rll;

        /// <summary>
        /// Gets the number of <see cref="Rung"/> objects in the current <see cref="Rll"/> routine.
        /// </summary>
        public int Count => _rungs.Count;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="comment"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public void Add(NeutralText text, string? comment = null, RungType? type = null)
        {
            var rung = new Rung
            {
                Number = Count,
                Type = type ?? RungType.Normal,
                Text = text,
                Comment = comment ?? string.Empty
            };

            _rungs.Add(rung);
        }

        /// <summary>
        /// Removes all rungs from the Rll routine. 
        /// </summary>
        public void Clear() => _rungs.Clear();

        /// <summary>
        /// Determines whether any rung in the Rll routine has logic or <see cref="NeutralText"/> equivalent to the
        /// specified text.
        /// </summary>
        /// <param name="text">The <see cref="NeutralText"/> to match on.</param>
        /// <returns><c>true</c> if the routine contains the specified text; otherwise, <c>false</c>.</returns>
        public bool Contains(NeutralText text) => _rungs.Any(r=> r.Text == text);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tagName"></param>
        /// <returns></returns>
        public bool Contains(TagName tagName) => _rungs.Any(r=> r.Text.TagNames().Contains(tagName));


        /// <inheritdoc />
        public IEnumerator<Rung> GetEnumerator() => _rungs.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public int IndexOf(NeutralText text)
        {
            var rung = _rungs.FirstOrDefault(r => r.Text == text);

            return rung?.Number ?? -1;
        }

        /// <summary>
        /// Inserts a rung into the Rll routine at the specified index or number.
        /// </summary>
        /// <param name="number">he zero-based number at which rung should be inserted.</param>
        /// <param name="text">The rung text or logic to insert.</param>
        /// <param name="comment">The optional rung comment of the logic being inserted.</param>
        /// <exception cref="ArgumentOutOfRangeException">number is less than 0. -or- number is greater than Count.</exception>
        public void Insert(int number, NeutralText text, string? comment = null)
        {
            var rung = new Rung
            {
                Number = Count,
                Type = RungType.Normal,
                Text = text,
                Comment = comment ?? string.Empty
            };
            
            _rungs.Insert(number, rung);
        }

        /// <summary>
        /// Removes the rung at the specified rung number of the Rll routine.
        /// </summary>
        /// <param name="number">The zero-based index of the rung to remove.</param>
        /// <exception cref="ArgumentOutOfRangeException">number is less than 0. -or- number is equal to or greater than Count.</exception>
        public void Remove(int number) => _rungs.RemoveAt(number);
    }
}