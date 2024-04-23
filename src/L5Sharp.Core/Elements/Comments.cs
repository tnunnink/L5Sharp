using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace L5Sharp.Core;

/*<Comments>
    <Comment Operand=".0">
    <![CDATA[This is a test]]>
    </Comment>
</Comments>*/

/// <summary>
/// An element wrapping the <c>Comments</c> child element of a Tag component. This class implements
/// <see cref="IDictionary{TKey,TValue}"/> in order to retrieve and mutate a given tag's comment collection.
/// </summary>
public class Comments : LogixElement, IEnumerable<Comment>
{
    /// <summary>
    /// Creates a new empty <see cref="Comments"/> collection.
    /// </summary>
    public Comments() : base(L5XName.Comments)
    {
    }

    /// <summary>
    /// Creates a new <see cref="Comments"/> initialized with the provided <see cref="XElement"/>.
    /// </summary>
    /// <param name="element">The <see cref="XElement"/> to initialize the type with.</param>
    /// <exception cref="ArgumentNullException"><c>element</c> is null.</exception>
    public Comments(XElement element) : base(element)
    {
    }
    
    /// <inheritdoc />
    public IEnumerator<Comment> GetEnumerator() =>
        Element.Elements(L5XName.Comment).Select(e => new Comment(e)).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}