using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Utilities;

namespace L5Sharp.Common;

[L5XType(L5XName.Rung)]
[L5XType(L5XName.Line)]
public class CodeElement : LogixElement
{
    public CodeElement(XElement element) : base(element)
    {
    }

    public string Element { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public string Container => base.Element.Ancestors(L5XName.Program).FirstOrDefault()?.LogixName() ??
        throw base.Element.L5XError(L5XName.Program);

    /// <summary>
    /// 
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    public string Routine => base.Element.Ancestors(L5XName.Routine).FirstOrDefault()?.LogixName() ??
                             throw base.Element.L5XError(L5XName.Routine);

    public string Location { get; set; }
    public IEnumerable<string> References { get; set; }
}