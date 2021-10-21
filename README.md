-- Creating Code

Create a new file called `Jose.CheatsMod.csproj`
```text/xml
<?xml version="1.0" encoding="utf-8"?>
<Project ToolsVersion="Current" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <PropertyGroup>
    <OuterWildsRootDirectory>$(OuterWildsDir)\Outer Wilds</OuterWildsRootDirectory>
    <OuterWildsModsDirectory>%AppData%\OuterWildsModManager\OWML\Mods</OuterWildsModsDirectory>
  </PropertyGroup>
</Project>
```