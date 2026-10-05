# Phinix Store Playtest 1.3.0

独立开发测试插件，适用于 RimWorld 1.6 和已实现本地化支持的 Phinix 开发版 host（ClientExtensionAbstractions 1.7.0）。此发布基于 Assembly-CSharp 1.6.9676.18020，请先重新构建 host 与内置扩展。仅用测试存档。

插件注册测试 Tab、保留点击计数，并在确认后添加 100 白银。中英文 JSON 经通用 host 本地化服务应用到 Tab、按钮、确认与结果提示，其他游戏语言回退到英文；停用商店不会停用插件翻译。

仅保留最新版 1.3.0；它不出现在正式 index 中，旧测试源已退役。手工测试请使用完整文件夹 bundle，放到开发版 Common/Extensions 的直接子文件夹。DLL、本地化伴随文件及 Resources 必须在同一插件文件夹。先移除旧 Playtest 副本并重启，避免重复模块。

原 ZIP 包含 manifest.json、Assemblies/Phinix.Store.Playtest.dll、Resources/Localization/en-US.json 和 zh-CN.json；哈希与长度由 manifest 验证。原 ZIP、v1.3.0 标签和 sourceCommit 不改写，不包含游戏或 host DLL。

## 构建与打包

本仓库不包含完整 host，请使用 Phinix-Rework 开发检出、.NET 10 和自备的 RimWorld 1.6 编译参考，替换以下绝对路径：

```sh
dotnet build managed-playtest/Playtest.csproj -c Release -p:PhinixRoot=/absolute/Phinix-Rework -p:GameReferences=/absolute/RimWorld/Managed
dotnet build managed-playtest/ManagedPackageTool/ManagedPackageTool.csproj -c Release -p:PhinixRoot=/absolute/Phinix-Rework
```

打包器参数：--assembly，--package-id phinix.poc.playtest，--name，--version 1.3.0，重复 --language-file 指向两个 JSON，--output 指向新 ZIP，--bundle-output 指向新文件夹。重复 --host-assembly 提供准确的 Utils、ClientExtensionAbstractions、mscorlib、Assembly-CSharp 和 Unity 参考。工具拒绝覆盖输出并排除参考 DLL。

## 游戏测试

1. 放置整个 bundle，重启后查看测试 Tab。手工 bundle 的卸载是移除整个文件夹再重启；商店托管安装的旧副本继续通过扩展管理卸载，二者不能混装。
2. 测试中英文 Tab、计数、确认及结果；缺少日文/法文翻译时回退英文。
3. 切换语言不改变计数，停用商店不影响翻译。
4. 扩展管理停用/启用，重启验证 Tab 隐藏/恢复；保留设置和存档数据。

编译成功不代表已完成游戏内验收。正式索引及 GitHub/CF 访问属于独立基础设施，本插件不会重新上架到正式 index。
