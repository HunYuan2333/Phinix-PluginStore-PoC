# Phinix Store Playtest (PoC)

<p align="center">
  English · <a href="./README.zh-CN.md">简体中文</a>
</p>

> [!WARNING]
> **Developer Testing Fixture Only**: This repository contains a test fixture used strictly for developing and verifying the Phinix managed extension runtime. It is **not** an official player-facing mod or store plugin.
>
> If you are a player looking for official plugins, use the in-game Plugin Store or browse [Phinix-Plugin-Index](https://github.com/HunYuan2333/Phinix-Plugin-Index). If you are an author writing a custom plugin, see [Phinix-Example-Plugin](https://github.com/HunYuan2333/Phinix-Example-Plugin).

---

## Overview & Purpose

Playtest 1.3.0 is a standalone fixture retained to validate the extension host's assembly loading, lifecycle transitions, setting persistence, and package-scoped localization:
- **Test Tab & Actions**: Registers a test tab with a persistent counter and a debug action that grants 100 silver upon confirmation.
- **Savegame Notice**: Use **disposable test saves only**. Do not load this plugin in valuable colonies.
- **Catalog Exclusion**: This package is explicitly excluded from player catalogs via `catalog-exclusions.json` in the official index. There is no in-game test source selector.

---

## Retained Version & Release Assets

- **Immutable Version**: Only the verified [Playtest 1.3.0 release](https://github.com/HunYuan2333/Phinix-PluginStore-PoC/releases/tag/v1.3.0) is retained.
- **Asset Integrity**: The published ZIP, tag, and source commit hashes remain fixed for regression testing.
- **Cleaned Surface**: Obsolete prototype catalogs, legacy local mod wrappers, and retired worker scripts have been permanently removed.

---

## Developer Testing Workflow

1. **Build & Package**: Refer to [`managed-playtest/README.md`](managed-playtest/README.md) for compilation and `ManagedPackageTool` instructions.
2. **Local Installation**:
   - Copy the packed extension folder into `<path-to-RimWorld-Mod>/Common/Extensions/`.
   - Ensure no duplicate copies of the same module exist.
3. **Verification Checklist**:
   - Restart RimWorld and verify that the test tab appears.
   - Switch language between English and Simplified Chinese to verify string resolution.
   - Disable or uninstall the extension via Extension Manager to verify clean teardown upon game restart.

---

## Prototype Retirement & History

The original distribution PoC validated Cloudflare Worker caching and GitHub release streaming. The production distribution architecture is now maintained independently across:
- **Catalog & Admission**: [Phinix-Plugin-Index](https://github.com/HunYuan2333/Phinix-Plugin-Index)
- **Production Gateway**: [Phinix-Plugin-Gateway](https://github.com/HunYuan2333/Phinix-Plugin-Gateway)
- **Plugin Implementation Reference**: [Phinix-Example-Plugin](https://github.com/HunYuan2333/Phinix-Example-Plugin)
