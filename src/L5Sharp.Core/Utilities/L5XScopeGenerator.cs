using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace L5Sharp.Core;

/// <summary>
/// Provides functionality to generate a list of all possible scopes that are merged with an input scope.
/// This allows us to generate a list of possible scopes/keys to use in lookup functions.
/// </summary>
internal class L5XScopeGenerator(XElement controller)
{
    private readonly Scope _controllerScope = Scope.To($"{controller.LogixName()}//");
    private readonly List<Scope> _scopes = BuildScopes(controller);

    /// <summary>
    /// Generates the full/absolute scope path to be used to find elements in the lookup. 
    /// </summary>
    public Scope GenerateSingle(Scope scope, Type? type = default)
    {
        //If the scope does not container a type then try to replace it with the provided type.
        var scopeType = type is not null ? ScopeType.Parse(type.L5XType()) : ScopeType.Null;
        scope = scope.Type != ScopeType.Null ? scope : scope.Merge($"/{scopeType.Value}/");
        
        //If the scope is a nested tag member we need to strip that off because we don't index each nested member.
        if (scope.Name.Depth > 0)
        {
            scope = scope.Merge(scope.Name.Root);
        }
        
        return _controllerScope.Merge(scope);
    }

    /// <summary>
    /// Generates a list of all possible scopes that are merged with the provided scope and optional type.
    /// </summary>
    public IEnumerable<Scope> GenerateAll(Scope scope, Type? type = default)
    {
        //If the scope does not container a type then try to replace it with the provided type.
        var scopeType = type is not null ? ScopeType.Parse(type.L5XType()) : ScopeType.Null;
        scope = scope.Type != ScopeType.Null ? scope : scope.Merge($"/{scopeType.Value}/");
        
        //If the scope is a nested tag member we need to strip that off because we don't index each nested member.
        if (scope.Name.Depth > 0)
        {
            scope = scope.Merge(scope.Name.Root);
        }

        return _scopes.Where(scope.IsIn).Select(s => s.Merge(scope));
    }

    #region Internal

    private static List<Scope> BuildScopes(XElement controller)
    {
        var scopes = new List<Scope>();

        scopes.AddRange(BuildControllerScopes(controller));
        scopes.AddRange(BuildProgramScopes(controller));
        scopes.AddRange(BuildRoutineScopes(controller));

        return scopes;
    }

    private static IEnumerable<Scope> BuildControllerScopes(XElement element)
    {
        var types = ScopeType.All().Where(t => t.InController).Select(x => x.Value).ToList();
        var controller = element.LogixName();
        return types.Select(t => Scope.To($"{controller}/{t}/"));
    }

    private static List<Scope> BuildProgramScopes(XElement element)
    {
        var scopes = new List<Scope>();
        var types = ScopeType.All().Where(t => t.InProgram).Select(x => x.Value).ToList();
        var controller = element.LogixName();
        var programs = element.Descendants(L5XName.Program);

        foreach (var program in programs)
        {
            var name = program.LogixName();
            scopes.AddRange(types.Select(t => Scope.To($"{controller}/{name}/{t}/")));
        }

        return scopes;
    }

    private static List<Scope> BuildRoutineScopes(XElement element)
    {
        var scopes = new List<Scope>();
        var controller = element.LogixName();
        var routines = element.Descendants(L5XName.Program).Descendants(L5XName.Routine);

        foreach (var routine in routines)
        {
            var programName = routine.Ancestors(L5XName.Program).First().LogixName();
            var routineName = routine.LogixName();
            var routineType = routine.Attribute(L5XName.Type)?.Value.Parse<RoutineType>() ?? RoutineType.Typeless;

            if (routineType == RoutineType.RLL)
                scopes.Add(Scope.To($"{controller}/{programName}/{routineName}/{ScopeType.Rung}/"));
            if (routineType == RoutineType.ST)
                scopes.Add(Scope.To($"{controller}/{programName}/{routineName}/{ScopeType.Line}/"));
            if (routineType == RoutineType.FBD)
                scopes.Add(Scope.To($"{controller}/{programName}/{routineName}/{ScopeType.Sheet}/"));
        }

        return scopes;
    }

    #endregion
}