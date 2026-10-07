# Phinix 商店测试夹具 (PoC)

<p align="center">
  <a href="./README.md">English</a> · 简体中文
</p>

> [!WARNING]
> **仅限开发者测试使用**：本项目仅作为 Phinix 托管扩展运行时研发与回归测试的夹具，**绝非**面向普通玩家的正式模组或商店插件。
>
> 玩家如需安装官方支持的插件，请使用游戏内商店或访问 [Phinix-Plugin-Index](https://github.com/HunYuan2333/Phinix-Plugin-Index)；插件作者如需编写自定义插件，请参考官方 [Phinix-Example-Plugin](https://github.com/HunYuan2333/Phinix-Example-Plugin)。

---

## 概述与定位

Playtest 1.3.0 是一个独立的测试夹具，用于持续验证宿主运行时的程序集加载、生命周期切换、设置持久化与多语言本地化：
- **测试界面与动作**：注册一个测试 Tab，包含点击计数器以及经确认后在当前地图生成 100 白银的调试动作。
- **存档安全提示**：**仅限一次性临时测试存档**使用，切勿载入任何重要殖民地存档。
- **目录显式排除**：本包已被官方索引仓库通过 `catalog-exclusions.json` 显式排除，不会出现在玩家商店列表中，游戏中亦无测试源选择器。

---

## 保留版本与资产

- **固定版本**：仓库仅保留经过回归验证的 [Playtest 1.3.0 Release](https://github.com/HunYuan2333/Phinix-PluginStore-PoC/releases/tag/v1.3.0)。
- **资产不可变**：已发布的 ZIP、Tag 标签与源码 Commit 哈希保持锁定，用于长期回归校验。
- **表面清理**：过期的原型目录、旧版本 Mod 包装与已退役的早期脚本已从当前仓库表面彻底清理。

---

## 开发者测试流程

1. **构建与打包**：参考 [`managed-playtest/README.zh-CN.md`](managed-playtest/README.zh-CN.md) 了解编译与 `ManagedPackageTool` 参数。
2. **本地安装测试**：
   - 将打包好的插件目录复制至 `<RimWorld模组目录>/Common/Extensions/`。
   - 确保同一模块不存在多个重复副本。
3. **验证清单**：
   - 重启 RimWorld 并确认测试 Tab 正常展示。
   - 在游戏设置中切换中英文语言，检查词条即时本地化更新。
   - 在“扩展管理”中停用或卸载该测试插件，重启游戏确认生命周期注销完整。

---

## 原型退役与历史归档

早期分发链路 PoC 已圆满完成对 Cloudflare Worker 缓存与 GitHub Release 串流的验证。当前正式生产分发体系已完全独立移交至：
- **目录与准入体系**：[Phinix-Plugin-Index](https://github.com/HunYuan2333/Phinix-Plugin-Index)
- **生产加速网关**：[Phinix-Plugin-Gateway](https://github.com/HunYuan2333/Phinix-Plugin-Gateway)
- **插件开发参考标准**：[Phinix-Example-Plugin](https://github.com/HunYuan2333/Phinix-Example-Plugin)
