# Managed Playtest 1.2.1

Source and packaging-tool source for the Phinix managed DLL test. Build inside a Phinix-Rework checkout (RimWorld 1.6 / Phinix 0.9.7 / client abstractions 1.6.0). Do not bundle host, framework, Unity or game DLLs. This folder is a source snapshot; its project references require the full checkout or an explicit PhinixRoot.

1.2.1 fixes the sample reference build: Assembly-CSharp 1.6.9676.18020, replacing the obsolete 1.6.9438.37837 reference in 1.2.0. Override GameReferences when building the sample to point at the intended game Managed directory; never infer a match from the major/minor label alone. These game files are compiler inputs only and are not published.

Install from phinix.managed on the staging store and restart. Its registered tab offers a persistent click counter and a confirmed 100-silver action for test saves. Disable/uninstall take effect at restart; settings remain after removal. No separate RimWorld mod shell is used.

ManagedPackageTool accepts repeated --assembly and optional repeated --host-assembly paths. When host identities are supplied, every external CLR reference must match exactly before any ZIP is written. Supply the intended game mscorlib/Assembly-CSharp/Unity modules and the actual Utils/ClientExtensionAbstractions DLLs. This checks static compatibility, not runtime activation or arbitrary plugin safety.

The old 1.2.0 release remains immutable. New assets use new version/tag/digests; no overwrite is permitted.
