using System;
using System.Collections.Generic;
using L5Sharp.Core;

namespace L5Sharp
{
    /// <summary>
    /// Represents a Logix Ladder Logic Routine content. 
    /// </summary>
    public interface ILadderLogic : ILogixContent, IEnumerable<Rung>
    {
        /// <summary>
        /// Gets the number of <see cref="Rung"/> objects in the current <see cref="ILadderLogic"/>.
        /// </summary>
        int Count { get; }
        
        /// <summary>
        /// Gets or sets a <see cref="Rung"/> object at the specified number.
        /// </summary>
        /// <param name="number">The number of the <c>Rung</c> to get.</param>
        /// <remarks>
        /// When setting a <see cref="Rung"/> to the current number, a new object will be created to assign the correct
        /// Rung number property.
        /// </remarks>
        Rung this[int number] { get; set; }

        /// <summary>
        /// Adds the provided <see cref="Rung"/> to the current <see cref="ILadderLogic"/>.
        /// </summary>
        /// <param name="rung">The <see cref="Rung"/> object to add.</param>
        /// <exception cref="ArgumentNullException">When rung is null.</exception>
        /// <remarks>
        /// 
        /// </remarks>
        /// <seealso cref="Add(L5Sharp.Core.NeutralText)"/>
        void Add(Rung rung);

        /// <summary>
        /// Adds a new <see cref="Rung"/> with the provided <see cref="Core.NeutralText"/> to the current <see cref="ILadderLogic"/>.
        /// </summary>
        /// <param name="text">The <see cref="Core.NeutralText"/> that represents the logic for the new <see cref="Rung"/>.</param>
        /// <exception cref="ArgumentNullException">When text is null.</exception>
        /// <seealso cref="Add(L5Sharp.Core.Rung)"/>
        void Add(NeutralText text);

        /// <summary>
        /// Adds the provided collection of <see cref="Rung"/> objects to the current <see cref="ILadderLogic"/>.
        /// </summary>
        /// <param name="rungs">The collection of <see cref="Rung"/> to add.</param>
        /// <exception cref="ArgumentNullException">When rungs is null.</exception>
        void AddRange(IEnumerable<Rung> rungs);

        /// <summary>
        /// Clears all <see cref="Rung"/> objects from the current <see cref="ILadderLogic"/>.
        /// </summary>
        void Clear();

        /// <summary>
        /// Applies the provided comment value to the specified <see cref="Rung"/> number.
        /// </summary>
        /// <param name="number">The number of the <see cref="Rung"/> to comment.</param>
        /// <param name="comment">The string comment to apply to the specified <see cref="Rung"/>.</param>
        /// <exception cref="ArgumentOutOfRangeException">When the number is outside the bounds of the <see cref="ILadderLogic"/> collection.</exception>
        void Comment(int number, string comment);

        /// <summary>
        /// Determines whether the provided  <see cref="Core.NeutralText"/> is contained in the current <see cref="ILadderLogic"/>.
        /// </summary>
        /// <param name="text">The <see cref="Core.NeutralText"/> to locate in the <see cref="ILadderLogic"/>.</param>
        /// <returns>true if the provided <see cref="Core.NeutralText"/> exists in the current <see cref="ILadderLogic"/>; otherwise, false</returns>
        bool ContainsText(NeutralText text);

        /// <summary>
        /// Gets the first occurrence of the <see cref="Rung"/> that matches the provided condition in the
        /// current <see cref="ILadderLogic"/> instance.
        /// </summary>
        /// <param name="match">
        /// The <see cref="Predicate{Rung}"/> delegate that defines the conditions of the object to search for.
        /// </param>
        /// <returns>The first <see cref="Rung"/> that matches the condition of the specified predicate if found; otherwise; null</returns>
        Rung? Find(Predicate<Rung> match);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        IEnumerable<Rung> FindAll(Predicate<Rung> match);

        /// <summary>
        /// Inserts the provided <see cref="Rung"/> to the current <see cref="ILadderLogic"/> at the specified number.
        /// </summary>
        /// <param name="number">The number at which to insert the provided <see cref="Rung"/>.</param>
        /// <param name="rung">The <see cref="Rung"/> instance to insert.</param>
        /// <exception cref="ArgumentOutOfRangeException">When the number is outside the bounds of the <see cref="ILadderLogic"/> collection.</exception>
        /// <seealso cref="Insert(int,L5Sharp.Core.NeutralText)"/>
        void Insert(int number, Rung rung);

        /// <summary>
        /// Inserts a new <see cref="Rung"/> with the provided <see cref="Core.NeutralText"/> to the current <see cref="ILadderLogic"/> at the specified number.
        /// </summary>
        /// <param name="number">The number at which to insert the provided <see cref="Rung"/>.</param>
        /// <param name="text">The <see cref="Core.NeutralText"/> that represents the logic for the new <see cref="Rung"/>.</param>
        /// <exception cref="ArgumentOutOfRangeException">When the number is outside the bounds of the <see cref="ILadderLogic"/> collection.</exception>
        /// <seealso cref="Insert(int,L5Sharp.Core.Rung)"/>
        void Insert(int number, NeutralText text);

        /// <summary>
        /// Removes a <see cref="Rung"/> object with the specified number from the current <see cref="ILadderLogic"/>.
        /// </summary>
        /// <param name="number">The number of the <see cref="Rung"/> to remove.</param>
        /// <returns>true if the <see cref="Rung"/> was found and removed. otherwise; false.</returns>
        /// <exception cref="ArgumentOutOfRangeException">When the number is outside the bounds of the <see cref="ILadderLogic"/> collection.</exception>
        void Remove(int number);

        /// <summary>
        /// Updates the current <see cref="Rung"/> at the specified number with the provided <see cref="Core.NeutralText"/>.
        /// </summary>
        /// <param name="number">The number at which to update the provided <see cref="Rung"/>.</param>
        /// <param name="text">The <see cref="Core.NeutralText"/> that represents the logic for the new <see cref="Rung"/>.</param>
        /// <exception cref="ArgumentOutOfRangeException">When the number is outside the bounds of the <see cref="ILadderLogic"/> collection.</exception>
        void Update(int number, NeutralText text);
    }
}