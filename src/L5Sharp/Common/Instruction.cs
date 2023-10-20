using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using JetBrains.Annotations;
using L5Sharp.Utilities;

// ReSharper disable IdentifierTypo
// ReSharper disable InconsistentNaming
// ReSharper disable CommentTypo

namespace L5Sharp.Common;

/// <summary>
/// A class representing a instruction definition and ...
/// </summary>
[PublicAPI]
public sealed class Instruction
{
    /// <summary>
    /// Pattern for identifying any instruction and the contents of it's signature. This expression should
    /// capture everything enclosed or between the instruction parentheses. This includes nested parenthesis.
    /// This works on the assumption that the text has balanced opening/closing parentheses.
    /// </summary>
    public const string Pattern = @"[A-Za-z_]\w{1,39}\((?>\((?<c>)|[^()]+|\)(?<-c>))*(?(c)(?!))\)";

    /// <summary>
    /// Pattern finds all text prior to opening parentheses, which is the instruction name or key that identifies
    /// the instruction.
    /// </summary>
    private const string KeyPattern = @"[A-Za-z_]\w{1,39}(?=\()";
    
    /// <summary>
    /// Captures all content within parentheses, including outer parentheses and nested parentheses, assuming they
    /// are balanced (number of opening equals number of closing).
    /// </summary>
    private const string SignaturePattern = @"\((?>\((?<c>)|[^()]+|\)(?<-c>))*(?(c)(?!))\)";
    
    /// <summary>
    /// The regex pattern for Logix tag names without starting and ending anchors.
    /// This pattern also includes a negative lookahead for removing text prior to parenthesis (i.e. instruction keys)
    /// Use this pattern for tag names within text, such as longer
    /// </summary>
    private const string TagNamePattern =
        @"(?!\w*\()[A-Za-z_][\w+:]{1,39}(?:(?:\[\d+\]|\[\d+,\d+\]|\[\d+,\d+,\d+\])?(?:\.[A-Za-z_]\w{1,39})?)+(?:\.[0-9][0-9]?)?";

    /// <summary>
    /// Creates a new <see cref="Instruction"/> with the provided string key and regex signature pattern.
    /// </summary>
    /// <param name="key">The key identifier of the instruction.</param>
    /// <param name="arguments"></param>
    public Instruction(string key, params Argument[] arguments)
    {
        if (string.IsNullOrEmpty(key))
            throw new ArgumentException("Instruction key cannot be null or empty.", nameof(key));
        Key = key;
        Arguments = arguments;
    }

    /// <summary>
    /// The collection of <see cref="Argument"/> values for the instruction instance.
    /// </summary>
    public IEnumerable<Argument> Arguments { get; }

    /// <summary>
    /// Indicates whether this <c>Instruction</c> is one that calls or references a <c>Routine</c> component by name.
    /// </summary>
    public bool CallsRoutine => this == JSR || this == JXR || this == SFR || this == SFP || this == FOR;

    /// <summary>
    /// Indicates whether this <c>Instruction</c> is one that calls or references a <c>Task</c> component by name.
    /// </summary>
    public bool CallsTask => this == EVENT;

    /// <summary>
    /// Indicates whether the instruction is conditional or evaluates a certain condition to be true or false, in which
    /// it directs the flow of the program to a different location.
    /// </summary>
    /// <value><c>true</c> if the instruction is conditional; Otherwise, <c>false</c>.</value>
    /// <remarks>An example of a condition instruction is an <see cref="XIC"/>.</remarks>
    public bool IsConditional => ConditionalKeys().Contains(Key);

    /// <summary>
    /// The unique identifier of the instruction instance.
    /// </summary>
    /// <value>
    /// A <see cref="string"/> containing the short hand instruction key identifier (e.g. XIC/OTe).
    /// For AOI this is the name of the component.
    /// </value>
    public string Key { get; }

    /// <summary>
    /// The signature or valid regex pattern of the instruction neutral text.
    /// </summary>
    /// <remarks>
    /// This format string represent a regex capture pattern to help parse <see cref="NeutralText"/> for the instruction.
    /// </remarks>
    public string Signature => $"({string.Join(',', Arguments.AsEnumerable())})";

