using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;


// ReSharper disable StringLiteralTypo
// ReSharper disable IdentifierTypo
// ReSharper disable InconsistentNaming
// ReSharper disable CommentTypo

namespace L5Sharp.Core;

/// <summary>
/// A wrapper around a string of <see cref="NeutralText"/> that represents a single logix instruction instance.
/// This class provides methods and properties for extracting and analyzing data of the instruction.
/// This class also includes built-in facotry methods to create new instances of known Rockwell instructions.
/// </summary>
public sealed class Instruction
{
    /// <summary>
    /// Pattern for identifying any instruction and the contents of its signature. This expression should
    /// capture everything enclosed or between the instruction parentheses. This includes nested parenthesis.
    /// This works on the assumption that the text has balanced opening/closing parentheses.
    /// </summary>
    public const string Pattern = @"[A-Za-z_]\w{1,39}\((?>\((?<c>)|[^()]+|\)(?<-c>))*(?(c)(?!))\)";

    /// <summary>
    /// Captures all content within parentheses, including outer parentheses and nested parentheses, assuming they
    /// are balanced (number of opening equals number of closing).
    /// </summary>
    private const string SignaturePattern = @"\((?>\((?<c>)|[^()]+|\)(?<-c>))*(?(c)(?!))\)";

    /// <summary>
    /// A regex pattern that finds all commas not contained in array brackets so that we can split the arguments
    /// of an instruction signature into separate parsable values.
    /// </summary>
    private const string ArgumentSplitPattern = ",(?![^[]*])";

    /// <summary>
    /// Lazy list of all known instructions and their corresponding factory method function.
    /// </summary>
    private static readonly Dictionary<string, Func<Instruction>> _known =
        Factories().ToDictionary(x => x.Key, x => x.Value);

    /// <summary>
    /// Indicates whether this <c>Instruction</c> is one that calls or references a <c>Task</c> component by name.
    /// </summary>
    private bool IsSystemCall => Key is nameof(GSV) or nameof(SSV);

    /// <summary>
    /// Indicates whether this <c>Instruction</c> is one that calls or references a <c>Task</c> component by name.
    /// </summary>
    private bool IsTaskCall => Key is nameof(EVENT);

    /// <summary>
    /// Indicates whether this <c>Instruction</c> is one that calls or references a <c>Routine</c> component by name.
    /// </summary>
    private bool IsRoutineCall => Key is nameof(JSR) or nameof(JXR) or nameof(SFR) or nameof(SFP) or nameof(FOR);

    /// <summary>
    /// Creates a new <see cref="Instruction"/> with the provided string key and regex signature pattern.
    /// </summary>
    /// <param name="key">The key identifier of the instruction.</param>
    /// <param name="signature"></param>
    /// <param name="arguments"></param>
    private Instruction(string key, string? signature = null, params Argument[] arguments)
    {
        if (string.IsNullOrEmpty(key))
            throw new ArgumentException("Instruction key cannot be null or empty.", nameof(key));

        Key = key;
        Signature = signature ?? $"{key}({DefaultArgs(arguments.Length)})";
        Arguments = arguments;
    }

    /// <summary>
    /// The unique shorthand identifier of the instruction type.
    /// </summary>
    /// <value>
    /// A <see cref="string"/> containing the shorthand instruction key identifier (e.g. XIC/OTE).
    /// For an <c>AddOnInstruction</c> this is the name of the component.
    /// </value>
    public string Key { get; }

    /// <summary>
    /// The string that rerepsents the signatrue of the instruction. The signature is the neutral text representation
    /// with operand or parameter names instead of the actual argument values.
    /// </summary>
    public string Signature { get; }

    /// <summary>
    /// The collection of <see cref="Core.Argument"/> values the instruction contains.
    /// </summary>
    /// <value>
    /// A collection of  <see cref="Core.Argument"/> value objects.
    /// These could represent literal values, tag names, or nested expressions.
    /// </value>
    public Argument[] Arguments { get; }

    /// <summary>
    /// The collection of operand names found in the signature of the instruction.
    /// </summary>
    /// <remarks>
    /// Operands are the names of the parameters that define the instruction signature, whereas <see cref="Arguments"/>
    /// are the actual values that are found in the text representation of the instruction.
    /// Operands can only be obtained from known instruction signatures, which are configured when using the built-in
    /// factory methods of this class.
    /// </remarks>
    public string[] Operands => ExtractOperands();

    /// <summary>
    /// Retrieves all referenced tag name values found in this instruction.
    /// </summary>
    /// <returns>A collection of <see cref="TagName"/> values cotnained by the instruction.</returns>
    public TagName[] Tags => ExtractTags();

    /// <summary>
    /// Retrieves all immediate atomic type values found in this instruction.
    /// </summary>
    /// <returns>A collection of <see cref="AtomicData"/> values cotnained by the instruction.</returns>
    public AtomicData[] Values => ExtractValues();

    /// <summary>
    /// Retrieves all component references found in the instruction as a relative <see cref="Scope"/> object.
    /// </summary>
    /// <returns>A collection of <see cref="Scope"/> values cotnained by the instruction.</returns>
    /// <remarks>
    /// These will not only be tag references, but any other component type including routine, task, module, etc.
    /// This is determined by the instruction type (e.g. JSR, EVENT, GSV, etc.).
    /// </remarks>
    public Scope[] References => ExtractReferences();

    /// <summary>
    /// The <see cref="NeutralText"/> representation of the instruction instance.
    /// </summary>
    /// <value>A <see cref="NeutralText"/> instance that represents the instruction in Logix neutral text format.</value>
    public NeutralText Text => new($"{Key}({Arguments.Combine(',')})");

    /// <summary>
    /// Indicates whether the instruction is conditional or evaluates a certain condition to be true or false, from which
    /// it directs the control flow of a program.
    /// </summary>
    /// <value><c>true</c> if the instruction is conditional; Otherwise, <c>false</c>.</value>
    /// <remarks>An example of a condition instruction is an <see cref="XIC"/>.</remarks>
    public bool IsConditional => Key is nameof(CMP)
        or nameof(EQU) or nameof(EQ)
        or nameof(GEQ) or nameof(GE)
        or nameof(GRT) or nameof(GT)
        or nameof(LEQ) or nameof(LE)
        or nameof(LES) or nameof(LT)
        or nameof(LIM) or nameof(LIMIT)
        or nameof(MEQ)
        or nameof(NEQ) or nameof(NE)
        or nameof(XIC) or nameof(XIO);

    /// <summary>
    /// Indicates whether the instruction argument count matches the operand count.
    /// </summary>
    public bool IsValid => Key is nameof(JSR) or nameof(SBR) or nameof(RET) || Operands.Length == Arguments.Length;

    /// <summary>
    /// Creates a new <see cref="Instruction"/> with the provided key and optional arguments.
    /// </summary>
    /// <param name="key">A <see cref="string"/> containing the unique name of the instruction.</param>
    /// <param name="args">A set of <see cref="Core.Argument"/> to initialize the intruction with.</param>
    /// <returns>A <see cref="Instruction"/> instance with the provided key and arguments.</returns>
    /// <remarks>
    /// This factory method is the means through which to create unknown or other instruction that are not captured
    /// in this classes static factory methods. If this is a known instruction, use the corresponding instruction
    /// factory method, so it can initialize the known signature.
    /// </remarks>
    public static Instruction New(string key, params Argument[] args) => new(key, arguments: args);

    /// <summary>
    /// Creates a new <see cref="Instruction"/> with the provided key, signature, and optional arguments.
    /// </summary>
    /// <param name="key">A <see cref="string"/> containing the unique name of the instruction.</param>
    /// <param name="signature">A <see cref="string"/> containing the method signature or format. This should be in the
    /// format 'key(arg1,arg2,...)'.</param>
    /// <param name="args">A set of <see cref="Core.Argument"/> to initialize the intruction with.</param>
    /// <returns>A <see cref="Instruction"/> instance with the provided key, signature, and arguments.</returns>
    public static Instruction New(string key, string? signature = null, params Argument[] args) =>
        new(key, signature, args);

    /// <summary>
    /// Parses the provided string neutral text into a <see cref="Instruction"/> instance.
    /// </summary>
    /// <param name="text">The neutral text to parse.</param>
    /// <returns>A <see cref="Instruction"/> object representing the parsed text.</returns>
    /// <exception cref="ArgumentException"><c>text</c> is null or empty.</exception>
    public static Instruction Parse(string text)
    {
        if (string.IsNullOrEmpty(text))
            throw new ArgumentException("Instruction text can not be null or empty.", nameof(text));

        if (!Regex.IsMatch(text, Pattern))
            throw new FormatException("Instruction text must be in the format of 'key(arg1,arg2,...)'.");

        // ReSharper disable once ReplaceSubstringWithRangeIndexer not supported in .NET Standard
        var key = text.Substring(0, text.IndexOf('('));
        var match = Regex.Match(text, SignaturePattern);
        var signature = match.Value.Substring(1, match.Value.Length - 2);
        var arguments = Regex.Split(signature, ArgumentSplitPattern).Select(Core.Argument.TryParse).ToArray();

        return _known.TryGetValue(key, out var create)
            ? create().With(arguments)
            : new Instruction(key, arguments: arguments);
    }

    /// <summary>
    /// Returns a collection of all known or defined <see cref="Instruction"/> instances.
    /// </summary>
    /// <returns>
    /// A <see cref="IEnumerable{T}"/> containing all known <see cref="Instruction"/> instances with no arguments.
    /// </returns>
    public static IEnumerable<string> Keys() => _known.Keys.AsEnumerable();

    /// <summary>
    /// Creates a <see cref="Instruction"/> of the same type with the updated argument values.
    /// </summary>
    /// <param name="arguments">The collection of arguments make up the instruction signature.</param>
    /// <returns>A new <see cref="Instruction"/> complete with the provided <see cref="Core.Argument"/> values.</returns>
    public Instruction Append(params Argument[] arguments) =>
        new(Key, Signature, Arguments.Concat(arguments).ToArray());


    /// <summary>
    /// Creates a <see cref="Instruction"/> of the same type with the updated argument values.
    /// </summary>
    /// <param name="arguments">The collection of arguments make up the instruction signature.</param>
    /// <returns>A new <see cref="Instruction"/> complete with the provided <see cref="Core.Argument"/> values.</returns>
    public Instruction With(params Argument[] arguments) => new(Key, Signature, arguments);

    /// <summary>
    /// Retrieves the argument for a specified operand name from the instruction.
    /// </summary>
    /// <param name="operand">A <see cref="string"/> containing the operand name to search for.</param>
    /// <returns>A <see cref="Core.Argument"/> matching the specified operand name.</returns>
    public Argument? Argument(string operand)
    {
        var index = Operands.ToList().IndexOf(operand);
        return index >= 0 ? Arguments.ElementAt(index) : default;
    }

    /// <inheritdoc />
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true;

