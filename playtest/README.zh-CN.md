# Phinix 商店测试插件 1.1.0

独立扩展 `phinix.poc.playtest`，与旧的 inert marker 测试包分开。通过公共
`IExtensionBuilder.RegisterApi<IMainTabProvider>` 注册 Tab，走通用发现、激活和关闭流程。
Tab 提供点击计数和需要确认的“生成 100 白银”，白银放在当前地图中心附近。
请使用测试存档：保存游戏后白银会保留。没有网络处理、静态地图引用或存档组件。

编译和打包命令见英文说明；源码放在独立仓库时，编译需要
`-p:PhinixRoot=/Phinix-Rework的绝对路径`。只打包声明的 DLL，不附带框架和游戏引用。

经 staging 商店安装后，在游戏 Mod 列表启用并重启；Phinix 内应出现“商店测试”Tab，
日志应出现 `Playtest: activated`。禁用扩展并重启后应看不到 Tab。
卸载前需要禁用 **游戏 Mod** 并重启；仅关闭扩展不能卸载 CLR 程序集。
商店会检查依赖、归属和每个文件的实际摘要。编译通过不代表已完成游戏验收。
