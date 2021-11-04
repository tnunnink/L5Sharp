using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using L5Sharp.Exceptions;
using L5Sharp.Extensions;
using L5Sharp.Utilities;

namespace L5Sharp.Core
{
    public class Instruction : IInstruction
    {
        private const string ResourceNamespace = "Resources";
        private const string ResourceFileName = "Instructions.xml";
        private static readonly ResourceReader Resources = new ResourceReader(typeof(Instruction));
        private static readonly XDocument InstructionData = LoadPredefined();
        
        private readonly Dictionary<string, IMember> _operands = new Dictionary<string, IMember>();

        private static readonly Dictionary<string, IInstruction> Registered =
            new Dictionary<string, IInstruction>(StringComparer.OrdinalIgnoreCase);

        protected Instruction(string name, string description, IEnumerable<IMember> operands = null)
        {
            Validate.Name(name);
            
            Name = name;
            Description = description;
            
            operands ??= Array.Empty<Member>();
            
            foreach (var operand in operands)
            {
                if (_operands.ContainsKey(operand.Name))
                    throw new ComponentNameCollisionException(operand.Name, typeof(Member));

                _operands.Add(operand.Name, operand);
            }

            if (!Registered.ContainsKey(name))
                Registered.Add(name, this);
        }

        internal Instruction(XElement element)
        {
            Validate.Name(element.GetName());

            Name = element.GetName();
            
            var parameters = element.Descendants(LogixNames.GetComponentName<IParameter>());

            foreach (var parameter in parameters)
            {
                var typeName = parameter.GetDataTypeName();
                if (typeName == null)
                    throw new ArgumentNullException(nameof(typeName), "DataType can not be null");
                
                var name = parameter.GetName();
                var type = Logix.DataType.Parse(typeName);

                var member = new Member(name, type);

                _operands.Add(member.Name, member);
            }

            if (!Registered.ContainsKey(Name))
                Registered.Add(Name, this);
        }

        public string Name { get; }
        public string Description { get; }
        public string Signature => $"{Name}({string.Join(",", _operands.Values.Select(m => m.Name))})";
        public IEnumerable<IMember> Operands => _operands.Values.AsEnumerable();

        public INeutralText GenerateText(params ITagMember[] tags)
        {
            return new NeutralText(this, tags);
        }
        
        public INeutralText GenerateText(params object[] values)
        {
            throw new NotImplementedException();
        }
        
        public INeutralText<TInstruction> GenerateText<TInstruction>(params ITagMember[] tags) 
            where TInstruction : IInstruction
        {
            throw new NotImplementedException();
        }
        
        public INeutralText<TInstruction> Generate<TInstruction>(params string[] parameters) 
            where TInstruction : IInstruction
        {
            throw new NotImplementedException();
        }
        
        public INeutralText<TInstruction> Generate<TInstruction>(params object[] values) 
            where TInstruction : IInstruction
        {
            throw new NotImplementedException();
        }

        public static bool Contains(string name)
        {
            return Registered.ContainsKey(name);
        }

        public static IInstruction Parse(string name)
        {
            return Registered.ContainsKey(name) ? Registered[name] : null;
        }

        internal static XElement LoadElement(string name)
        {
            return InstructionData.Descendants(nameof(Instruction)).SingleOrDefault(x => x.GetName() == name);
        }

        private static XDocument LoadPredefined()
        {
            using var stream = Resources.GetStream(ResourceFileName, ResourceNamespace);
            return XDocument.Load(stream);
        }
    }
}