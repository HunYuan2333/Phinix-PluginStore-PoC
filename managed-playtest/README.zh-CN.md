# Phinix 商店测试插件 1.3.0

[English](README.md)。仅供测试存档。需要带本地化服务的 Phinix 开发宿主、ClientExtensionAbstractions 1.7.0，以及 RimWorld 1.6（Assembly-CSharp 1.6.9676.18020）。先完整重编主体和内置插件。

插件注册可见 Tab、保存点击计数，并在测试地图上确认后添加 100 白银。en-US.json 和 zh-CN.json 通过宿主通用本地化服务驱动 Tab、按钮、确认框和结果；其他语言回退到已有英文。安装后无需启用商店也能翻译。

从 staging 的 phinix.managed 来源安装 1.3.0 前，先取走手动复制的 Playtest 文件夹或旧散装 DLL/本地 Mod，并完整重启，避免重复模块。安装、启停、卸载重启后生效；卸载保留设置和存档。

ZIP 包含 manifest.json、Assemblies/Phinix.Store.Playtest.dll、Resources/Localization/en-US.json 和 zh-CN.json。下载后放入 SaveData/Phinix/ManagedExtensions/packages 下的独立受管文件夹。清单校验全部声明文件的长度和 SHA-256，不附带游戏/宿主 DLL。

手动随包测试可以使用打包器的 --bundle-output，将 DLL、相邻 .dll.localization.json 和 Resources 保留在同一个插件文件夹，再把整个文件夹放在 Common/Extensions 的下一层。宿主只扫描直接子插件目录，不任意递归加载。

本分支提供样例和打包器源码，不包含完整开发宿主。构建时把 PhinixRoot 指向支持本地化的 Phinix-Rework 检出目录，把 GameReferences 指向自己的游戏参考程序集；命令和打包参数见英文说明。

游戏测试：安装重启后确认 Tab 和语言资源；检查中文、英文以及日文/法文缺译回退；切换语言保持计数；关闭商店后插件仍翻译；检查停用、启用、卸载后重启，确认 Tab 和保留数据符合预期。编译和静态校验不能替代游戏测试。

本测试版本仍通过目录 v2 发布；目录 v3 的商店多语言简介以及客户端 GitHub 直连属于后续独立步骤。