    /// <summary>
    /// The <see cref="NeutralText"/> representation of the instruction.
    /// </summary>
    /// <value>A <see cref="NeutralText"/> instance that represents the instruction in Logix neutral text format.</value>
    public NeutralText Text => new($"{Key}{Signature}");
    
    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true;

        return obj switch
        {
            Instruction other => Key.IsEquivalent(other.Key),
            string key => Key.IsEquivalent(key),
            _ => false
        };
    }

    /// <inheritdoc />
    public override int GetHashCode() => Key.GetHashCode();
    
    /// <summary>
    /// Determines the the provided <see cref="Instruction"/> has the same.
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool IsEquivalent(Instruction? other) => other is not null && Text.Equals(other.Text);

    /// <summary>
    /// Creates a <see cref="Instruction"/> that represents the current instruction with the provided
    /// argument values.
    /// </summary>
    /// <param name="arguments">The collection of arguments that are provided to the instruction signature.</param>
    /// <returns>A new <see cref="Instruction"/> complete with the provided <see cref="Argument"/> values.</returns>
    public Instruction Of(params Argument[] arguments) => new(Key, arguments);

    /// <summary>
    /// Parses the provided string neutral text into a <see cref="Instruction"/> instance.
    /// </summary>
    /// <param name="text">The neutral text to parse.</param>
    /// <returns>A <see cref="Instruction"/> object representing the parsed text.</returns>
    /// <exception cref="ArgumentException"><c>text</c> is null or empty.</exception>
    public static Instruction Parse(string text)
    {
        if (string.IsNullOrEmpty(text))
            throw new ArgumentException("Instruction text cannot be null or empty.", nameof(text));

        if (!Regex.IsMatch(text, Pattern))
            throw new FormatException("Instruction text must be in the format of 'KEY(ARGUMENTS)'.");

        var key = text[..text.IndexOf('(')];
        var signature = Regex.Match(text, SignaturePattern).Value[1..^1];
        var arguments = Regex.Split(signature, ",(?![^[]*])").Select(Argument.Parse).ToArray();
        
        return new Instruction(key, arguments);
    }

    /// <inheritdoc />
    public override string ToString() => Key;

    /// <summary>
    /// Returns a collection of all known or defined <see cref="Instruction"/> instances.
    /// </summary>
    /// <returns>
    /// A <see cref="IEnumerable{T}"/> containing all known <see cref="Instruction"/> instances with no arguments.
    /// </returns>
    public static IEnumerable<string> Keys() => _known.Value.Keys.AsEnumerable();

    /// <summary>
    /// Returns a collection of all known or defined <see cref="Instruction"/> instances.
    /// </summary>
    /// <returns>
    /// A <see cref="IEnumerable{T}"/> containing all known <see cref="Instruction"/> instances with no arguments.
    /// </returns>
    public static IEnumerable<Instruction> Known() => _known.Value.Values.AsEnumerable();

    /// <summary>
    /// Implicitly converts the <see cref="Instruction"/> instance to a <see cref="string"/> value.
    /// </summary>
    /// <param name="instruction">The instruction to convert.</param>
    /// <returns>A <see cref="string"/> representing the instrcution key.</returns>
    public static implicit operator string(Instruction instruction) => instruction.Key;

    /// <summary>
    /// Explicitly converts the <see cref="string"/> value to an <see cref="Instruction"/> instance.
    /// </summary>
    /// <param name="key">The string key of the instrution.</param>
    /// <returns></returns>
    public static implicit operator Instruction(string key) => new(key);

    /// <summary>
    /// Determines the equality of two <see cref="Instruction"/> instances.
    /// </summary>
    /// <param name="left">The left <see cref="Instruction"/> to compare.</param>
    /// <param name="right">The right <see cref="Instruction"/> to compare.</param>
    /// <returns><c>true</c> if the values are equal; Otherwise, <c>false</c>.</returns>
    public static bool operator ==(Instruction? left, Instruction? right) => Equals(left, right);

    /// <summary>
    /// Determines the equality of two <see cref="Instruction"/> instances.
    /// </summary>
    /// <param name="left">The left <see cref="Instruction"/> to compare.</param>
    /// <param name="right">The right <see cref="Instruction"/> to compare.</param>
    /// <returns><c>true</c> if the values are not equal; Otherwise, <c>false</c>.</returns>
    public static bool operator !=(Instruction? left, Instruction? right) => !Equals(left, right);

    #region Instructions

    /// <summary>
    /// Gets the <c>ABL</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction ABL = new(nameof(ABL));

    /// <summary>
    /// Gets the <c>ABS</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction ABS = new(nameof(ABS));

    /// <summary>
    /// Gets the <c>ACB</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction ACB = new(nameof(ACB));

    /// <summary>
    /// Gets the <c>ACL</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction ACL = new(nameof(ACL));

    /// <summary>
    /// Gets the <c>ACS</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction ACS = new(nameof(ACS));

    /// <summary>
    /// Gets the <c>ADD</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction ADD = new(nameof(ADD));

    /// <summary>
    /// Gets the <c>AFI</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction AFI = new(nameof(AFI));

    /// <summary>
    /// Gets the <c>AHL</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction AHL = new(nameof(AHL));

    /// <summary>
    /// Gets the <c>ALMA</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction ALMA = new(nameof(ALMA));

    /// <summary>
    /// Gets the <c>ALMD</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction ALMD = new(nameof(ALMD));

    /// <summary>
    /// Gets the <c>AND</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction AND = new(nameof(AND));

    /// <summary>
    /// Gets the <c>ARD</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction ARD = new(nameof(ARD));

    /// <summary>
    /// Gets the <c>ARL</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction ARL = new(nameof(ARL));

    /// <summary>
    /// Gets the <c>ASN</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction ASN = new(nameof(ASN));

    /// <summary>
    /// Gets the <c>ATN</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction ATN = new(nameof(ATN));

    /// <summary>
    /// Gets the <c>AVC</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction AVC = new(nameof(AVC));

    /// <summary>
    /// Gets the <c>AVE</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction AVE = new(nameof(AVE));

    /// <summary>
    /// Gets the <c>AWA</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction AWA = new(nameof(AWA));

    /// <summary>
    /// Gets the <c>AWT</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction AWT = new(nameof(AWT));

    /// <summary>
    /// Gets the <c>BRK</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction BRK = new(nameof(BRK));

    /// <summary>
    /// Gets the <c>BSL</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction BSL = new(nameof(BSL));

    /// <summary>
    /// Gets the <c>BSR</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction BSR = new(nameof(BSR));

    /// <summary>
    /// Gets the <c>BTD</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction BTD = new(nameof(BTD));

    /// <summary>
    /// Gets the <c>CBCM</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction CBCM = new(nameof(CBCM));

    /// <summary>
    /// Gets the <c>CBIM</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction CBIM = new(nameof(CBIM));

    /// <summary>
    /// Gets the <c>CBSSM</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction CBSSM = new(nameof(CBSSM));

    /// <summary>
    /// Gets the <c>CLR</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction CLR = new(nameof(CLR));

    /// <summary>
    /// Gets the <c>CMP</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction CMP = new(nameof(CMP), false);

    /// <summary>
    /// Gets the <c>CONCAT</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction CONCAT = new(nameof(CONCAT));

    /// <summary>
    /// Gets the <c>COP</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction COP = new(nameof(COP));

    /// <summary>
    /// Gets the <c>COS</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction COS = new(nameof(COS));

    /// <summary>
    /// Gets the <c>CPM</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction CPM = new(nameof(CPM));

    /// <summary>
    /// Gets the <c>CPS</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction CPS = new(nameof(CPS));

    /// <summary>
    /// Gets the <c>CPT</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction CPT = new(nameof(CPT));

    /// <summary>
    /// Gets the <c>CROUT</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction CROUT = new(nameof(CROUT));

    /// <summary>
    /// Gets the <c>CSM</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction CSM = new(nameof(CSM));

    /// <summary>
    /// Gets the <c>CTD</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction CTD = new(nameof(CTD));

    /// <summary>
    /// Gets the <c>CTU</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction CTU = new(nameof(CTU));

    /// <summary>
    /// Gets the <c>DCM</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction DCM = new(nameof(DCM));

    /// <summary>
    /// Gets the <c>DCS</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction DCS = new(nameof(DCS));

    /// <summary>
    /// Gets the <c>DCSRT</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction DCSRT = new(nameof(DCSRT));

    /// <summary>
    /// Gets the <c>DCST</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction DCST = new(nameof(DCST));

    /// <summary>
    /// Gets the <c>DCSTM</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction DCSTM = new(nameof(DCSTM));

    /// <summary>
    /// Gets the <c>DCSTL</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction DCSTL = new(nameof(DCSTL));

    /// <summary>
    /// Gets the <c>DDT</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction DDT = new(nameof(DDT));

    /// <summary>
    /// Gets the <c>DEG</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction DEG = new(nameof(DEG));

    /// <summary>
    /// Gets the <c>DELETE</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction DELETE = new(nameof(DELETE));

    /// <summary>
    /// Gets the <c>DIN</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction DIN = new(nameof(DIN));

    /// <summary>
    /// Gets the <c>DIV</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction DIV = new(nameof(DIV));

    /// <summary>
    /// Gets the <c>DTOS</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction DTOS = new(nameof(DTOS));

    /// <summary>
    /// Gets the <c>DTR</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction DTR = new(nameof(DTR));

    /// <summary>
    /// Gets the <c>ENPEN</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction ENPEN = new(nameof(ENPEN));

    /// <summary>
    /// Gets the <c>EOT</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction EOT = new(nameof(EOT));

    /// <summary>
    /// Gets the <c>EPMS</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction EPMS = new(nameof(EPMS));

    /// <summary>
    /// Gets the <c>EQU</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction EQU = new(nameof(EQU), false);

    /// <summary>
    /// Gets the <c>ESTOP</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction ESTOP = new(nameof(ESTOP));

    /// <summary>
    /// Gets the <c>EVENT</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction EVENT = new(nameof(EVENT));

    /// <summary>
    /// Gets the <c>FAL</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction FAL = new(nameof(FAL));

    /// <summary>
    /// Gets the <c>FBC</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction FBC = new(nameof(FBC));

    /// <summary>
    /// Gets the <c>FFL</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction FFL = new(nameof(FFL));

    /// <summary>
    /// Gets the <c>FFU</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction FFU = new(nameof(FFU));

    /// <summary>
    /// Gets the <c>FIND</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction FIND = new(nameof(FIND));

    /// <summary>
    /// Gets the <c>FLL</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction FLL = new(nameof(FLL));

    /// <summary>
    /// Gets the <c>FOR</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction FOR = new(nameof(FOR));

    /// <summary>
    /// Gets the <c>FPMS</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction FPMS = new(nameof(FPMS));

    /// <summary>
    /// Gets the <c>FRD</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction FRD = new(nameof(FRD));

    /// <summary>
    /// Gets the <c>FSBM</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction FSBM = new(nameof(FSBM));

    /// <summary>
    /// Gets the <c>FSC</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction FSC = new(nameof(FSC));

    /// <summary>
    /// Gets the <c>GEQ</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction GEQ = new(nameof(GEQ));

    /// <summary>
    /// Gets the <c>GRT</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction GRT = new(nameof(GRT));

    /// <summary>
    /// Gets the <c>GSV</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction GSV = new(nameof(GSV));

    /// <summary>
    /// Gets the <c>INSERT</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction INSERT = new(nameof(INSERT));

    /// <summary>
    /// Gets the <c>IOT</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction IOT = new(nameof(IOT));

    /// <summary>
    /// Gets the <c>JMP</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction JMP = new(nameof(JMP));

    /// <summary>
    /// Gets the <c>JSR</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction JSR = new(nameof(JSR));

    /// <summary>
    /// Gets the <c>JXR</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction JXR = new(nameof(JXR));

    /// <summary>
    /// Gets the <c>LBL</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction LBL = new(nameof(LBL));

    /// <summary>
    /// Gets the <c>LC</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction LC = new(nameof(LC));

    /// <summary>
    /// Gets the <c>LEQ</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction LEQ = new(nameof(LEQ));

    /// <summary>
    /// Gets the <c>LES</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction LES = new(nameof(LES));

    /// <summary>
    /// Gets the <c>LFL</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction LFL = new(nameof(LFL));

    /// <summary>
    /// Gets the <c>LFU</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction LFU = new(nameof(LFU));

    /// <summary>
    /// Gets the <c>LIM</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction LIM = new(nameof(LIM));

    /// <summary>
    /// Gets the <c>LN</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction LN = new(nameof(LN));

    /// <summary>
    /// Gets the <c>LOG</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction LOG = new(nameof(LOG));

    /// <summary>
    /// Gets the <c>LOWER</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction LOWER = new(nameof(LOWER));

    /// <summary>
    /// Gets the <c>MAAT</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction MAAT = new(nameof(MAAT));

    /// <summary>
    /// Gets the <c>MAFR</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction MAFR = new(nameof(MAFR));

    /// <summary>
    /// Gets the <c>MAG</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction MAG = new(nameof(MAG));

    /// <summary>
    /// Gets the <c>MAH</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction MAH = new(nameof(MAH));

    /// <summary>
    /// Gets the <c>MAHD</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction MAHD = new(nameof(MAHD));

    /// <summary>
    /// Gets the <c>MAJ</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction MAJ = new(nameof(MAJ));

    /// <summary>
    /// Gets the <c>MAM</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction MAM = new(nameof(MAM));

    /// <summary>
    /// Gets the <c>MAOC</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction MAOC = new(nameof(MAOC));

    /// <summary>
    /// Gets the <c>MAPC</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction MAPC = new(nameof(MAPC));

    /// <summary>
    /// Gets the <c>MAR</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction MAR = new(nameof(MAR));

    /// <summary>
    /// Gets the <c>MAS</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction MAS = new(nameof(MAS));

    /// <summary>
    /// Gets the <c>MASD</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction MASD = new(nameof(MASD));

    /// <summary>
    /// Gets the <c>MASR</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction MASR = new(nameof(MASR));

    /// <summary>
    /// Gets the <c>MATC</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction MATC = new(nameof(MATC));

    /// <summary>
    /// Gets the <c>MAW</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction MAW = new(nameof(MAW));

    /// <summary>
    /// Gets the <c>MCCD</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction MCCD = new(nameof(MCCD));

    /// <summary>
    /// Gets the <c>MCCM</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction MCCM = new(nameof(MCCM));

    /// <summary>
    /// Gets the <c>MCCP</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction MCCP = new(nameof(MCCP));

    /// <summary>
    /// Gets the <c>MCLM</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction MCLM = new(nameof(MCLM));

    /// <summary>
    /// Gets the <c>MCD</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction MCD = new(nameof(MCD));

    /// <summary>
    /// Gets the <c>MCR</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction MCR = new(nameof(MCR));

    /// <summary>
    /// Gets the <c>MCS</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction MCS = new(nameof(MCS));

    /// <summary>
    /// Gets the <c>MCSD</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction MCSD = new(nameof(MCSD));

    /// <summary>
    /// Gets the <c>MCSR</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction MCSR = new(nameof(MCSR));

    /// <summary>
    /// Gets the <c>MCSV</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction MCSV = new(nameof(MCSV));

    /// <summary>
    /// Gets the <c>MCT</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction MCT = new(nameof(MCT));

    /// <summary>
    /// Gets the <c>MCTP</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction MCTP = new(nameof(MCTP));

    /// <summary>
    /// Gets the <c>MDF</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction MDF = new(nameof(MDF));

    /// <summary>
    /// Gets the <c>MDO</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction MDO = new(nameof(MDO));

    /// <summary>
    /// Gets the <c>MDOC</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction MDOC = new(nameof(MDOC));

    /// <summary>
    /// Gets the <c>MDR</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction MDR = new(nameof(MDR));

    /// <summary>
    /// Gets the <c>MDW</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction MDW = new(nameof(MDW));

    /// <summary>
    /// Gets the <c>MEQ</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction MEQ = new(nameof(MEQ));

    /// <summary>
    /// Gets the <c>MGS</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction MGS = new(nameof(MGS));

    /// <summary>
    /// Gets the <c>MGSD</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction MGSD = new(nameof(MGSD));

    /// <summary>
    /// Gets the <c>MGSP</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction MGSP = new(nameof(MGSP));

    /// <summary>
    /// Gets the <c>MGSR</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction MGSR = new(nameof(MGSR));

    /// <summary>
    /// Gets the <c>MID</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction MID = new(nameof(MID));

    /// <summary>
    /// Gets the <c>MMVC</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction MMVC = new(nameof(MMVC));

    /// <summary>
    /// Gets the <c>MOD</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction MOD = new(nameof(MOD));

    /// <summary>
    /// Gets the <c>MOV</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction MOV = new(nameof(MOV));

    /// <summary>
    /// Gets the <c>MRAT</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction MRAT = new(nameof(MRAT));

    /// <summary>
    /// Gets the <c>MRHD</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction MRHD = new(nameof(MRHD));

    /// <summary>
    /// Gets the <c>MRP</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction MRP = new(nameof(MRP));

    /// <summary>
    /// Gets the <c>MSF</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction MSF = new(nameof(MSF));

    /// <summary>
    /// Gets the <c>MSG</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction MSG = new(nameof(MSG));

    /// <summary>
    /// Gets the <c>MSO</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction MSO = new(nameof(MSO));

    /// <summary>
    /// Gets the <c>MUL</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction MUL = new(nameof(MUL));

    /// <summary>
    /// Gets the <c>MVC</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction MVC = new(nameof(MVC));

    /// <summary>
    /// Gets the <c>MVM</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction MVM = new(nameof(MVM));

    /// <summary>
    /// Gets the <c>NEG</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction NEG = new(nameof(NEG));

    /// <summary>
    /// Gets the <c>NEQ</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction NEQ = new(nameof(NEQ));

    /// <summary>
    /// Gets the <c>NOP</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction NOP = new(nameof(NOP));

    /// <summary>
    /// Gets the <c>NOT</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction NOT = new(nameof(NOT));

    /// <summary>
    /// Gets the <c>ONS</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction ONS = new(nameof(ONS));

    /// <summary>
    /// Gets the <c>OR</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction OR = new(nameof(OR));

    /// <summary>
    /// Gets the <c>OSF</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction OSF = new(nameof(OSF));

    /// <summary>
    /// Gets the <c>OSR</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction OSR = new(nameof(OSR));

    /// <summary>
    /// Gets the <c>OTE</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction OTE = new(nameof(OTE));

    /// <summary>
    /// Gets the <c>OTL</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction OTL = new(nameof(OTL));

    /// <summary>
    /// Gets the <c>OTU</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction OTU = new(nameof(OTU));

    /// <summary>
    /// Gets the <c>PATT</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction PATT = new(nameof(PATT));

    /// <summary>
    /// Gets the <c>PCLF</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction PCLF = new(nameof(PCLF));

    /// <summary>
    /// Gets the <c>PCMD</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction PCMD = new(nameof(PCMD));

    /// <summary>
    /// Gets the <c>PDET</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction PDET = new(nameof(PDET));

    /// <summary>
    /// Gets the <c>PFL</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction PFL = new(nameof(PFL));

    /// <summary>
    /// Gets the <c>PID</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction PID = new(nameof(PID));

    /// <summary>
    /// Gets the <c>POVR</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction POVR = new(nameof(POVR));

    /// <summary>
    /// Gets the <c>PPD</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction PPD = new(nameof(PPD));

    /// <summary>
    /// Gets the <c>PRNP</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction PRNP = new(nameof(PRNP));

    /// <summary>
    /// Gets the <c>PSC</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction PSC = new(nameof(PSC));

    /// <summary>
    /// Gets the <c>PXRQ</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction PXRQ = new(nameof(PXRQ));

    /// <summary>
    /// Gets the <c>RAD</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction RAD = new(nameof(RAD));

    /// <summary>
    /// Gets the <c>RES</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction RES = new(nameof(RES));

    /// <summary>
    /// Gets the <c>RET</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction RET = new(nameof(RET));

    /// <summary>
    /// Gets the <c>RIN</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction RIN = new(nameof(RIN));

    /// <summary>
    /// Gets the <c>ROUT</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction ROUT = new(nameof(ROUT));

    /// <summary>
    /// Gets the <c>RTO</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction RTO = new(nameof(RTO));

    /// <summary>
    /// Gets the <c>RTOS</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction RTOS = new(nameof(RTOS));

    /// <summary>
    /// Gets the <c>SBR</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction SBR = new(nameof(SBR));

    /// <summary>
    /// Gets the <c>SFP</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction SFP = new(nameof(SFP));

    /// <summary>
    /// Gets the <c>SFR</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction SFR = new(nameof(SFR));

    /// <summary>
    /// Gets the <c>SIN</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction SIN = new(nameof(SIN));

    /// <summary>
    /// Gets the <c>SIZE</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction SIZE = new(nameof(SIZE));

    /// <summary>
    /// Gets the <c>SMAT</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction SMAT = new(nameof(SMAT));

    /// <summary>
    /// Gets the <c>SQI</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction SQI = new(nameof(SQI));

    /// <summary>
    /// Gets the <c>SQL</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction SQL = new(nameof(SQL));

    /// <summary>
    /// Gets the <c>SQO</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction SQO = new(nameof(SQO));

    /// <summary>
    /// Gets the <c>SQR</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction SQR = new(nameof(SQR));

    /// <summary>
    /// Gets the <c>SRT</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction SRT = new(nameof(SRT));

    /// <summary>
    /// Gets the <c>SSV</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction SSV = new(nameof(SSV));

    /// <summary>
    /// Gets the <c>STD</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction STD = new(nameof(STD));

    /// <summary>
    /// Gets the <c>STOD</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction STOD = new(nameof(STOD));

    /// <summary>
    /// Gets the <c>STOR</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction STOR = new(nameof(STOR));

    /// <summary>
    /// Gets the <c>SUB</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction SUB = new(nameof(SUB));

    /// <summary>
    /// Gets the <c>SWPB</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction SWPB = new(nameof(SWPB));

    /// <summary>
    /// Gets the <c>TAN</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction TAN = new(nameof(TAN));

    /// <summary>
    /// Gets the <c>THRS</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction THRS = new(nameof(THRS));

    /// <summary>
    /// Gets the <c>THRSE</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction THRSE = new(nameof(THRSE));

    /// <summary>
    /// Gets the <c>TND</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction TND = new(nameof(TND));

    /// <summary>
    /// Gets the <c>TOD</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction TOD = new(nameof(TOD));

    /// <summary>
    /// Gets the <c>TOF</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction TOF = new(nameof(TOF));

    /// <summary>
    /// Gets the <c>TON</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction TON = new(nameof(TON));

    /// <summary>
    /// Gets the <c>TRN</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction TRN = new(nameof(TRN));

    /// <summary>
    /// Gets the <c>TSAM</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction TSAM = new(nameof(TSAM));

    /// <summary>
    /// Gets the <c>TSSM</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction TSSM = new(nameof(TSSM));

    /// <summary>
    /// Gets the <c>UID</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction UID = new(nameof(UID));

    /// <summary>
    /// Gets the <c>UIE</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction UIE = new(nameof(UIE));

    /// <summary>
    /// Gets the <c>UPPER</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction UPPER = new(nameof(UPPER));

    /// <summary>
    /// Gets the <c>XIC</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction XIC = new(nameof(XIC));

    /// <summary>
    /// Gets the <c>XIO</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction XIO = new(nameof(XIO));

    /// <summary>
    /// Gets the <c>XOR</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction XOR = new(nameof(XOR));

    /// <summary>
    /// Gets the <c>XPY</c> instruction definition instance.
    /// </summary>
    public static readonly Instruction XPY = new(nameof(XPY));

    #endregion
    
    /// <summary>
    /// List of known instrcution keys to used to determine if a given instruction is destructive.
    /// </summary>
    private static List<string> ConditionalKeys() => new()
    {
        nameof(CMP), nameof(EQU), nameof(GEQ), nameof(GRT),nameof(LEQ), nameof(LES),
        nameof(LIM), nameof(MEQ), nameof(NEQ), nameof(XIC),nameof(XIO)
    };

    /// <summary>
    /// Lazy list of all known instructions.
    /// </summary>
    private static Lazy<Dictionary<string, Instruction>> _known =>
        new(() => GetInstructionFields().ToDictionary(i => i.Key, StringComparer.OrdinalIgnoreCase),
            LazyThreadSafetyMode.ExecutionAndPublication);

    /// <summary>
    /// Returns all defined instructions members for the provided type.
    /// </summary>
    private static IEnumerable<Instruction> GetInstructionFields()
    {
        var type = typeof(Instruction);

        return type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(f => type.IsAssignableFrom(f.FieldType))
            .Select(f => (Instruction) f.GetValue(null))
            .ToList();
    }
}