using System.Collections;
using System.Collections.Generic;
using L5Sharp.Enums;

namespace L5Sharp.Components
{
    /// <summary>
    /// 
    /// </summary>
    public class RllRoutine : Routine, IList<Rung>
    {
        private readonly List<Rung> _rungs = new();

        /// <inheritdoc />
        public override RoutineType Type => RoutineType.Rll;

        /// <summary>
        /// Gets the number of <see cref="Rung"/> objects in the current <see cref="RllRoutine"/> routine.
        /// </summary>
        public int Count => _rungs.Count;

        /// <inheritdoc />
        public bool IsReadOnly => false;

        /// <inheritdoc />
        public Rung this[int index]
        {
            get => _rungs[index];
            set => _rungs[index] = value;
        }

        /// <inheritdoc />
        public void Add(Rung rung) => _rungs.Add(rung);

        /// <inheritdoc />
        public void Clear() => _rungs.Clear();

        /// <inheritdoc />
        public bool Contains(Rung rung) => _rungs.Contains(rung);

        /// <inheritdoc />
        public void CopyTo(Rung[] array, int arrayIndex) => _rungs.CopyTo(array, arrayIndex);

        /// <inheritdoc />
        public IEnumerator<Rung> GetEnumerator() => _rungs.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <inheritdoc />
        public int IndexOf(Rung rung) => _rungs.IndexOf(rung);

        /// <inheritdoc />
        public void Insert(int index, Rung rung) => _rungs.Insert(index, rung);

        /// <inheritdoc />
        public bool Remove(Rung rung) => _rungs.Remove(rung);

        /// <inheritdoc />
        public void RemoveAt(int index) => _rungs.RemoveAt(index);
    }
}