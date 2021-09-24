using System.Collections.Generic;
using LogixHelper.Enumerations;
using LogixHelper.Utilities;

namespace LogixHelper.Primitives
{
    public class Tag
    {
        private Tag(string name, DataType dataType)
        {
            Validate.TagName(name);
            Name = name;

            DataType = dataType;
            //use datatype to populate structure
        }

        public Tag(string name, DataType dataType, Program program = null, string description = null)
        {
            //optional
            Description = description ?? string.Empty;
            Program = program;

            //program? (and therefore scope)

            //defaultable
            //TagType = base unless this a produced/consumed/alias tag (not sure how to support yet)
            //Useage = null unless part or AOI?
            //Class = 
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public DataType DataType { get; private set; }
        public string Dimensions { get; private set; }
        public Radix Radix { get; private set; }
        public TagType TagType { get; }
        public TagUsage Usage { get; }
        public Scope Scope { get; set; }
        public Program Program { get; set; }
        public string AliasFor { get; set; }
        public bool Constant { get; set; }
        public ExternalAccess ExternalAccess { get; set; }
        public object Value { get; set; }

        public object ForceValue { get; set; }
        public IEnumerable<Tag> Tags { get; set; }

        private bool IsValueMember => Value != null;
        private bool IsArrayMember => Dimensions != "0";
        private bool IsStructureMember => !IsArrayMember && !IsValueMember;

        //todo not on documentation but xsd, what is this?
        public string AliasBase { get; set; }

        //todo not on documentation but xsd, what is this?
        public bool IO { get; set; }

        //todo not on documentation but xsd. But probably means can it be forced
        public bool CanForce { get; set; }

        //todo not on documentation but xsd full tag name?
        public string FullName { get; set; }

        //todo not on documentation but xsd. probably internal? meaning as it been verified in current controller context
        public bool Verified { get; set; }
    }
}