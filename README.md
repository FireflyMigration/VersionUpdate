# VersionUpdate

This utility updates all your `AssemblyInfo.cs` with either a new full version (e.g "4.6.1.32094") or just a new revision of the version (e.g "4.6.<b>1</b>.32094")

You can provide this utility with two command line arguments:
1) Path
2) Version

For example:

`c:\>VersionUpdate.exe path=d:\northwind\dotnet version=4.6.1.32099`

Notice:
- If no path is specified, make sure to run the utility from the Dotnet folder
- If no version is specified, the utility will only increment the revision.
