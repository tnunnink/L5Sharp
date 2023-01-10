using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using L5Sharp.Core;
using L5Sharp.Enums;

namespace L5Sharp.Components
{
    public class RLL : ILogixComponent, IEnumerable<Rung>
    {
        private readonly List<Rung> _rungs = new();

        /// <inheritdoc />
        public string Name { get; set; } = string.Empty;

        /// <inheritdoc />
        public string Description { get; set; } = string.Empty;

        
        public int Count => _rungs.Count;
        
        public Rung this[int number]
        {
            get => _rungs[number];
            set => _rungs[number] = new Rung(number, value.Type, value.Comment, value.Text);
        }

        /// <inheritdoc />
        public void Clear() => _rungs.Clear();

        /// <inheritdoc />
        public bool ContainsText(NeutralText text) => _rungs.Any(r => r.Text == text);

        /// <inheritdoc />
        public Rung? Find(Predicate<Rung> match) => match is not null
            ? _rungs.FirstOrDefault(r => match(r))
            : throw new ArgumentNullException(nameof(match));

        /// <inheritdoc />
        public IEnumerable<Rung> FindAll(Predicate<Rung> match) => match is not null
            ? _rungs.Where(r => match(r))
            : throw new ArgumentNullException(nameof(match));

        /// <inheritdoc />
        public void Add(Rung rung)
        {
            if (rung is null)
                throw new ArgumentNullException(nameof(rung));

            _rungs.Add(new Rung(Count, rung.Type, rung.Comment, rung.Text));
        }

        /// <inheritdoc />
        public void Add(NeutralText text)
        {
            if (text is null)
                throw new ArgumentNullException(nameof(text));

            _rungs.Add(new Rung(Count, RungType.Normal, string.Empty, text));
        }

        /// <inheritdoc />
        public void AddRange(IEnumerable<Rung> rungs)
        {
            if (rungs is null)
                throw new ArgumentNullException(nameof(rungs));

            var collection = rungs.ToArray();

            for (var i = 0; i < collection.Length; i++)
                _rungs.Add(new Rung(i, collection[i].Type, collection[i].Comment, collection[i].Text));
        }

        /// <inheritdoc />
        public void Insert(int number, Rung rung)
        {
            if (rung is null)
                throw new ArgumentNullException(nameof(rung));

            if (number < 0 || number >= Count)
                throw new ArgumentOutOfRangeException(nameof(number),
                    "Number is outside the bounds of the content collection");

            _rungs.Insert(number, new Rung(number, rung.Type, rung.Comment, rung.Text));
        }

        /// <inheritdoc />
        public void Insert(int number, NeutralText text)
        {
            if (text is null)
                throw new ArgumentNullException(nameof(text));

            if (number < 0 || number >= Count)
                throw new ArgumentOutOfRangeException(nameof(number),
                    "Number is outside the bounds of the content collection");

            _rungs.Insert(number, new Rung(number, RungType.Normal, string.Empty, text));
        }

        /// <inheritdoc />
        public void Remove(int number)
        {
            if (number < 0 || number >= Count)
                throw new ArgumentOutOfRangeException(nameof(number),
                    "Number is outside the bounds of the content collection");

            _rungs.RemoveAt(number);
        }

        /// <inheritdoc />
        public void Update(int number, NeutralText text)
        {
            if (number < 0 || number >= Count)
                throw new ArgumentOutOfRangeException(nameof(number),
                    "Number is outside the bounds of the content collection");

            var current = _rungs[number];

            _rungs[number] = new Rung(number, current.Type, current.Comment, text);
        }

        /// <inheritdoc />
        public void Comment(int number, string comment)
        {
            if (number < 0 || number >= Count)
                throw new ArgumentOutOfRangeException(nameof(number), "Number is outside the bounds of the collection");

            var current = _rungs[number];

            _rungs[number] = new Rung(current.Number, current.Type, comment, current.Text);
        }

        /// <inheritdoc />
        public IEnumerator<Rung> GetEnumerator() => _rungs.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}