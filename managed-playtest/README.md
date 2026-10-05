# Phinix Store Playtest 1.3.0

Test-only managed DLL plugin for RimWorld 1.6 and the Phinix development host with ClientExtensionAbstractions 1.7.0. Rebuild the complete host and bundled extensions first. This release uses Assembly-CSharp 1.6.9676.18020. Do not install into a valuable save.

The plugin registers a visible test tab, retains a click counter, and grants 100 silver only after confirmation in a test map. English and Simplified Chinese JSON files drive tab/button/confirmation/result text through the generic host localization service. Other game languages fall back to supplied English. The store is not required for translations after installation.

Install v1.3.0 from the phinix.managed staging source. Remove any manually copied Playtest folder or earlier loose/local-Mod copy and restart first; two copies of the same module are not supported. Installation, enable/disable and uninstall take effect after a full restart. Uninstall retains settings and save data.

The ZIP has manifest.json, Assemblies/Phinix.Store.Playtest.dll and Resources/Localization/en-US.json plus zh-CN.json. Managed downloads are installed into a package-owned folder under SaveData/Phinix/ManagedExtensions/packages. The manifest verifies lengths and SHA-256 of DLL and language resources. No game or host DLLs are shipped.

For manual bundled testing, the packager can emit a folder containing the DLL, its .dll.localization.json companion and package-scoped Resources. Copy that entire folder as one immediate child of Common/Extensions; the host probes immediate plugin folders only, without arbitrary recursive DLL discovery.

## Source and build

This branch contains sample and packager source; it does not contain the complete development host. Use the corresponding Phinix-Rework checkout with localization support, .NET 10 and your own RimWorld 1.6 compile-only references. Set PhinixRoot when building this standalone sample project. The ManagedPackageTool project also accepts PhinixRoot. Do not redistribute game/reference DLLs.

Example commands (replace absolute checkout/reference/output paths):

```sh
dotnet build managed-playtest/Playtest.csproj -c Release -p:PhinixRoot=/absolute/Phinix-Rework -p:GameReferences=/absolute/RimWorld/Managed
dotnet build managed-playtest/ManagedPackageTool/ManagedPackageTool.csproj -c Release -p:PhinixRoot=/absolute/Phinix-Rework
```

Run the packager with --assembly, --package-id phinix.poc.playtest, --name, --version 1.3.0, repeated --language-file for the two JSON files, --output for a new ZIP, and optionally --bundle-output for a new bundled folder. Supply repeated --host-assembly for exact current Utils, ClientExtensionAbstractions, mscorlib, Assembly-CSharp and Unity references. The tool refuses output overwrites and excludes host/game assemblies.

## Game checks

1. Install, restart and check the test tab and both language files in the package folder.
2. Check Chinese and English tab, counter, confirmation and result text; missing Japanese/French falls back to English.
3. Check that language switching preserves the counter, and disabling the store does not disable plugin translations.
4. Check Playtest disable/re-enable and uninstall across restarts; verify that its tab disappears while settings/save data remain.

These tests complement static/runtime checks; compilation does not prove in-game behavior. Catalog v3 multilingual store descriptions and direct GitHub client access are separate pending work. This test release uses the current catalog v2 publication chain.
