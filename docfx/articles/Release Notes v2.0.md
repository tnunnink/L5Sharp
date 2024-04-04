## Release Notes 2.0
This release follows from version 0.19.X. The reason for the version change is to represent this as a mostly complete
and no longer a work in progress library. The reason for v2 and not v1 is because I accidentally released v1 a long time
ago before backing into v0.1.

### Changes
1. Adds support for .NET Standard 2.0 to make library compatible with .NET Framework applications.
2. `LogixType` now implements `LogixElement`, making them wrap an underlying element where possible, 
and allowing a single base class for all types.
3. Created new `LogixObject` which implements `LogixElement` and has the common methods for Scope and adding, replacing,
and removing the object from the L5X.
4. Combined the `LogixData` with the `LogixSerializer` to have a single point of deserialization for all elements.
5. Removed `AtomicType` bit members to avoid exceedingly large number of member tags per tag.
6. Removed `Class` and `Family` properties from LogixType.

### Breaking Changes


### 