        return obj switch
        {
            Instruction other => Equals(Text, other.Text),
            string text => Text.ToString().IsEquivalent(text),
            _ => false
        };
    }

    /// <inheritdoc />
    public override int GetHashCode() => Text.GetHashCode();

    /// <inheritdoc />
    public override string ToString() => Text.ToString();

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

    #region Factories

    /// <summary>
    /// Gets the <c>ABL</c> instruction definition instance.
    /// </summary>
    public static Instruction ABL(Argument channel, Argument serial_port_control, Argument character_count) =>
        new(nameof(ABL), "ABL(channel,serial_port_control,character_count)", channel, serial_port_control,
            character_count);

    /// <summary>
    /// Gets the <c>ABS</c> instruction definition instance.
    /// </summary>
    public static Instruction ABS(Argument source, Argument destination) =>
        new(nameof(ABS), "ABS(source,destination)", source, destination);

    /// <summary>
    /// Gets the <c>ACB</c> instruction definition instance.
    /// </summary>
    public static Instruction ACB(Argument channel, Argument serial_port_control, Argument character_count) =>
        new(nameof(ACB), "ACB(channel,serial_port_control,character_count)", channel, serial_port_control,
            character_count);

    /// <summary>
    /// Gets the <c>ACL</c> instruction definition instance.
    /// </summary>
    public static Instruction ACL(Argument channel, Argument clear_serial_port_read, Argument clear_serial_port_write)
        => new(nameof(ACL),
            "ACL(channel,clear_serial_port_read,clear_serial_port_write)",
            channel,
            clear_serial_port_read,
            clear_serial_port_write);

    /// <summary>
    /// Gets the <c>ACS</c> instruction definition instance.
    /// </summary>
    public static Instruction ACS(Argument source, Argument destination) =>
        new(nameof(ACS), "ACS(source,destination)", source, destination);

    /// <summary>
    /// Gets the <c>ACOS</c> instruction definition instance.
    /// </summary>
    public static Instruction ACOS(Argument source, Argument destination) =>
        new(nameof(ACOS), "ACOS(source,destination)", source, destination);

    /// <summary>
    /// Gets the <c>ADD</c> instruction definition instance.
    /// </summary>
    public static Instruction ADD(Argument source_A, Argument source_B, Argument destination) =>
        new(nameof(ADD), "ADD(source_A,source_B,destination)", source_A, source_B, destination);

    /// <summary>
    /// Gets the <c>AFI</c> instruction definition instance.
    /// </summary>
    public static Instruction AFI() => new(nameof(AFI), "AFI()");

    /// <summary>
    /// Gets the <c>AHL</c> instruction definition instance.
    /// </summary>
    public static Instruction AHL(Argument channel, Argument ANDMask, Argument ORMask, Argument serial_port_control,
        Argument channel_status) => new(nameof(AHL),
        "AHL(channel,ANDMask,ORMask,serial_port_control,channel_status)", channel, ANDMask, ORMask,
        serial_port_control, channel_status);

    /// <summary>
    /// Gets the <c>ALMA</c> instruction definition instance.
    /// </summary>
    public static Instruction ALMA(Argument alma_tag, Argument @in, Argument program_acknowledge_all,
        Argument program_disable, Argument program_enable) => new(nameof(ALMA),
        "ALMA(alma_tag,in,program_acknowledge_all,program_disable,program_enable)", alma_tag, @in,
        program_acknowledge_all, program_disable, program_enable);

    /// <summary>
    /// Gets the <c>ALMD</c> instruction definition instance.
    /// </summary>
    public static Instruction ALMD(Argument almd_tag, Argument program_acknowledge, Argument program_reset,
        Argument program_disable, Argument program_enable) => new(nameof(ALMD),
        "ALMD(almd_tag,program_acknowledge,program_reset,program_disable,program_enable)", almd_tag,
        program_acknowledge, program_reset, program_disable, program_enable);

    /// <summary>
    /// Gets the <c>AND</c> instruction definition instance.
    /// </summary>
    public static Instruction AND(Argument source_A, Argument source_B, Argument destination) =>
        new(nameof(AND), "AND(source_A,source_B,destination)", source_A, source_B, destination);

    /// <summary>
    /// Gets the <c>ARD</c> instruction definition instance.
    /// </summary>
    public static Instruction ARD(Argument channel, Argument destination, Argument serial_port_control,
        Argument string_length, Argument characters_read) => new(nameof(ARD),
        "ARD(channel,destination,serial_port_control,string_length,characters_read)", channel, destination,
        serial_port_control, string_length, characters_read);

    /// <summary>
    /// Gets the <c>ARL</c> instruction definition instance.
    /// </summary>
    public static Instruction ARL(Argument channel, Argument destination, Argument serial_port_control,
        Argument string_length, Argument characters_read) => new(nameof(ARL),
        "ARL(channel,destination,serial_port_control,string_length,characters_read)", channel, destination,
        serial_port_control, string_length, characters_read);

    /// <summary>
    /// Gets the <c>ASN</c> instruction definition instance.
    /// </summary>
    public static Instruction ASN(Argument source, Argument destination) =>
        new(nameof(ASN), "ASN(source,destination)", source, destination);

    /// <summary>
    /// Gets the <c>ASIN</c> instruction definition instance.
    /// </summary>
    public static Instruction ASIN(Argument source, Argument destination) =>
        new(nameof(ASIN), "ASIN(source,destination)", source, destination);

    /// <summary>
    /// Gets the <c>ATN</c> instruction definition instance.
    /// </summary>
    public static Instruction ATN(Argument source, Argument destination) =>
        new(nameof(ATN), "ATN(source,destination)", source, destination);

    /// <summary>
    /// Gets the <c>ATAN</c> instruction definition instance.
    /// </summary>
    public static Instruction ATAN(Argument source, Argument destination) =>
        new(nameof(ATAN), "ATAN(source,destination)", source, destination);

    /// <summary>
    /// Gets the <c>AVC</c> instruction definition instance.
    /// </summary>
    public static Instruction AVC(Argument avc_tag, Argument feedback_type, Argument feedback_reation_time,
        Argument delay_type, Argument delay_time, Argument output_follows_actuate, Argument actuate,
        Argument delay_enable, Argument feedback_1, Argument input_status, Argument output_status, Argument reset) =>
        new(nameof(AVC),
            "AVC(avc_tag,feedback_type,feedback_reation_time,delay_type,delay_time,output_follows_actuate,actuate,delay_enable,feedback_1,input_status,output_status,reset)",
            avc_tag, feedback_type, feedback_reation_time, delay_type, delay_time, output_follows_actuate, actuate,
            delay_enable, feedback_1, input_status, output_status, reset);

    /// <summary>
    /// Gets the <c>AVE</c> instruction definition instance.
    /// </summary>
    public static Instruction AVE(Argument array, Argument dim_to_vary, Argument destination, Argument control,
        Argument length, Argument position) => new(nameof(AVE),
        "AVE(array,dim_to_vary,destination,control,length,position)", array, dim_to_vary, destination, control, length,
        position);

    /// <summary>
    /// Gets the <c>AWA</c> instruction definition instance.
    /// </summary>
    public static Instruction AWA(Argument channel, Argument source, Argument serial_port_control,
        Argument string_length, Argument characters_sent) => new(nameof(AWA),
        "AWA(channel,source,serial_port_control,string_length,characters_sent)", channel, source, serial_port_control,
        string_length, characters_sent);

    /// <summary>
    /// Gets the <c>AWT</c> instruction definition instance.
    /// </summary>
    public static Instruction AWT(Argument channel, Argument source, Argument serial_port_control,
        Argument string_length, Argument characters_sent) => new(nameof(AWT),
        "AWT(channel,source,serial_port_control,string_length,characters_sent)", channel, source, serial_port_control,
        string_length, characters_sent);

    /// <summary>
    /// Gets the <c>BRK</c> instruction definition instance.
    /// </summary>
    public static Instruction BRK() => new(nameof(BRK), "BRK()");

    /// <summary>
    /// Gets the <c>BSL</c> instruction definition instance.
    /// </summary>
    public static Instruction BSL(Argument array, Argument control, Argument source_bit, Argument length) =>
        new(nameof(BSL), "BSL(array,control,source_bit,length)", array, control, source_bit, length);

    /// <summary>
    /// Gets the <c>BSR</c> instruction definition instance.
    /// </summary>
    public static Instruction BSR(Argument array, Argument control, Argument source_bit, Argument length) =>
        new(nameof(BSR), "BSR(array,control,source_bit,length)", array, control, source_bit, length);

    /// <summary>
    /// Gets the <c>BTD</c> instruction definition instance.
    /// </summary>
    public static Instruction BTD(Argument source, Argument source_bit, Argument destination, Argument destination_bit,
        Argument length) => new(nameof(BTD),
        "BTD(source,source_bit,destination,destination_bit,length)", source, source_bit, destination, destination_bit,
        length);

    /// <summary>
    /// Gets the <c>CBCM</c> instruction definition instance.
    /// </summary>
    public static Instruction CBCM(Argument cbcm_tag, Argument ack_type, Argument mode, Argument takeover_mode,
        Argument enable, Argument safety_enable, Argument standard_enable, Argument arm_continuous, Argument start,
        Argument stop_at_top, Argument press_in_motion, Argument motion_monitor_fault, Argument slide_zone,
        Argument safety_enable_ack) => new(nameof(CBCM),
        "CBCM(cbcm_tag,ack_type,mode,takeover_mode,enable,safety_enable,standard_enable,arm_continuous,start,stop_at_top,press_in_motion,motion_monitor_fault,slide_zone,safety_enable_ack)",
        cbcm_tag, ack_type, mode, takeover_mode, enable, safety_enable, standard_enable, arm_continuous, start,
        stop_at_top, press_in_motion, motion_monitor_fault, slide_zone, safety_enable_ack);

    /// <summary>
    /// Gets the <c>CBIM</c> instruction definition instance.
    /// </summary>
    public static Instruction CBIM(Argument cbim_tag, Argument ack_type, Argument inch_time, Argument enable,
        Argument safety_enable, Argument standard_enable, Argument start, Argument press_in_motion,
        Argument motion_monitor_fault, Argument slide_zone, Argument safety_enable_ack) => new(nameof(CBIM),
        "CBIM(cbim_tag,ack_type,inch_time,enable,safety_enable,standard_enable,start,press_in_motion,motion_monitor_fault,slide_zone,safety_enable_ack)",
        cbim_tag, ack_type, inch_time, enable, safety_enable, standard_enable, start, press_in_motion,
        motion_monitor_fault, slide_zone, safety_enable_ack);

    /// <summary>
    /// Gets the <c>CBSSM</c> instruction definition instance.
    /// </summary>
    public static Instruction CBSSM(Argument cbssm_tag, Argument ack_type, Argument takeover_mode, Argument enable,
        Argument safety_enable, Argument standard_enable, Argument start, Argument press_in_motion,
        Argument motion_monitor_fault, Argument slide_zone, Argument saefty_enable_ack) => new(nameof(CBSSM),
        "CBSSM(cbssm_tag,ack_type,takeover_mode,enable,safety_enable,standard_enable,start,press_in_motion,motion_monitor_fault,slide_zone,saefty_enable_ack)",
        cbssm_tag, ack_type, takeover_mode, enable, safety_enable, standard_enable, start, press_in_motion,
        motion_monitor_fault, slide_zone, saefty_enable_ack);

    /// <summary>
    /// Gets the <c>CLR</c> instruction definition instance.
    /// </summary>
    public static Instruction CLR(Argument destination) => new(nameof(CLR), "CLR(destination)", destination);

    /// <summary>
    /// Gets the <c>CMP</c> instruction definition instance.
    /// </summary>
    public static Instruction CMP(Argument expression) => new(nameof(CMP), "CMP(expression)", expression);

    /// <summary>
    /// Gets the <c>CONCAT</c> instruction definition instance.
    /// </summary>
    public static Instruction CONCAT(Argument sourceA, Argument sourceB, Argument destination) =>
        new(nameof(CONCAT), "CONCAT(sourceA,sourceB,destination)", sourceA, sourceB, destination);

    /// <summary>
    /// Gets the <c>COP</c> instruction definition instance.
    /// </summary>
    public static Instruction COP(Argument source, Argument destination, Argument length) => new(nameof(COP),
        "COP(source,destination,length)", source, destination, length);

    /// <summary>
    /// Gets the <c>COS</c> instruction definition instance.
    /// </summary>
    public static Instruction COS(Argument source, Argument destination) =>
        new(nameof(COS), "COS(source,destination)", source, destination);

    /// <summary>
    /// Gets the <c>CPM</c> instruction definition instance.
    /// </summary>
    public static Instruction CPM(Argument cpm_tag, Argument cam_profile, Argument enable, Argument brake_cam,
        Argument takeover_cam, Argument dynamic_cam, Argument input_status, Argument reverse,
        Argument press_motion_status, Argument reset) => new(nameof(CPM),
        "CPM(cpm_tag,cam_profile,enable,brake_cam,takeover_cam,dynamic_cam,input_status,reverse,press_motion_status,reset)",
        cpm_tag, cam_profile, enable, brake_cam, takeover_cam, dynamic_cam, input_status, reverse, press_motion_status,
        reset);

    /// <summary>
    /// Gets the <c>CPS</c> instruction definition instance.
    /// </summary>
    public static Instruction CPS(Argument source, Argument destination, Argument length) => new(nameof(CPS),
        "CPS(source,destination,length)", source, destination, length);

    /// <summary>
    /// Gets the <c>CPT</c> instruction definition instance.
    /// </summary>
    public static Instruction CPT(Argument destination, Argument expression) =>
        new(nameof(CPT), "CPT(destination,expression)", destination, expression);

    /// <summary>
    /// Gets the <c>CROUT</c> instruction definition instance.
    /// </summary>
    public static Instruction CROUT(Argument crout_tag, Argument feedback_type, Argument feedback_reaction_time,
        Argument actuate, Argument feedback_1, Argument feedback_2, Argument input_status, Argument output_status,
        Argument reset) => new(nameof(CROUT),
        "CROUT(crout_tag,feedback_type,feedback_reaction_time,actuate,feedback_1,feedback_2,input_status,output_status,reset)",
        crout_tag, feedback_type, feedback_reaction_time, actuate, feedback_1, feedback_2, input_status, output_status,
        reset);

    /// <summary>
    /// Gets the <c>CSM</c> instruction definition instance.
    /// </summary>
    public static Instruction CSM(Argument csm_tag, Argument mechanical_delay_timer, Argument max_pulse_period,
        Argument motion_request, Argument channel_A, Argument channel_B, Argument input_status, Argument reset) => new(
        nameof(CSM),
        "CSM(csm_tag,mechanical_delay_timer,max_pulse_period,motion_request,channel_A,channel_B,input_status,reset)",
        csm_tag, mechanical_delay_timer, max_pulse_period, motion_request, channel_A, channel_B, input_status, reset);

    /// <summary>
    /// Gets the <c>CTD</c> instruction definition instance.
    /// </summary>
    public static Instruction CTD(Argument counter, Argument preset, Argument accum) =>
        new(nameof(CTD), "CTD(counter,preset,accum)", counter, preset, accum);

    /// <summary>
    /// Gets the <c>CTU</c> instruction definition instance.
    /// </summary>
    public static Instruction CTU(Argument counter, Argument preset, Argument accum) =>
        new(nameof(CTU), "CTU(counter,preset,accum)", counter, preset, accum);

    /// <summary>
    /// Gets the <c>DCM</c> instruction definition instance.
    /// </summary>
    public static Instruction DCM(Argument dcm_tag, Argument safety_function, Argument input_type,
        Argument descrepancy_time, Argument channel_A, Argument channel_B, Argument input_status, Argument reset) =>
        new(nameof(DCM),
            "DCM(dcm_tag,safety_function,input_type,descrepancy_time,channel_A,channel_B,input_status,reset)", dcm_tag,
            safety_function, input_type, descrepancy_time, channel_A, channel_B, input_status, reset);

    /// <summary>
    /// Gets the <c>DCS</c> instruction definition instance.
    /// </summary>
    public static Instruction DCS(Argument dcs_tag, Argument safety_function, Argument input_type,
        Argument discrepancy_time, Argument restart_type, Argument cold_start_type, Argument channel_A,
        Argument channel_B, Argument input_status, Argument reset) => new(nameof(DCS),
        "DCS(dcs_tag,safety_function,input_type,discrepancy_time,restart_type,cold_start_type,channel_A,channel_B,input_status,reset)",
        dcs_tag, safety_function, input_type, discrepancy_time, restart_type, cold_start_type, channel_A, channel_B,
        input_status, reset);

    /// <summary>
    /// Gets the <c>DCSRT</c> instruction definition instance.
    /// </summary>
    public static Instruction DCSRT(Argument dcsrt_tag, Argument safety_function, Argument input_type,
        Argument discrepancy_time, Argument enable, Argument channel_A, Argument channel_B, Argument input_status,
        Argument reset) => new(nameof(DCSRT),
        "DCSRT(dcsrt_tag,safety_function,input_type,discrepancy_time,enable,channel_A,channel_B,input_status,reset)",
        dcsrt_tag, safety_function, input_type, discrepancy_time, enable, channel_A, channel_B, input_status, reset);

    /// <summary>
    /// Gets the <c>DCST</c> instruction definition instance.
    /// </summary>
    public static Instruction DCST(Argument dcst_tag, Argument safety_function, Argument input_type,
        Argument discrepancy_time, Argument restart_type, Argument cold_start_type, Argument channel_A,
        Argument channel_B, Argument test_request, Argument input_status, Argument reset) => new(nameof(DCST),
        "DCST(dcst_tag,safety_function,input_type,discrepancy_time,restart_type,cold_start_type,channel_A,channel_B,test_request,input_status,reset)",
        dcst_tag, safety_function, input_type, discrepancy_time, restart_type, cold_start_type, channel_A, channel_B,
        test_request, input_status, reset);

    /// <summary>
    /// Gets the <c>DCSTM</c> instruction definition instance.
    /// </summary>
    public static Instruction DCSTM(Argument dcstm_tag, Argument safety_function, Argument input_type,
        Argument discrepancy_time, Argument restart_type, Argument cold_start_type, Argument test_type,
        Argument test_time, Argument channel_A, Argument channel_B, Argument test_request, Argument mute,
        Argument muting_lamp_status, Argument input_status, Argument reset) => new(nameof(DCSTM),
        "DCSTM(dcstm_tag,safety_function,input_type,discrepancy_time,restart_type,cold_start_type,test_type,test_time,channel_A,channel_B,test_request,mute,muting_lamp_status,input_status,reset)",
        dcstm_tag, safety_function, input_type, discrepancy_time, restart_type, cold_start_type, test_type, test_time,
        channel_A, channel_B, test_request, mute, muting_lamp_status, input_status, reset);

    /// <summary>
    /// Gets the <c>DCSTL</c> instruction definition instance.
    /// </summary>
    public static Instruction DCSTL(Argument dcstl_tag, Argument safety_function, Argument input_type,
        Argument discrepancy_time, Argument restart_type, Argument cold_start_type, Argument channel_A,
        Argument channel_B, Argument test_request, Argument unlock_request, Argument lock_feedback,
        Argument hazard_stopped, Argument input_status, Argument reset) => new(nameof(DCSTL),
        "DCSTL(dcstl_tag,safety_function,input_type,discrepancy_time,restart_type,cold_start_type,channel_A,channel_B,test_request,unlock_request,lock_feedback,hazard_stopped,input_status,reset)",
        dcstl_tag, safety_function, input_type, discrepancy_time, restart_type, cold_start_type, channel_A, channel_B,
        test_request, unlock_request, lock_feedback, hazard_stopped, input_status, reset);

    /// <summary>
    /// Gets the <c>DDT</c> instruction definition instance.
    /// </summary>
    public static Instruction DDT(Argument source, Argument reference, Argument result, Argument cmp_control,
        Argument length, Argument position, Argument result_control, Argument result_length,
        Argument result_position) => new(nameof(DDT),
        "DDT(source,reference,result,cmp_control,length,position,result_control,length,position)", source, reference,
        result, cmp_control, length, position, result_control, result_length, result_position);

    /// <summary>
    /// Gets the <c>DEG</c> instruction definition instance.
    /// </summary>
    public static Instruction DEG(Argument source, Argument destination) =>
        new(nameof(DEG), "DEG(source,destination)", source, destination);

    /// <summary>
    /// Gets the <c>DELETE</c> instruction definition instance.
    /// </summary>
    public static Instruction DELETE(Argument source, Argument quantity, Argument start, Argument destination) =>
        new(nameof(DELETE), "DELETE(source,quantity,start,destination)", source, quantity, start, destination);

    /// <summary>
    /// Gets the <c>DIN</c> instruction definition instance.
    /// </summary>
    public static Instruction DIN(Argument din_tag, Argument reset_type, Argument channel_A, Argument channel_B,
        Argument circuit_reset, Argument fault_reset) => new(nameof(DIN),
        "DIN(din_tag,reset_type,channel_A,channel_B,circuit_reset,fault_reset)", din_tag, reset_type, channel_A,
        channel_B, circuit_reset, fault_reset);

    /// <summary>
    /// Gets the <c>DIV</c> instruction definition instance.
    /// </summary>
    public static Instruction DIV(Argument source_A, Argument source_B, Argument destination) =>
        new(nameof(DIV), "DIV(source_A,source_B,destination)", source_A, source_B, destination);

    /// <summary>
    /// Gets the <c>DTOS</c> instruction definition instance.
    /// </summary>
    public static Instruction DTOS(Argument source, Argument destination) =>
        new(nameof(DTOS), "DTOS(source,destination)", source, destination);

    /// <summary>
    /// Gets the <c>DTR</c> instruction definition instance.
    /// </summary>
    public static Instruction DTR(Argument source, Argument mask, Argument reference) =>
        new(nameof(DTR), "DTR(source,mask,reference)", source, mask, reference);

    /// <summary>
    /// Gets the <c>ENPEN</c> instruction definition instance.
    /// </summary>
    public static Instruction ENPEN(Argument enpen_tag, Argument reset_type, Argument channel_A, Argument channel_B,
        Argument circuit_reset, Argument fault_reset) => new(nameof(ENPEN),
        "ENPEN(enpen_tag,reset_type,channel_A,channel_B,circuit_reset,fault_reset)", enpen_tag, reset_type, channel_A,
        channel_B, circuit_reset, fault_reset);

    /// <summary>
    /// Gets the <c>EOT</c> instruction definition instance.
    /// </summary>
    public static Instruction EOT(Argument data_bit) => new(nameof(EOT), "EOT(data_bit)", data_bit);

    /// <summary>
    /// Gets the <c>EPMS</c> instruction definition instance.
    /// </summary>
    public static Instruction EPMS(Argument epms_tag, Argument input_1, Argument input_2, Argument input_3,
        Argument input_4, Argument input_5, Argument input_6, Argument input_7, Argument input_8, Argument input_status,
        Argument lck, Argument reset) => new(nameof(EPMS),
        "EPMS(epms_tag,input_1,input_2,input_3,input_4,input_5,input_6,input_7,input_8,input_status,lock,reset)",
        epms_tag, input_1, input_2, input_3, input_4, input_5, input_6, input_7, input_8, input_status, lck, reset);

    /// <summary>
    /// Gets the <c>EQU</c> instruction definition instance.
    /// </summary>
    public static Instruction EQU(Argument source_A, Argument source_B) =>
        new(nameof(EQU), "EQU(source_A,source_B)", source_A, source_B);

    /// <summary>
    /// Gets the <c>EQ</c> instruction definition instance.
    /// </summary>
    public static Instruction EQ(Argument source_A, Argument source_B) =>
        new(nameof(EQ), "EQ(source_A,source_B)", source_A, source_B);

    /// <summary>
    /// Gets the <c>ESTOP</c> instruction definition instance.
    /// </summary>
    public static Instruction ESTOP(Argument estop_tag, Argument reset_type, Argument channel_A, Argument channel_B,
        Argument circuit_reset, Argument fault_reset) => new(nameof(ESTOP),
        "ESTOP(estop_tag,reset_type,channel_A,channel_B,circuit_reset,fault_reset)", estop_tag, reset_type, channel_A,
        channel_B, circuit_reset, fault_reset);

    /// <summary>
    /// Gets the <c>EVENT</c> instruction definition instance.
    /// </summary>
    public static Instruction EVENT(Argument task) => new(nameof(EVENT), "EVENT(task)", task);

    /// <summary>
    /// Gets the <c>FAL</c> instruction definition instance.
    /// </summary>
    public static Instruction FAL(Argument control, Argument length, Argument position, Argument mode,
        Argument destination, Argument expression) =>
        new(nameof(FAL), "FAL(control,length,position,mode,destination,expression)", control, length, position, mode,
            destination, expression);

    /// <summary>
    /// Gets the <c>FBC</c> instruction definition instance.
    /// </summary>
    public static Instruction FBC(Argument source, Argument reference, Argument result, Argument cmp_control,
        Argument length, Argument position, Argument result_control, Argument result_length,
        Argument result_position) => new(nameof(FBC),
        "FBC(source,reference,result,cmp_control,length,position,result_control,length,position)", source, reference,
        result, cmp_control, length, position, result_control, result_length, result_position);

    /// <summary>
    /// Gets the <c>FFL</c> instruction definition instance.
    /// </summary>
    public static Instruction FFL(Argument source, Argument FIFO, Argument control, Argument length,
        Argument position) =>
        new(nameof(FFL), "FFL(source,FIFO,control,length,position)", source, FIFO, control, length, position);

    /// <summary>
    /// Gets the <c>FFU</c> instruction definition instance.
    /// </summary>
    public static Instruction FFU(Argument FIFO, Argument destination, Argument control, Argument length,
        Argument position) =>
        new(nameof(FFU), "FFU(FIFO,destination,control,length,position)", FIFO, destination, control, length,
            position);

    /// <summary>
    /// Gets the <c>FIND</c> instruction definition instance.
    /// </summary>
    public static Instruction FIND(Argument source, Argument search, Argument start, Argument result) =>
        new(nameof(FIND), "FIND(source,search,start,result)", source, search, start, result);

    /// <summary>
    /// Gets the <c>FLL</c> instruction definition instance.
    /// </summary>
    public static Instruction FLL(Argument source, Argument destination, Argument length) => new(nameof(FLL),
        "FLL(source,destination,length)", source, destination, length);

    /// <summary>
    /// Gets the <c>FOR</c> instruction definition instance.
    /// </summary>
    public static Instruction FOR(Argument routine_name, Argument index, Argument initial_value,
        Argument terminal_value, Argument step_size) => new(nameof(FOR),
        "FOR(routine_name,index,initial_value,terminal_value,step_size)", routine_name, index, initial_value,
        terminal_value, step_size);

    /// <summary>
    /// Gets the <c>FPMS</c> instruction definition instance.
    /// </summary>
    public static Instruction FPMS(Argument fpms_tag, Argument input_1, Argument input_2, Argument input_3,
        Argument input_4, Argument input_5, Argument fault_reset) => new(nameof(FPMS),
        "FPMS(fpms_tag,input_1,input_2,input_3,input_4,input_5,fault_reset)", fpms_tag, input_1, input_2, input_3,
        input_4, input_5, fault_reset);

    /// <summary>
    /// Gets the <c>FRD</c> instruction definition instance.
    /// </summary>
    public static Instruction FRD(Argument source, Argument destination) =>
        new(nameof(FRD), "FRD(source,destination)", source, destination);

    /// <summary>
    /// Gets the <c>BCD_TO</c> instruction definition instance.
    /// </summary>
    public static Instruction BCD_TO(Argument source, Argument destination) =>
        new(nameof(BCD_TO), "BCD_TO(source,destination)", source, destination);

    /// <summary>
    /// Gets the <c>FSBM</c> instruction definition instance.
    /// </summary>
    public static Instruction FSBM(Argument fsbm_tag, Argument restart_type, Argument S1_S2_time, Argument S2_LC_time,
        Argument LC_S3_time, Argument S3_S4_time, Argument maximum_mute_time, Argument maximum_override_time,
        Argument direction, Argument light_curtain, Argument sensor_1, Argument sensor_2, Argument sensor_3,
        Argument sensor_4, Argument enable_mute, Argument @override, Argument input_status, Argument muting_lamp_status,
        Argument reset) => new(nameof(FSBM),
        "FSBM(fsbm_tag,restart_type,S1_S2_time,S2_LC_time,LC_S3_time,S3_S4_time,maximum_mute_time,maximum_override_time,direction,light_curtain,sensor_1,sensor_2,sensor_3,sensor_4,enable_mute,override,input_status,muting_lamp_status,reset)",
        fsbm_tag, restart_type, S1_S2_time, S2_LC_time, LC_S3_time, S3_S4_time, maximum_mute_time,
        maximum_override_time, direction, light_curtain, sensor_1, sensor_2, sensor_3, sensor_4, enable_mute, @override,
        input_status, muting_lamp_status, reset);

    /// <summary>
    /// Gets the <c>FSC</c> instruction definition instance.
    /// </summary>
    public static Instruction FSC(Argument control, Argument length, Argument position, Argument mode,
        Argument expression) =>
        new(nameof(FSC), "FSC(control,length,position,mode,expression)", control, length, position, mode, expression);

    /// <summary>
    /// Gets the <c>GEQ</c> instruction definition instance.
    /// </summary>
    public static Instruction GEQ(Argument source_A, Argument source_B) =>
        new(nameof(GEQ), "GEQ(source_A,source_B)", source_A, source_B);

    /// <summary>
    /// Gets the <c>GE</c> instruction definition instance.
    /// </summary>
    public static Instruction GE(Argument source_A, Argument source_B) =>
        new(nameof(GE), "GE(source_A,source_B)", source_A, source_B);

    /// <summary>
    /// Gets the <c>GRT</c> instruction definition instance.
    /// </summary>
    public static Instruction GRT(Argument source_A, Argument source_B) =>
        new(nameof(GRT), "GRT(source_A,source_B)", source_A, source_B);

    /// <summary>
    /// Gets the <c>GT</c> instruction definition instance.
    /// </summary>
    public static Instruction GT(Argument source_A, Argument source_B) =>
        new(nameof(GT), "GT(source_A,source_B)", source_A, source_B);

    /// <summary>
    /// Gets the <c>GSV</c> instruction definition instance.
    /// </summary>
    public static Instruction GSV(Argument class_name, Argument instance_name, Argument attribute_name,
        Argument destination) =>
        new(nameof(GSV), "GSV(class_name,instance_name,attribute_name,destination)", class_name, instance_name,
            attribute_name, destination);

    /// <summary>
    /// Gets the <c>INSERT</c> instruction definition instance.
    /// </summary>
    public static Instruction INSERT(Argument sourceA, Argument sourceB, Argument start, Argument destination) =>
        new(nameof(INSERT), "INSERT(sourceA,sourceB,start,destination)", sourceA, sourceB, start, destination);

    /// <summary>
    /// Gets the <c>IOT</c> instruction definition instance.
    /// </summary>
    public static Instruction IOT(Argument output_tag) => new(nameof(IOT), "IOT(output_tag)", output_tag);

    /// <summary>
    /// Gets the <c>JMP</c> instruction definition instance.
    /// </summary>
    public static Instruction JMP(Argument label_name) => new(nameof(JMP), "JMP(label_name)", label_name);

    /// <summary>
    /// Gets the <c>JSR</c> instruction definition instance.
    /// </summary>
    public static Instruction JSR(Argument routine_name, Argument number_of_inputs, params Argument[]? parameters) =>
        new(nameof(JSR), "JSR(routine_name,number_of_inputs,input_1,input_n,return_1,return_n)",
            parameters is not null
                ? new[] { routine_name, number_of_inputs }.Concat(parameters).ToArray()
                : new[] { routine_name, number_of_inputs });

    /// <summary>
    /// Gets the <c>JXR</c> instruction definition instance.
    /// </summary>
    public static Instruction JXR(Argument external_routine_name, Argument external_routine_control, Argument parameter,
        Argument return_parameter) => new(nameof(JXR),
        "JXR(external_routine_name,external_routine_control,parameter,return_parameter)", external_routine_name,
        external_routine_control, parameter, return_parameter);

    /// <summary>
    /// Gets the <c>LBL</c> instruction definition instance.
    /// </summary>
    public static Instruction LBL(Argument label_name) => new(nameof(LBL), "LBL(label_name)", label_name);

    /// <summary>
    /// Gets the <c>LC</c> instruction definition instance.
    /// </summary>
    public static Instruction LC(Argument lc_tag, Argument reset_type, Argument channel_A, Argument channel_B,
        Argument input_filter_time, Argument mute_light_curtain, Argument circuit_reset, Argument fault_reset) => new(
        nameof(LC),
        "LC(lc_tag,reset_type,channel_A,channel_B,input_filter_time,mute_light_curtain,circuit_reset,fault_reset)",
        lc_tag, reset_type, channel_A, channel_B, input_filter_time, mute_light_curtain, circuit_reset, fault_reset);

    /// <summary>
    /// Gets the <c>LEQ</c> instruction definition instance.
    /// </summary>
    public static Instruction LEQ(Argument source_A, Argument source_B) =>
        new(nameof(LEQ), "LEQ(source_A,source_B)", source_A, source_B);

    /// <summary>
    /// Gets the <c>LE</c> instruction definition instance.
    /// </summary>
    public static Instruction LE(Argument source_A, Argument source_B) =>
        new(nameof(LE), "LE(source_A,source_B)", source_A, source_B);

    /// <summary>
    /// Gets the <c>LES</c> instruction definition instance.
    /// </summary>
    public static Instruction LES(Argument source_A, Argument source_B) =>
        new(nameof(LES), "LES(source_A,source_B)", source_A, source_B);

    /// <summary>
    /// Gets the <c>LT</c> instruction definition instance.
    /// </summary>
    public static Instruction LT(Argument source_A, Argument source_B) =>
        new(nameof(LT), "LT(source_A,source_B)", source_A, source_B);

    /// <summary>
    /// Gets the <c>LFL</c> instruction definition instance.
    /// </summary>
    public static Instruction LFL(Argument source, Argument LIFO, Argument control, Argument length,
        Argument position) =>
        new(nameof(LFL), "LFL(source,LIFO,control,length,position)", source, LIFO, control, length, position);

    /// <summary>
    /// Gets the <c>LFU</c> instruction definition instance.
    /// </summary>
    public static Instruction LFU(Argument LIFO, Argument destination, Argument control, Argument length,
        Argument position) =>
        new(nameof(LFU), "LFU(LIFO,destination,control,length,position)", LIFO, destination, control, length,
            position);

    /// <summary>
    /// Gets the <c>LIM</c> instruction definition instance.
    /// </summary>
    public static Instruction LIM(Argument low_limit, Argument test, Argument high_limit) => new(nameof(LIM),
        "LIM(low_limit,test,high_limit)", low_limit, test, high_limit);

    /// <summary>
    /// Gets the <c>LIMIT</c> instruction definition instance.
    /// </summary>
    public static Instruction LIMIT(Argument low_limit, Argument test, Argument high_limit) => new(nameof(LIMIT),
        "LIMIT(low_limit,test,high_limit)", low_limit, test, high_limit);

    /// <summary>
    /// Gets the <c>LN</c> instruction definition instance.
    /// </summary>
    public static Instruction LN(Argument source, Argument destination) =>
        new(nameof(LN), "LN(source,destination)", source, destination);

    /// <summary>
    /// Gets the <c>LOG</c> instruction definition instance.
    /// </summary>
    public static Instruction LOG(Argument source, Argument destination) =>
        new(nameof(LOG), "LOG(source,destination)", source, destination);

    /// <summary>
    /// Gets the <c>LOWER</c> instruction definition instance.
    /// </summary>
    public static Instruction LOWER(Argument source, Argument destination) =>
        new(nameof(LOWER), "LOWER(source,destination)", source, destination);

    /// <summary>
    /// Gets the <c>MAAT</c> instruction definition instance.
    /// </summary>
    public static Instruction MAAT(Argument axis, Argument motion_control) =>
        new(nameof(MAAT), "MAAT(axis,motion_control)", axis, motion_control);

    /// <summary>
    /// Gets the <c>MAFR</c> instruction definition instance.
    /// </summary>
    public static Instruction MAFR(Argument axis, Argument motion_control) =>
        new(nameof(MAFR), "MAFR(axis,motion_control)", axis, motion_control);

    /// <summary>
    /// Gets the <c>MAG</c> instruction definition instance.
    /// </summary>
    public static Instruction MAG(Argument slave_axis, Argument master_axis, Argument motion_control,
        Argument direction, Argument ratio, Argument slave_counts, Argument master_counts, Argument master_reference,
        Argument ratio_format, Argument clutch, Argument accel_rate, Argument accel_units) => new(nameof(MAG),
        "MAG(slave_axis,master_axis,motion_control,direction,ratio,slave_counts,master_counts,master_reference,ratio_format,clutch,accel_rate,accel_units)",
        slave_axis, master_axis, motion_control, direction, ratio, slave_counts, master_counts, master_reference,
        ratio_format, clutch, accel_rate, accel_units);

    /// <summary>
    /// Gets the <c>MAH</c> instruction definition instance.
    /// </summary>
    public static Instruction MAH(Argument axis, Argument motion_control) =>
        new(nameof(MAH), "MAH(axis,motion_control)", axis, motion_control);

    /// <summary>
    /// Gets the <c>MAHD</c> instruction definition instance.
    /// </summary>
    public static Instruction MAHD(Argument axis, Argument motion_control, Argument diagnostic_test,
        Argument observed_direction) => new(nameof(MAHD),
        "MAHD(axis,motion_control,diagnostic_test,observed_direction)", axis, motion_control, diagnostic_test,
        observed_direction);

    /// <summary>
    /// Gets the <c>MAJ</c> instruction definition instance.
    /// </summary>
    public static Instruction MAJ(Argument axis, Argument motion_control, Argument direction, Argument speed,
        Argument speed_units, Argument accel_rate, Argument accel_units, Argument decel_rate, Argument decel_units,
        Argument profile, Argument merge, Argument merge_speed) => new(nameof(MAJ),
        "MAJ(axis,motion_control,direction,speed,speed_units,accel_rate,accel_units,decel_rate,decel_units,profile,merge,merge_speed)",
        axis, motion_control, direction, speed, speed_units, accel_rate, accel_units, decel_rate, decel_units, profile,
        merge, merge_speed);

    /// <summary>
    /// Gets the <c>MAM</c> instruction definition instance.
    /// </summary>
    public static Instruction MAM(Argument axis, Argument motion_control, Argument move_type, Argument position,
        Argument speed, Argument speed_units, Argument accel_rate, Argument accel_units, Argument decel_rate,
        Argument decel_units, Argument profile, Argument merge, Argument merge_speed) => new(nameof(MAM),
        "MAM(axis,motion_control,move_type,position,speed,speed_units,accel_rate,accel_units,decel_rate,decel_units,profile,merge,merge_speed)",
        axis, motion_control, move_type, position, speed, speed_units, accel_rate, accel_units, decel_rate, decel_units,
        profile, merge, merge_speed);

    /// <summary>
    /// Gets the <c>MAOC</c> instruction definition instance.
    /// </summary>
    public static Instruction MAOC(Argument axis, Argument execution_target, Argument motion_control, Argument output,
        Argument input, Argument output_cam, Argument cam_start_position, Argument cam_end_position,
        Argument output_compensation, Argument execution_mode, Argument execution_schedule, Argument axis_arm_position,
        Argument cam_arm_position, Argument reference) => new(nameof(MAOC),
        "MAOC(axis,execution_target,motion_control,output,input,output_cam,cam_start_position,cam_end_position,output_compensation,execution_mode,execution_schedule,axis_arm_position,cam_arm_position,reference)",
        axis, execution_target, motion_control, output, input, output_cam, cam_start_position, cam_end_position,
        output_compensation, execution_mode, execution_schedule, axis_arm_position, cam_arm_position, reference);

    /// <summary>
    /// Gets the <c>MAPC</c> instruction definition instance.
    /// </summary>
    public static Instruction MAPC(Argument slave_axis, Argument master_axis, Argument motion_control,
        Argument direction, Argument cam_profile, Argument slave_scaling, Argument master_scaling,
        Argument execution_mode, Argument execution_schedule, Argument master_lock_position, Argument cam_lock_position,
        Argument master_reference, Argument master_direction) => new(nameof(MAPC),
        "MAPC(slave_axis,master_axis,motion_control,direction,cam_profile,slave_scaling,master_scaling,execution_mode,execution_schedule,master_lock_position,cam_lock_position,master_reference,master_direction)",
        slave_axis, master_axis, motion_control, direction, cam_profile, slave_scaling, master_scaling, execution_mode,
        execution_schedule, master_lock_position, cam_lock_position, master_reference, master_direction);

    /// <summary>
    /// Gets the <c>MAR</c> instruction definition instance.
    /// </summary>
    public static Instruction MAR(Argument axis, Argument motion_control, Argument trigger_condition,
        Argument windowed_registration, Argument minimum_position, Argument maximum_position) => new(nameof(MAR),
        "MAR(axis,motion_control,trigger_condition,windowed_registration,minimum_position,maximum_position)", axis,
        motion_control, trigger_condition, windowed_registration, minimum_position, maximum_position);

    /// <summary>
    /// Gets the <c>MAS</c> instruction definition instance.
    /// </summary>
    public static Instruction MAS(Argument axis, Argument motion_control, Argument stop_type, Argument change_decel,
        Argument decel_rate, Argument decel_units) => new(nameof(MAS),
        "MAS(axis,motion_control,stop_type,change_decel,decel_rate,decel_units)", axis, motion_control, stop_type,
        change_decel, decel_rate, decel_units);

    /// <summary>
    /// Gets the <c>MASD</c> instruction definition instance.
    /// </summary>
    public static Instruction MASD(Argument axis, Argument motion_control) =>
        new(nameof(MASD), "MASD(axis,motion_control)", axis, motion_control);

    /// <summary>
    /// Gets the <c>MASR</c> instruction definition instance.
    /// </summary>
    public static Instruction MASR(Argument axis, Argument motion_control) =>
        new(nameof(MASR), "MASR(axis,motion_control)", axis, motion_control);

    /// <summary>
    /// Gets the <c>MATC</c> instruction definition instance.
    /// </summary>
    public static Instruction MATC(Argument axis, Argument motion_control, Argument direction, Argument cam_profile,
        Argument distance_scaling, Argument time_scaling, Argument execution_mode, Argument execution_schedule) => new(
        nameof(MATC),
        "MATC(axis,motion_control,direction,cam_profile,distance_scaling,time_scaling,execution_mode,execution_schedule)",
        axis, motion_control, direction, cam_profile, distance_scaling, time_scaling, execution_mode,
        execution_schedule);

    /// <summary>
    /// Gets the <c>MAW</c> instruction definition instance.
    /// </summary>
    public static Instruction MAW(Argument axis, Argument motion_control, Argument trigger_condition,
        Argument position) =>
        new(nameof(MAW), "MAW(axis,motion_control,trigger_condition,position)", axis, motion_control,
            trigger_condition, position);

    /// <summary>
    /// Gets the <c>MCCD</c> instruction definition instance.
    /// </summary>
    public static Instruction MCCD(Argument coordinate_system, Argument motion_control, Argument motion_type,
        Argument change_speed, Argument speed, Argument speed_units, Argument change_accel, Argument accel_rate,
        Argument accel_units, Argument change_decel, Argument decel_rate, Argument decel_units, Argument scope) => new(
        nameof(MCCD),
        "MCCD(coordinate_system,motion_control,motion_type,change_speed,speed,speed_units,change_accel,accel_rate,accel_units,change_decel,decel_rate, decel_units,scope)",
        coordinate_system, motion_control, motion_type, change_speed, speed, speed_units, change_accel, accel_rate,
        accel_units, change_decel, decel_rate, decel_units, scope);

    /// <summary>
    /// Gets the <c>MCCM</c> instruction definition instance.
    /// </summary>
    public static Instruction MCCM(Argument coordinate_system, Argument motion_control, Argument move_type,
        Argument position, Argument circle_type, Argument radius, Argument direction, Argument speed,
        Argument speed_units, Argument accel_rate, Argument accel_units, Argument decel_rate, Argument decel_units,
        Argument profile, Argument termination_type, Argument merge, Argument merge_speed) => new(nameof(MCCM),
        "MCCM(coordinate_system,motion_control,move_type,position,circle_type,via/center/radius,direction,speed,speed_units,accel_rate,accel_units,decel_rate,decel_units,profile,termination_type,merge,merge_speed)",
        coordinate_system, motion_control, move_type, position, circle_type, radius, direction, speed, speed_units,
        accel_rate, accel_units, decel_rate, decel_units, profile, termination_type, merge, merge_speed);

    /// <summary>
    /// Gets the <c>MCCP</c> instruction definition instance.
    /// </summary>
    public static Instruction MCCP(Argument motion_control, Argument cam, Argument length, Argument start_slope,
        Argument end_slope, Argument cam_profile) => new(nameof(MCCP),
        "MCCP(motion_control,cam,length,start_slope,end_slope,cam_profile)", motion_control, cam, length, start_slope,
        end_slope, cam_profile);

    /// <summary>
    /// Gets the <c>MCLM</c> instruction definition instance.
    /// </summary>
    public static Instruction MCLM(Argument coordinate_system, Argument motion_control, Argument move_type,
        Argument position, Argument speed, Argument speed_units, Argument accel_rate, Argument accel_units,
        Argument decel_rate, Argument decel_units, Argument profile, Argument termination_type, Argument merge,
        Argument merge_speed) => new(nameof(MCLM),
        "MCLM(coordinate_system,motion_control,move_type,position,speed,speed_units,accel_rate,accel_units,decel_rate,decel_units,profile, termination_type,merge,merge_speed)",
        coordinate_system, motion_control, move_type, position, speed, speed_units, accel_rate, accel_units, decel_rate,
        decel_units, profile, termination_type, merge, merge_speed);

    /// <summary>
    /// Gets the <c>MCD</c> instruction definition instance.
    /// </summary>
    public static Instruction MCD(Argument axis, Argument motion_control, Argument motion_type, Argument change_speed,
        Argument speed, Argument change_accel, Argument accel_rate, Argument change_decel, Argument decel_rate,
        Argument speed_units, Argument accel_units, Argument decel_units) => new(nameof(MCD),
        "MCD(axis,motion_control,motion_type,change_speed,speed,change_accel,accel_rate,change_decel,decel_rate,speed_units,accel_units,decel_units)",
        axis, motion_control, motion_type, change_speed, speed, change_accel, accel_rate, change_decel, decel_rate,
        speed_units, accel_units, decel_units);

    /// <summary>
    /// Gets the <c>MCR</c> instruction definition instance.
    /// </summary>
    public static Instruction MCR() => new(nameof(MCR), "MCR()");

    /// <summary>
    /// Gets the <c>MCS</c> instruction definition instance.
    /// </summary>
    public static Instruction MCS(Argument coordinate_system, Argument motion_control, Argument stop_type,
        Argument change_decel, Argument decel_rate, Argument decel_units) => new(nameof(MCS),
        "MCS(coordinate_system,motion_control,stop_type,change_decel,decel_rate, decel_units)", coordinate_system,
        motion_control, stop_type, change_decel, decel_rate, decel_units);

    /// <summary>
    /// Gets the <c>MCSD</c> instruction definition instance.
    /// </summary>
    public static Instruction MCSD(Argument coordinate_system, Argument motion_control) =>
        new(nameof(MCSD), "MCSD(coordinate_system,motion_control)", coordinate_system, motion_control);

    /// <summary>
    /// Gets the <c>MCSR</c> instruction definition instance.
    /// </summary>
    public static Instruction MCSR(Argument coordinate_system, Argument motion_control) =>
        new(nameof(MCSR), "MCSR(coordinate_system,motion_control)", coordinate_system, motion_control);

    /// <summary>
    /// Gets the <c>MCSV</c> instruction definition instance.
    /// </summary>
    public static Instruction MCSV(Argument motion_control, Argument cam_profile, Argument master_value,
        Argument slave_value, Argument slope_value, Argument slope_derivative) => new(nameof(MCSV),
        "MCSV(motion_control,cam_profile,master_value,slave_value,slope_value,slope_derivative)", motion_control,
        cam_profile, master_value, slave_value, slope_value, slope_derivative);

    /// <summary>
    /// Gets the <c>MCT</c> instruction definition instance.
    /// </summary>
    public static Instruction MCT(Argument source_system, Argument target_system, Argument motion_control,
        Argument orientation, Argument translation) => new(nameof(MCT),
        "MCT(source_system,target_system,motion_control,orientation,translation)", source_system, target_system,
        motion_control, orientation, translation);

    /// <summary>
    /// Gets the <c>MCTP</c> instruction definition instance.
    /// </summary>
    public static Instruction MCTP(Argument source_system, Argument target_system, Argument motion_control,
        Argument orientation, Argument translation, Argument transform_direction, Argument reference_position,
        Argument transform_position) => new(nameof(MCTP),
        "MCTP(source_system,target_system,motion_control,orientation,translation,transform_direction,reference_position,transform_position)",
        source_system, target_system, motion_control, orientation, translation, transform_direction, reference_position,
        transform_position);

    /// <summary>
    /// Gets the <c>MDF</c> instruction definition instance.
    /// </summary>
    public static Instruction MDF(Argument axis, Argument motion_control) =>
        new(nameof(MDF), "MDF(axis,motion_control)", axis, motion_control);

    /// <summary>
    /// Gets the <c>MDO</c> instruction definition instance.
    /// </summary>
    public static Instruction MDO(Argument axis, Argument motion_control, Argument drive_output,
        Argument drive_units) =>
        new(nameof(MDO), "MDO(axis,motion_control,drive_output,drive_units)", axis, motion_control, drive_output,
            drive_units);

    /// <summary>
    /// Gets the <c>MDOC</c> instruction definition instance.
    /// </summary>
    public static Instruction MDOC(Argument axis, Argument execution_target, Argument motion_control,
        Argument disarm_type) =>
        new(nameof(MDOC), "MDOC(axis,execution_target,motion_control,disarm_type)", axis, execution_target,
            motion_control, disarm_type);

    /// <summary>
    /// Gets the <c>MDR</c> instruction definition instance.
    /// </summary>
    public static Instruction MDR(Argument axis, Argument motion_control) =>
        new(nameof(MDR), "MDR(axis,motion_control)", axis, motion_control);

    /// <summary>
    /// Gets the <c>MDW</c> instruction definition instance.
    /// </summary>
    public static Instruction MDW(Argument axis, Argument motion_control) =>
        new(nameof(MDW), "MDW(axis,motion_control)", axis, motion_control);

    /// <summary>
    /// Gets the <c>MEQ</c> instruction definition instance.
    /// </summary>
    public static Instruction MEQ(Argument source, Argument mask, Argument compare) =>
        new(nameof(MEQ), "MEQ(source,mask,compare)", source, mask, compare);

    /// <summary>
    /// Gets the <c>MGS</c> instruction definition instance.
    /// </summary>
    public static Instruction MGS(Argument group, Argument motion_control, Argument stop_mode) =>
        new(nameof(MGS), "MGS(group,motion_control,stop_mode)", group, motion_control, stop_mode);

    /// <summary>
    /// Gets the <c>MGSD</c> instruction definition instance.
    /// </summary>
    public static Instruction MGSD(Argument group, Argument motion_control) =>
        new(nameof(MGSD), "MGSD(group,motion_control)", group, motion_control);

    /// <summary>
    /// Gets the <c>MGSP</c> instruction definition instance.
    /// </summary>
    public static Instruction MGSP(Argument group, Argument motion_control) =>
        new(nameof(MGSP), "MGSP(group,motion_control)", group, motion_control);

    /// <summary>
    /// Gets the <c>MGSR</c> instruction definition instance.
    /// </summary>
    public static Instruction MGSR(Argument group, Argument motion_control) =>
        new(nameof(MGSR), "MGSR(group,motion_control)", group, motion_control);

    /// <summary>
    /// Gets the <c>MID</c> instruction definition instance.
    /// </summary>
    public static Instruction MID(Argument source, Argument quantity, Argument start, Argument destination) =>
        new(nameof(MID), "MID(source,quantity,start,destination)", source, quantity, start, destination);

    /// <summary>
    /// Gets the <c>MMVC</c> instruction definition instance.
    /// </summary>
    public static Instruction MMVC(Argument mmvc_tag, Argument enable, Argument keyswitch, Argument bottom,
        Argument flywheel_stopped, Argument safety_enable, Argument actuate, Argument input_status,
        Argument output_status, Argument reset) => new(nameof(MMVC),
        "MMVC(mmvc_tag,enable,keyswitch,bottom,flywheel_stopped,safety_enable,actuate,input_status,output_status,reset)",
        mmvc_tag, enable, keyswitch, bottom, flywheel_stopped, safety_enable, actuate, input_status, output_status,
        reset);

    /// <summary>
    /// Gets the <c>MOD</c> instruction definition instance.
    /// </summary>
    public static Instruction MOD(Argument source_A, Argument source_B, Argument destination) =>
        new(nameof(MOD), "MOD(source_A,source_B,destination)", source_A, source_B, destination);

    /// <summary>
    /// Gets the <c>MOV</c> instruction definition instance.
    /// </summary>
    public static Instruction MOV(Argument source, Argument destination) =>
        new(nameof(MOV), "MOV(source,destination)", source, destination);

    /// <summary>
    /// Gets the <c>MOVE</c> instruction definition instance.
    /// </summary>
    public static Instruction MOVE(Argument source, Argument destination) =>
        new(nameof(MOVE), "MOVE(source,destination)", source, destination);

    /// <summary>
    /// Gets the <c>MRAT</c> instruction definition instance.
    /// </summary>
    public static Instruction MRAT(Argument axis, Argument motion_control) =>
        new(nameof(MRAT), "MRAT(axis,motion_control)", axis, motion_control);

    /// <summary>
    /// Gets the <c>MRHD</c> instruction definition instance.
    /// </summary>
    public static Instruction MRHD(Argument axis, Argument motion_control, Argument diagnostic_test) =>
        new(nameof(MRHD), "MRHD(axis,motion_control,diagnostic_test)", axis, motion_control, diagnostic_test);

    /// <summary>
    /// Gets the <c>MRP</c> instruction definition instance.
    /// </summary>
    public static Instruction MRP(Argument axis, Argument motion_control, Argument type, Argument position_select,
        Argument position) =>
        new(nameof(MRP), "MRP(axis,motion_control,type,position_select,position)", axis, motion_control, type,
            position_select, position);

    /// <summary>
    /// Gets the <c>MSF</c> instruction definition instance.
    /// </summary>
    public static Instruction MSF(Argument axis, Argument motion_control) =>
        new(nameof(MSF), "MSF(axis,motion_control)", axis, motion_control);

    /// <summary>
    /// Gets the <c>MSG</c> instruction definition instance.
    /// </summary>
    public static Instruction MSG(Argument message_control) =>
        new(nameof(MSG), "MSG(message_control)", message_control);

    /// <summary>
    /// Gets the <c>MSO</c> instruction definition instance.
    /// </summary>
    public static Instruction MSO(Argument axis, Argument motion_control) =>
        new(nameof(MSO), "MSO(axis,motion_control)", axis, motion_control);

    /// <summary>
    /// Gets the <c>MUL</c> instruction definition instance.
    /// </summary>
    public static Instruction MUL(Argument source_A, Argument source_B, Argument destination) =>
        new(nameof(MUL), "MUL(source_A,source_B,destination)", source_A, source_B, destination);

    /// <summary>
    /// Gets the <c>MVC</c> instruction definition instance.
    /// </summary>
    public static Instruction MVC(Argument mvc_tag, Argument feedback_type, Argument feedback_reaction_time,
        Argument actuate, Argument feedback_1, Argument feedback_2, Argument input_status, Argument output_status,
        Argument reset) => new(nameof(MVC),
        "MVC(mvc_tag,feedback_type,feedback_reaction_time,actuate,feedback_1,feedback_2,input_status,output_status,reset)",
        mvc_tag, feedback_type, feedback_reaction_time, actuate, feedback_1, feedback_2, input_status, output_status,
        reset);

    /// <summary>
    /// Gets the <c>MVM</c> instruction definition instance.
    /// </summary>
    public static Instruction MVM(Argument source, Argument mask, Argument destination) =>
        new(nameof(MVM), "MVM(source,mask,destination)", source, mask, destination);

    /// <summary>
    /// Gets the <c>NEG</c> instruction definition instance.
    /// </summary>
    public static Instruction NEG(Argument source, Argument destination) =>
        new(nameof(NEG), "NEG(source,destination)", source, destination);

    /// <summary>
    /// Gets the <c>NEQ</c> instruction definition instance.
    /// </summary>
    public static Instruction NEQ(Argument source_A, Argument source_B) =>
        new(nameof(NEQ), "NEQ(source_A,source_B)", source_A, source_B);

    /// <summary>
    /// Gets the <c>NE</c> instruction definition instance.
    /// </summary>
    public static Instruction NE(Argument source_A, Argument source_B) =>
        new(nameof(NE), "NE(source_A,source_B)", source_A, source_B);

    /// <summary>
    /// Gets the <c>NOP</c> instruction definition instance.
    /// </summary>
    public static Instruction NOP() => new(nameof(NOP), "NOP()");

    /// <summary>
    /// Gets the <c>NOT</c> instruction definition instance.
    /// </summary>
    public static Instruction NOT(Argument source, Argument destination) =>
        new(nameof(NOT), "NOT(source,destination)", source, destination);

    /// <summary>
    /// Gets the <c>ONS</c> instruction definition instance.
    /// </summary>
    public static Instruction ONS(Argument storage_bit) => new(nameof(ONS), "ONS(storage_bit)", storage_bit);

    /// <summary>
    /// Gets the <c>OR</c> instruction definition instance.
    /// </summary>
    public static Instruction OR(Argument source_A, Argument source_B, Argument destination) => new(nameof(OR),
        "OR(source_A,source_B,destination)", source_A, source_B, destination);

    /// <summary>
    /// Gets the <c>OSF</c> instruction definition instance.
    /// </summary>
    public static Instruction OSF(Argument storage_bit, Argument output_bit) =>
        new(nameof(OSF), "OSF(storage_bit,output_bit)", storage_bit, output_bit);

    /// <summary>
    /// Gets the <c>OSR</c> instruction definition instance.
    /// </summary>
    public static Instruction OSR(Argument storage_bit, Argument output_bit) =>
        new(nameof(OSR), "OSR(storage_bit,output_bit)", storage_bit, output_bit);

    /// <summary>
    /// Gets the <c>OTE</c> instruction definition instance.
    /// </summary>
    public static Instruction OTE(Argument data_bit) => new(nameof(OTE), "OTE(data_bit)", data_bit);

    /// <summary>
    /// Gets the <c>OTL</c> instruction definition instance.
    /// </summary>
    public static Instruction OTL(Argument data_bit) => new(nameof(OTL), "OTL(data_bit)", data_bit);

    /// <summary>
    /// Gets the <c>OTU</c> instruction definition instance.
    /// </summary>
    public static Instruction OTU(Argument data_bit) => new(nameof(OTU), "OTU(data_bit)", data_bit);

    /// <summary>
    /// Gets the <c>PATT</c> instruction definition instance.
    /// </summary>
    public static Instruction PATT(Argument phase_name, Argument result) =>
        new(nameof(PATT), "PATT(phase_name,result)", phase_name, result);

    /// <summary>
    /// Gets the <c>PCLF</c> instruction definition instance.
    /// </summary>
    public static Instruction PCLF(Argument phase_name) => new(nameof(PCLF), "PCLF(phase_name)", phase_name);

    /// <summary>
    /// Gets the <c>PCMD</c> instruction definition instance.
    /// </summary>
    public static Instruction PCMD(Argument phase_name, Argument command, Argument result) =>
        new(nameof(PCMD), "PCMD(phase_name,command,result)", phase_name, command, result);

    /// <summary>
    /// Gets the <c>PDET</c> instruction definition instance.
    /// </summary>
    public static Instruction PDET(Argument phase_name) => new(nameof(PDET), "PDET(phase_name)", phase_name);

    /// <summary>
    /// Gets the <c>PFL</c> instruction definition instance.
    /// </summary>
    public static Instruction PFL(Argument source) => new(nameof(PFL), "PFL(source)", source);

    /// <summary>
    /// Gets the <c>PID</c> instruction definition instance.
    /// </summary>
    public static Instruction PID(Argument PID, Argument process_variable, Argument tieback, Argument control_variable,
        Argument pid_master_loop, Argument inhold_bit, Argument inhold_value) => new(nameof(PID),
        "PID(PID,process_variable,tieback,control_variable,pid_master_loop,inhold_bit,inhold_value)", PID,
        process_variable, tieback, control_variable, pid_master_loop, inhold_bit, inhold_value);

    /// <summary>
    /// Gets the <c>POVR</c> instruction definition instance.
    /// </summary>
    public static Instruction POVR(Argument phase_name, Argument command, Argument result) =>
        new(nameof(POVR), "POVR(phase_name,command,result)", phase_name, command, result);

    /// <summary>
    /// Gets the <c>PPD</c> instruction definition instance.
    /// </summary>
    public static Instruction PPD() => new(nameof(PPD), "PPD()");

    /// <summary>
    /// Gets the <c>PRNP</c> instruction definition instance.
    /// </summary>
    public static Instruction PRNP() => new(nameof(PRNP), "PRNP()");

    /// <summary>
    /// Gets the <c>PSC</c> instruction definition instance.
    /// </summary>
    public static Instruction PSC() => new(nameof(PSC), "PSC()");

    /// <summary>
    /// Gets the <c>PXRQ</c> instruction definition instance.
    /// </summary>
    public static Instruction PXRQ(Argument phase_instruction, Argument external_request, Argument data_value) =>
        new(nameof(PXRQ), "PXRQ(phase_instruction,external_request,data_value)", phase_instruction, external_request,
            data_value);

    /// <summary>
    /// Gets the <c>RAD</c> instruction definition instance.
    /// </summary>
    public static Instruction RAD(Argument source, Argument destination) =>
        new(nameof(RAD), "RAD(source,destination)", source, destination);

    /// <summary>
    /// Gets the <c>RES</c> instruction definition instance.
    /// </summary>
    public static Instruction RES(Argument structure) => new(nameof(RES), "RES(structure)", structure);

    /// <summary>
    /// Gets the <c>RET</c> instruction definition instance.
    /// </summary>
    public static Instruction RET(params Argument[] outputs) => new(nameof(RET), "RET(return_1,return_n)", outputs);

    /// <summary>
    /// Gets the <c>RIN</c> instruction definition instance.
    /// </summary>
    public static Instruction RIN(Argument rin_tag, Argument reset_type, Argument channel_A, Argument channel_B,
        Argument circuit_reset, Argument fault_reset) => new(nameof(RIN),
        "RIN(rin_tag,reset_type,channel_A,channel_B,circuit_reset,fault_reset)", rin_tag, reset_type, channel_A,
        channel_B, circuit_reset, fault_reset);

    /// <summary>
    /// Gets the <c>ROUT</c> instruction definition instance.
    /// </summary>
    public static Instruction ROUT(Argument rout_tag, Argument feedback_type, Argument enable, Argument feedback_1,
        Argument feedback_2, Argument fault_reset) => new(nameof(ROUT),
        "ROUT(rout_tag,feedback_type,enable,feedback_1,feedback_2,fault_reset)", rout_tag, feedback_type, enable,
        feedback_1, feedback_2, fault_reset);

    /// <summary>
    /// Gets the <c>RTO</c> instruction definition instance.
    /// </summary>
    public static Instruction RTO(Argument timer, Argument preset, Argument accum) =>
        new(nameof(RTO), "RTO(timer,preset,accum)", timer, preset, accum);

    /// <summary>
    /// Gets the <c>RTOS</c> instruction definition instance.
    /// </summary>
    public static Instruction RTOS(Argument source, Argument destination) =>
        new(nameof(RTOS), "RTOS(source,destination)", source, destination);

    /// <summary>
    /// Gets the <c>SBR</c> instruction definition instance.
    /// </summary>
    public static Instruction SBR(params Argument[] inputs) => new(nameof(SBR), "SBR(input_1,input_n)", inputs);

    /// <summary>
    /// Gets the <c>SFP</c> instruction definition instance.
    /// </summary>
    public static Instruction SFP(Argument SFC_routine_name, Argument target_state) =>
        new(nameof(SFP), "SFP(SFC_routine_name,target_state)", SFC_routine_name, target_state);

    /// <summary>
    /// Gets the <c>SFR</c> instruction definition instance.
    /// </summary>
    public static Instruction SFR(Argument SFC_routine_name, Argument step_name) => new(nameof(SFR),
        "SFR(SFC_routine_name,step_name)", SFC_routine_name, step_name);

    /// <summary>
    /// Gets the <c>SIN</c> instruction definition instance.
    /// </summary>
    public static Instruction SIN(Argument source, Argument destination) =>
        new(nameof(SIN), "SIN(source,destination)", source, destination);

    /// <summary>
    /// Gets the <c>SIZE</c> instruction definition instance.
    /// </summary>
    public static Instruction SIZE(Argument souce, Argument dimension_to_vary, Argument size) =>
        new(nameof(SIZE), "SIZE(souce,dimension_to_vary,size)", souce, dimension_to_vary, size);

    /// <summary>
    /// Gets the <c>SMAT</c> instruction definition instance.
    /// </summary>
    public static Instruction SMAT(Argument smat_tag, Argument restart_type, Argument short_circuit_detect_delay_time,
        Argument channel_A, Argument channel_B, Argument input_status, Argument reset) => new(nameof(SMAT),
        "SMAT(smat_tag,restart_type,short_circuit_detect_delay_time,channel_A,channel_B,input_status,reset)", smat_tag,
        restart_type, short_circuit_detect_delay_time, channel_A, channel_B, input_status, reset);

    /// <summary>
    /// Gets the <c>SQI</c> instruction definition instance.
    /// </summary>
    public static Instruction SQI(Argument array, Argument mask, Argument source, Argument control, Argument length,
        Argument position) =>
        new(nameof(SQI), "SQI(array,mask,source,control,length,position)", array, mask, source, control, length,
            position);

    /// <summary>
    /// Gets the <c>SQL</c> instruction definition instance.
    /// </summary>
    public static Instruction SQL(Argument array, Argument source, Argument control, Argument length,
        Argument position) =>
        new(nameof(SQL), "SQL(array,source,control,length,position)", array, source, control, length, position);

    /// <summary>
    /// Gets the <c>SQO</c> instruction definition instance.
    /// </summary>
    public static Instruction SQO(Argument array, Argument mask, Argument destination, Argument control,
        Argument length, Argument position) =>
        new(nameof(SQO), "SQO(array,mask,destination,control,length,position)", array, mask, destination, control,
            length, position);

    /// <summary>
    /// Gets the <c>SQR</c> instruction definition instance.
    /// </summary>
    public static Instruction SQR(Argument source, Argument destination) =>
        new(nameof(SQR), "SQR(source,destination)", source, destination);

    /// <summary>
    /// Gets the <c>SQRT</c> instruction definition instance.
    /// </summary>
    public static Instruction SQRT(Argument source, Argument destination) =>
        new(nameof(SQRT), "SQRT(source,destination)", source, destination);

    /// <summary>
    /// Gets the <c>SRT</c> instruction definition instance.
    /// </summary>
    public static Instruction SRT(Argument array, Argument dim_to_vary, Argument control, Argument length,
        Argument position) =>
        new(nameof(SRT), "SRT(array,dim_to_vary,control,length,position)", array, dim_to_vary, control, length,
            position);

    /// <summary>
    /// Gets the <c>SSV</c> instruction definition instance.
    /// </summary>
    public static Instruction SSV(Argument class_name, Argument instance_name, Argument attribute_name,
        Argument source) =>
        new(nameof(SSV), "SSV(class_name,instance_name,attribute_name,source)", class_name, instance_name,
            attribute_name, source);

    /// <summary>
    /// Gets the <c>STD</c> instruction definition instance.
    /// </summary>
    public static Instruction STD(Argument array, Argument dim_to_vary, Argument destination, Argument control,
        Argument length, Argument position) => new(nameof(STD),
        "STD(array,dim_to_vary,destination,control,length,position)", array, dim_to_vary, destination, control, length,
        position);

    /// <summary>
    /// Gets the <c>STOD</c> instruction definition instance.
    /// </summary>
    public static Instruction STOD(Argument source, Argument destination) =>
        new(nameof(STOD), "STOD(source,destination)", source, destination);

    /// <summary>
    /// Gets the <c>STOR</c> instruction definition instance.
    /// </summary>
    public static Instruction STOR(Argument source, Argument destination) =>
        new(nameof(STOR), "STOR(source,destination)", source, destination);

    /// <summary>
    /// Gets the <c>SUB</c> instruction definition instance.
    /// </summary>
    public static Instruction SUB(Argument source_A, Argument source_B, Argument destination) =>
        new(nameof(SUB), "SUB(source_A,source_B,destination)", source_A, source_B, destination);

    /// <summary>
    /// Gets the <c>SWPB</c> instruction definition instance.
    /// </summary>
    public static Instruction SWPB(Argument source, Argument order_mode, Argument destination) =>
        new(nameof(SWPB), "SWPB(source,order_mode,destination)", source, order_mode, destination);

    /// <summary>
    /// Gets the <c>TAN</c> instruction definition instance.
    /// </summary>
    public static Instruction TAN(Argument source, Argument destination) =>
        new(nameof(TAN), "TAN(source,destination)", source, destination);

    /// <summary>
    /// Gets the <c>THRS</c> instruction definition instance.
    /// </summary>
    public static Instruction THRS(Argument thrs_tag, Argument active_pin_type, Argument active_pin,
        Argument right_button_normally_open, Argument right_button_normally_closed, Argument left_button_normally_open,
        Argument left_button_normally_closed, Argument fault_reset) => new(nameof(THRS),
        "THRS(thrs_tag,active_pin_type,active_pin,right_button_normally_open,right_button_normally_closed,left_button_normally_open,left_button_normally_closed,fault_reset)",
        thrs_tag, active_pin_type, active_pin, right_button_normally_open, right_button_normally_closed,
        left_button_normally_open, left_button_normally_closed, fault_reset);

    /// <summary>
    /// Gets the <c>THRSE</c> instruction definition instance.
    /// </summary>
    public static Instruction THRSE(Argument thrse_tag, Argument discprepancy_time, Argument enable,
        Argument disconnected, Argument right_button_normally_open, Argument right_button_normally_closed,
        Argument left_button_normally_open, Argument left_button_normally_closed, Argument input_status,
        Argument resest) => new(nameof(THRSE),
        "THRSE(thrse_tag,discprepancy_time,enable,disconnected,right_button_normally_open,right_button_normally_closed,left_button_normally_open,left_button_normally_closed,input_status,resest)",
        thrse_tag, discprepancy_time, enable, disconnected, right_button_normally_open, right_button_normally_closed,
        left_button_normally_open, left_button_normally_closed, input_status, resest);

    /// <summary>
    /// Gets the <c>TND</c> instruction definition instance.
    /// </summary>
    public static Instruction TND() => new(nameof(TND), "TND()");

    /// <summary>
    /// Gets the <c>TOD</c> instruction definition instance.
    /// </summary>
    public static Instruction TOD(Argument source, Argument destination) =>
        new(nameof(TOD), "TOD(source,destination)", source, destination);

    /// <summary>
    /// Gets the <c>TOF</c> instruction definition instance.
    /// </summary>
    public static Instruction TOF(Argument timer, Argument preset, Argument accum) =>
        new(nameof(TOF), "TOF(timer,preset,accum)", timer, preset, accum);

    /// <summary>
    /// Gets the <c>TON</c> instruction definition instance.
    /// </summary>
    public static Instruction TON(Argument timer, Argument preset, Argument accum) =>
        new(nameof(TON), "TON(timer,preset,accum)", timer, preset, accum);

    /// <summary>
    /// Gets the <c>TO_BCD</c> instruction definition instance.
    /// </summary>
    public static Instruction TO_BCD(Argument timer, Argument preset, Argument accum) =>
        new(nameof(TO_BCD), "TO_BCD(timer,preset,accum)", timer, preset, accum);

    /// <summary>
    /// Gets the <c>TRN</c> instruction definition instance.
    /// </summary>
    public static Instruction TRN(Argument source, Argument destination) =>
        new(nameof(TRN), "TRN(source,destination)", source, destination);

    /// <summary>
    /// Gets the <c>TRUNC</c> instruction definition instance.
    /// </summary>
    public static Instruction TRUNC(Argument source, Argument destination) =>
        new(nameof(TRUNC), "TRUNC(source,destination)", source, destination);

    /// <summary>
    /// Gets the <c>TSAM</c> instruction definition instance.
    /// </summary>
    public static Instruction TSAM(Argument tsam_tag, Argument restart_type, Argument S1_S2_time, Argument S2_LC_time,
        Argument maximum_mute_time, Argument maximum_override_time, Argument light_curtain, Argument sensor_1,
        Argument sensor_2, Argument enable_mute, Argument @override, Argument input_status, Argument muting_lamp_status,
        Argument reset) => new(nameof(TSAM),
        "TSAM(tsam_tag,restart_type,S1_S2_time,S2_LC_time,maximum_mute_time,maximum_override_time,light_curtain,sensor_1,sensor_2,enable_mute,override,input_status,muting_lamp_status,reset)",
        tsam_tag, restart_type, S1_S2_time, S2_LC_time, maximum_mute_time, maximum_override_time, light_curtain,
        sensor_1, sensor_2, enable_mute, @override, input_status, muting_lamp_status, reset);

    /// <summary>
    /// Gets the <c>TSSM</c> instruction definition instance.
    /// </summary>
    public static Instruction TSSM(Argument tssm_tag, Argument restart_type, Argument S1_S2_discrepancy_time,
        Argument S1_S2_LC_minimum_time, Argument S1_S2_LC_maximum_time, Argument maximum_mute_time,
        Argument maximum_override_time, Argument light_curtain, Argument sensor_1, Argument sensor_2,
        Argument enable_mute, Argument @override, Argument input_status, Argument muting_lamp_status, Argument reset) =>
        new(nameof(TSSM),
            "TSSM(tssm_tag,restart_type,S1_S2_discrepancy_time,S1_S2_LC_minimum_time,S1_S2_LC_maximum_time,maximum_mute_time,maximum_override_time,light_curtain,sensor_1,sensor_2,enable_mute,override,input_status,muting_lamp_status,reset)",
            tssm_tag, restart_type, S1_S2_discrepancy_time, S1_S2_LC_minimum_time, S1_S2_LC_maximum_time,
            maximum_mute_time, maximum_override_time, light_curtain, sensor_1, sensor_2, enable_mute, @override,
            input_status, muting_lamp_status, reset);

    /// <summary>
    /// Gets the <c>UID</c> instruction definition instance.
    /// </summary>
    public static Instruction UID() => new(nameof(UID), "UID()");

    /// <summary>
    /// Gets the <c>UIE</c> instruction definition instance.
    /// </summary>
    public static Instruction UIE() => new(nameof(UIE), "UIE()");

    /// <summary>
    /// Gets the <c>UPPER</c> instruction definition instance.
    /// </summary>
    public static Instruction UPPER(Argument source, Argument destination) =>
        new(nameof(UPPER), "UPPER(source,destination)", source, destination);

    /// <summary>
    /// Creates a new XIC instruction instance with the predeinfed signature and provided instruction arguments. 
    /// </summary>
    /// <returns>A new <see cref="Instruction"/> with initialized key, signature, and arguments.</returns>
    /// <remarks>
    /// Note that this instruction method signature was extracted from the Rockwell L5X documentation.
    /// Each instruction will take the set of <see cref="Core.Argument"/> matching the instruction signature.
    /// It is up to the caller to know whether these can be immediate value arguments or tag name reference arguments.
    ///</remarks>
    public static Instruction XIC(Argument data_bit) => new(nameof(XIC), "XIC(data_bit)", data_bit);

    /// <summary>
    /// Gets the <c>XIO</c> instruction definition instance.
    /// </summary>
    public static Instruction XIO(Argument data_bit) => new(nameof(XIO), "XIO(data_bit)", data_bit);

    /// <summary>
    /// Gets the <c>XOR</c> instruction definition instance.
    /// </summary>
    public static Instruction XOR(Argument source_A, Argument source_B, Argument destination) =>
        new(nameof(XOR), "XOR(source_A,source_B,destination)", source_A, source_B, destination);

    /// <summary>
    /// Gets the <c>XPY</c> instruction definition instance.
    /// </summary>
    public static Instruction XPY(Argument source_A, Argument source_B, Argument destination) =>
        new(nameof(XPY), "XPY(source_A,source_B,destination)", source_A, source_B, destination);

    /// <summary>
    /// Gets the <c>EXPT</c> instruction definition instance.
    /// </summary>
    public static Instruction EXPT(Argument source_A, Argument source_B, Argument destination) =>
        new(nameof(EXPT), "EXPT(source_A,source_B,destination)", source_A, source_B, destination);

    #endregion

    /// <summary>
    /// Extracts operands from the signature of the instruction.
    /// </summary>
    private string[] ExtractOperands()
    {
        var match = Regex.Match(Signature, SignaturePattern);
        var input = match.Value.Substring(1, match.Value.Length - 2);

        return !string.IsNullOrEmpty(input)
            ? Regex.Split(input, ArgumentSplitPattern)
            : Enumerable.Empty<string>().ToArray();
    }

    /// <summary>
    /// Find all tag name arguments in the current instruction and returns them as a flat list of <see cref="TagName"/>.
    /// </summary>
    private TagName[] ExtractTags()
    {
        //Ingore task calling instructions since they don't refer to a tag name.
        if (IsTaskCall) return [];

        //For GSV and SSV instruction only the last argument represents an actual tag name reference.
        if (IsSystemCall) return [Arguments.Last().ToString()];

        //Skip the first argument of a routine instruction as it does not refer to a tag name.
        var arguments = IsRoutineCall ? Arguments.Skip(1) : Arguments;

        //And then anything else return all tag arguments.
        return arguments.SelectMany(a => a.Tags).ToArray();
    }

    /// <summary>
    /// Find all atomic value arguments in the current instruction and returns them as a flat list of <see cref="AtomicData"/>.
    /// </summary>
    private AtomicData[] ExtractValues()
    {
        //Ingore any system or task calling instructions since they don't refer to a tag name.
        if (IsSystemCall || IsTaskCall) return [];

        //Skip the first argument of a routine instruction as it does not refer to a tag name.
        var arguments = IsRoutineCall ? Arguments.Skip(1) : Arguments;

        return arguments.SelectMany(a => a.Values).ToArray();
    }

    /// <summary>
    /// Determines the component references for each argument in the instruction and returns the relative scope
    /// representation of the component object.
    /// </summary>
    private Scope[] ExtractReferences()
    {
        switch (Key)
        {
            case nameof(GSV) or nameof(SSV) when Arguments.Length >= 2:
            {
                var component = Scope.To($"/{Arguments[0]}/{Arguments[1]}");
                var tag = Scope.To($"/{ScopeType.Tag}/{Arguments.Last()}");
                return component.Type != ScopeType.Null ? [component, tag] : [tag];
            }
            case nameof(EVENT) when Arguments.Length == 1:
            {
                var task = Scope.To($"/{ScopeType.Task}/{Arguments[0]}");
                return [task];
            }
            case nameof(JSR) or nameof(JXR) or nameof(SFR) or nameof(SFP) or nameof(FOR) when Arguments.Length >= 1:
            {
                var routine = Scope.To($"/{ScopeType.Routine}/{Arguments[0]}");
                var tags = Arguments.Skip(1).Where(a => a.IsTag).Select(a => Scope.To($"/{ScopeType.Tag}/{a}"));
                return new[] { routine }.Concat(tags).ToArray();
            }
            default:
                return Arguments.SelectMany(a => a.Tags).Select(a => Scope.To($"/{ScopeType.Tag}/{a}")).ToArray();
        }
    }

    /// <summary>
    /// Creates some default arg names for a provided number of arguments.
    /// </summary>
    private static string DefaultArgs(int number)
    {
        return Enumerable.Range(0, number).Select(i => $"arg{i}").Combine(',');
    }

    /// <summary>
    /// Indexes all instruction factory methods in the class and creates a function returning the instruction 
    /// mathcing the specified key or method name. The method is passed null argumnts and therefore will be a default
    /// instruction instance. Callers can then use <see cref="With"/> to pass argument array.
    /// </summary>
    private static IEnumerable<KeyValuePair<string, Func<Instruction>>> Factories()
    {
        var methods = typeof(Instruction).GetMethods(BindingFlags.Public | BindingFlags.Static)
            .Where(m => m.ReturnType == typeof(Instruction) && m.Name.All(char.IsUpper));

        foreach (var method in methods)
        {
            var arguments = method.GetParameters()
                .Select(p => Expression.TypeAs(Expression.Constant(null), p.ParameterType));
            var function = Expression.Call(method, arguments);
            var lambda = Expression.Lambda<Func<Instruction>>(function);
            yield return new KeyValuePair<string, Func<Instruction>>(method.Name, lambda.Compile());
        }
    }
}