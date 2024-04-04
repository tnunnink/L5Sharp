## Release Notes 2.0
This release follows from version 0.19.X. The reason for the version change is to represent this as a mostly complete
and no longer a work in progress library. The reason for v2 and not v1 is because I accidentally released v1 a long time
ago before backing into v0.1.

This update mostly focused on adding .NET 2.0 standard support and merging how LogixTypes were deserialized with
how all other LogixElements were deserialized, which makes the entire project more uniform and gives one point for 
deserialization of any element/type. Most of the API surface is the same with a couple minor tweaks. You should not notice too may changes.

### Changes
1. Adds support for .NET Standard 2.0 to make library compatible with .NET Framework applications.
2. `LogixType` now implements `LogixElement`, making them wrap an underlying element where possible, 
and allowing a single base class for all types.
3. Combined the old `LogixData` with the `LogixSerializer` to have a single point of deserialization for all elements.
4. Created new `LogixObject` which implements `LogixElement` and moved the common L5X, Scope, Container, and methods
for adding, replacing, and removing the object from the L5X.
5. Removed `AtomicType` bit members to avoid exceedingly large number of member tags it would generate when attemping to
all tags in a file.
6. Removed `Class` and `Family` properties from `LogixType` since they are not really useful.
7. Renamed `LogixType` to `LogixData`, as well as all derivatives to better match the L5X naming convention.
