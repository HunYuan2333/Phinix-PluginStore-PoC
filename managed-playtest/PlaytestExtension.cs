using System;
using PhinixClient;
using PhinixClient.Framework;
using RimWorld;
using UnityEngine;
using Utils;
using Utils.Framework;
using Verse;

namespace Phinix.Store.Playtest
{
    [PhinixExtension("phinix.poc.playtest")]
    public sealed class PlaytestExtension : IPhinixExtensionModule, IActivatablePhinixExtensionModule
    {
        private readonly PlaytestTab tab = new PlaytestTab();
        public string ExtensionId => "phinix.poc.playtest";
        public void Register(IExtensionBuilder builder) { builder.RegisterApi<IMainTabProvider>(tab); }
        public void Activate(ExtensionHostContext hostContext)
        {
            hostContext.TryGetService<IClientSettingsContext>(out var settings);
            tab.Start(hostContext.Log,settings);
            hostContext.Log("Playtest: activated; registered tab through the public extension API.", LogLevel.INFO);
        }
        public void Shutdown(ExtensionHostContext hostContext)
        {
            tab.Stop();
            hostContext.Log("Playtest: shutdown; cleared tab state and callbacks.", LogLevel.INFO);
        }
    }

    internal sealed class PlaytestTab : IMainTabProvider
    {
        private Action<string, LogLevel> log;
        private int clicks;
        private IClientSettingsContext settings;
        private bool active;
        private long generation;
        private string result;
        public string TabLabel => T("tab");
        public float TabOrder => 998f;
        internal void Start(Action<string, LogLevel> sink,IClientSettingsContext context) { Stop(); log = sink; settings=context; clicks=Math.Max(0,settings?.Get("playtest.clicks",0)??0); active = true; }
        internal void Stop() { active = false; clicks = 0; result = null; log = null; settings=null; generation++; }
        public void Draw(Rect inRect)
        {
            if (!active || inRect.width <= 0 || inRect.height <= 0) return;
            GameFont font = Text.Font; TextAnchor anchor = Text.Anchor; bool wrap = Text.WordWrap, enabled = GUI.enabled; Color color = GUI.color;
            try
            {
                Text.Font = GameFont.Small; Text.Anchor = TextAnchor.UpperLeft; Text.WordWrap = true; GUI.color = Color.white;
                Widgets.Label(new Rect(inRect.x, inRect.y, inRect.width, 70), T("intro"));
                if (Widgets.ButtonText(new Rect(inRect.x, inRect.y + 76, inRect.width, 32), T("click") + " (" + clicks + ")"))
                { if(clicks<int.MaxValue) clicks++; settings?.Set("playtest.clicks",clicks); log?.Invoke("Playtest: click count=" + clicks + ".", LogLevel.INFO); }
                GUI.enabled = enabled && Find.CurrentMap != null;
                if (Widgets.ButtonText(new Rect(inRect.x, inRect.y + 114, inRect.width, 32), T("silver")))
                {
                    long ticket = generation;
                    Map map = Find.CurrentMap;
                    Find.WindowStack.Add(Dialog_MessageBox.CreateConfirmation(T("confirm"), () =>
                    {
                        if (!active || ticket != generation || Find.CurrentMap != map) return;
                        GiveSilver(map);
                    }));
                }
                GUI.enabled = enabled;
                Widgets.Label(new Rect(inRect.x, inRect.y + 154, inRect.width, Math.Max(0, inRect.height - 154)), result == null ? T("mapRequired") : T(result));
            }
            finally { Text.Font = font; Text.Anchor = anchor; Text.WordWrap = wrap; GUI.color = color; GUI.enabled = enabled; }
        }
        private void GiveSilver(Map map)
        {
            try
            {
                Thing silver = ThingMaker.MakeThing(ThingDefOf.Silver); silver.stackCount = 100;
                bool placed = GenPlace.TryPlaceThing(silver, map.Center, map, ThingPlaceMode.Near);
                result = placed ? "given" : "failed";
                if (!placed && !silver.Destroyed && !silver.Spawned) silver.Destroy(DestroyMode.Vanish);
                log?.Invoke("Playtest: silver placement " + (placed ? "succeeded; requested=100." : "failed; no delivery claimed."), placed ? LogLevel.INFO : LogLevel.WARNING);
            }
            catch (Exception ex)
            { result = "failed"; log?.Invoke("Playtest: silver action failed; type=" + ex.GetType().Name + ".", LogLevel.WARNING); }
        }
        private static string T(string key)
        {
            bool chinese=LanguageDatabase.activeLanguage?.folderName?.StartsWith("Chinese",StringComparison.OrdinalIgnoreCase)==true;
            string value; return (chinese?Chinese:English).TryGetValue(key,out value)?value:key;
        }
        private static readonly System.Collections.Generic.Dictionary<string,string> English=new System.Collections.Generic.Dictionary<string,string>
        {
            {"tab","Store test"},
            {"intro","This tab checks discovery and registration. The click count is retained through uninstall/reinstall. Silver changes the current save."},
            {"click","Test registration: count clicks"},
            {"silver","Place 100 silver on the current map"},
            {"confirm","Place 100 silver near this map’s center? This changes the colony and will be retained if you save. Use a test save."},
            {"mapRequired","Silver requires a loaded map. Click counting also works without a map."},
            {"given","100 silver placed near the map center. See the Playtest operation log."},
            {"failed","Silver placement failed. See the Playtest operation log."}
        };
        private static readonly System.Collections.Generic.Dictionary<string,string> Chinese=new System.Collections.Generic.Dictionary<string,string>
        {
            {"tab","商店测试"},
            {"intro","这个 Tab 验证插件发现和注册。点击计数在卸载、重装后保留；白银操作会改变当前存档。"},
            {"click","测试注册：点击计数"},
            {"silver","在当前地图生成 100 白银"},
            {"confirm","在当前地图中心附近生成 100 白银？这会改变殖民地，保存游戏后会保留。请使用测试存档。"},
            {"mapRequired","生成白银需要已加载地图。没有地图时也可以测试点击计数。"},
            {"given","已在地图中心附近放置 100 白银。可查看 Playtest 操作日志。"},
            {"failed","白银放置失败，请查看 Playtest 操作日志。"}
        };
    }
}
