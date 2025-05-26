using System;

namespace L5Sharp.Core;

/// <summary>
/// Provides a builder interface for configuring and controlling import operations in a Logix controller application.
/// </summary>
public interface IImportControllerBuilder
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    IImportFromBuilder DataType(string name);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    IImportFromBuilder DataTypes(Func<DataType, bool> predicate);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    IImportFromBuilder Instruction(string name);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    IImportFromBuilder Instructions(Func<AddOnInstruction, bool> predicate);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    IImportFromBuilder Program(string name);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    IImportFromBuilder Programs(Func<Program, bool> predicate);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    IImportFromBuilder Tag(string name);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    IImportFromBuilder Tags(Func<Tag, bool> predicate);
}