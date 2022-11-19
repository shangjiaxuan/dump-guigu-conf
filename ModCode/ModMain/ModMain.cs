using System;
using System.Net.NetworkInformation;
using System.Reflection;
using EGameTypeData;
using MelonLoader;
using UnityEngine;
using System.Collections.Generic;
using static UISchoolBigFightScoreBase;
using Harmony;

/// <summary>
/// 当你手动修改了此命名空间，需要去模组编辑器修改对应的新命名空间，程序集也需要修改命名空间，否则DLL将加载失败！！！
/// </summary>
namespace MOD_B4oPvp
{
	internal class Dumper
	{
		//public static readonly Dictionary<string, Action<System.IO.StreamWriter>> special;

		static Dumper()
		{
			/*
			var temp = new Dictionary<string, Action<System.IO.StreamWriter>>();
			// UnityEngine.Color cyclic reference detected
			temp.Add("battleAbilitySuitBase", OnBattleAbilitySuitBase);
			// ConfMgrVariant cyclic reference detected
			temp.Add("battleSkillValue", OnBattleSkillValue);
			// ConfMgrVariant cyclic reference detected
			temp.Add("roleEffect", OnRoleEffect);
			// System.ComponentModel.ReferenceConverter constructor missing
			temp.Add("pillSchoolFight", OnPillSchoolFight);
			special = temp;*/
		}

		/*
		private static void OnBattleAbilitySuitBase(System.IO.StreamWriter writer)
		{
			MelonLogger.Error($"Conf file {(writer.BaseStream as System.IO.FileStream).Name} format not known, dumping dict...");
			writer.Write(Il2CppNewtonsoft.Json.JsonConvert.SerializeObject(g.conf.battleAbilitySuitBase.allConfDic));
		}

		private static void OnBattleSkillValue(System.IO.StreamWriter writer)
		{
			writer.Write(Il2CppNewtonsoft.Json.JsonConvert.SerializeObject(g.conf.battleSkillValue.allItems));
		}

		private static void OnRoleEffect(System.IO.StreamWriter writer)
		{
			writer.Write(Il2CppNewtonsoft.Json.JsonConvert.SerializeObject(g.conf.roleEffect.allArrtKey));
		}

		private static void OnPillSchoolFight(System.IO.StreamWriter writer)
		{
			writer.Write(Il2CppNewtonsoft.Json.JsonConvert.SerializeObject(g.conf.pillSchoolFight._healPillMap));
		}
		*/
		public static void Dump(string prefix)
		{
			if (!System.IO.Directory.Exists(prefix))
			{
				System.IO.Directory.CreateDirectory(prefix);
			}
			MelonLogger.Msg("Starting dump...");
			//MelonLogger.Msg(System.IO.Path.GetFullPath(prefix));
			try
			{
				/*MelonLogger.Msg($"Prop count: {g.conf.data.GetIl2CppType().GetProperties().Count}");
				MelonLogger.Msg($"Method count: {g.conf.data.GetIl2CppType().GetMethods().Count}");
				MelonLogger.Msg($"Field count: {g.conf.data.GetIl2CppType().GetFields().Count}");
				MelonLogger.Msg($"Mod mono Prop count: {g.conf.data.GetType().GetProperties().Length}");
				MelonLogger.Msg($"Mod mono Method count: {g.conf.data.GetType().GetMethods().Length}");
				MelonLogger.Msg($"Field count: {g.conf.data.GetType().GetFields().Length}");
				foreach (var item in g.conf.data.GetIl2CppType().GetFields())
				{
					string filename = $"{prefix}/{item.Name}.json";
					MelonLogger.Msg(filename);
					var file = System.IO.File.Create(filename);
					file.SetLength(0);
					System.IO.StreamWriter sw = new System.IO.StreamWriter(file, System.Text.Encoding.UTF8);
					try
					{
						sw.Write(Il2CppNewtonsoft.Json.JsonConvert.SerializeObject(item.GetValue(g.conf.data)));
					}
					catch (Exception ex)
					{
						MelonLogger.Error(ex.Message);
						MelonLogger.Error(ex.StackTrace);
					}
					finally
					{
						sw.Flush();
						file.Flush();
						sw.Close();
						file.Close();
						sw.Dispose();
						file.Dispose();
					}
				}*/
				foreach (var item in g.conf.allConfBase)
				{
					string filename = $"{prefix}/{item.confName}.json";
					MelonLogger.Msg(filename);
					var file = System.IO.File.Create(filename);
					file.SetLength(0);
					System.IO.StreamWriter sw = new System.IO.StreamWriter(file, System.Text.Encoding.UTF8);
					try
					{
						sw.Write(Il2CppNewtonsoft.Json.JsonConvert.SerializeObject(item.allConfBase, Il2CppNewtonsoft.Json.Formatting.Indented));
					}
					catch (Exception ex)
					{
						MelonLogger.Error(ex.Message);
						MelonLogger.Error(ex.StackTrace);
					}
					finally
					{
						sw.Flush();
						file.Flush();
						sw.Close();
						file.Close();
						sw.Dispose();
						file.Dispose();
					}
				}
			}
			catch (Exception ex)
			{
				MelonLogger.Error(ex.Message);
				MelonLogger.Error(ex.StackTrace);
			}
		}
	}

	public class ModMain
	{
		// keep reference just to be sure gc does not collect...
		private Il2CppSystem.Action<ETypeData> callIntoWorld;


		/// entry point
		public void Init()
		{
			Console.WriteLine("Init ....");

			callIntoWorld = (Il2CppSystem.Action<ETypeData>)OnIntoWorld;
			g.events.On(EGameType.IntoWorld, callIntoWorld);
		}

		public void Destroy()
		{
		}

		private void OnIntoWorld(ETypeData e)
		{
			Dumper.Dump("ConfDump");
		}
	}

}