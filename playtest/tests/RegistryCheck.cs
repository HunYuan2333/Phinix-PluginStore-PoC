using System;using System.Linq;using System.Reflection;using Utils;using Utils.Framework;using PhinixClient;
class PlaytestRegistryCheck
{
 static int Main(string[] a)
 {
  try {
   Assembly.LoadFrom(a[0]);
   var context=new ExtensionHostContext{Log=(line,level)=>Console.WriteLine(line)};
   var found=PhinixExtensionRegistry.DiscoverExtensions(context);
   PhinixExtensionRegistry.ActivateExtensions(found,context);
   var item=found.ExtensionResults.Single(x=>x.ExtensionId=="phinix.poc.playtest");
   if(item.State!=ExtensionModuleState.Active)throw new Exception("Not activated: "+item.State+" "+item.StateDetail);
   if(context.ResolveApis<IMainTabProvider>().Count!=1)throw new Exception("Provider registration failed");
   PhinixExtensionRegistry.ShutdownExtensions(found,context);
   if(item.State!=ExtensionModuleState.Shutdown)throw new Exception("Shutdown failed");
   Console.WriteLine("Actual sample discovered, registered one IMainTabProvider, activated, and shut down under Mono. No game UI/action invoked.");return 0;
  } catch(Exception e){Console.Error.WriteLine(e);return 1;}
 }
}
