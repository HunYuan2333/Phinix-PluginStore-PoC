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
            tab.Start(hostContext.Log);
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
        private bool active;
        private long generation;
        private string result;
        public string TabLabel => T("tab");
        public float TabOrder => 998f;
        internal void Start(Action<string, LogLevel> sink) { Stop(); log = sink; active = true; }
        internal void Stop() { active = false; clicks = 0; result = null; log = null; generation++; }
        public void Draw(Rect inRect)
        {
            if (!active || inRect.width <= 0 || inRect.height <= 0) return;
            GameFont font = Text.Font; TextAnchor anchor = Text.Anchor; bool wrap = Text.WordWrap, enabled = GUI.enabled; Color color = GUI.color;
            try
            {
                Text.Font = GameFont.Small; Text.Anchor = TextAnchor.UpperLeft; Text.WordWrap = true; GUI.color = Color.white;
                Widgets.Label(new Rect(inRect.x, inRect.y, inRect.width, 70), T("intro"));
                if (Widgets.ButtonText(new Rect(inRect.x, inRect.y + 76, inRect.width, 32), T("click") + " (" + clicks + ")"))
                { clicks++; log?.Invoke("Playtest: click count=" + clicks + ".", LogLevel.INFO); }
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
        private static string T(string key) => ("Phinix_playtest_" + key).Translate();
    }
}
