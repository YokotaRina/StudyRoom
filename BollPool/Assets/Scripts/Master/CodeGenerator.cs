using System.Diagnostics;
using UnityEditor;
using UnityEngine;

public class MenuItems : MonoBehaviour {
    [MenuItem ("MasterMemory/CodeGenerate")]
    private static void Generate () {
        ExecuteMasterMemoryCodeGenerator ();
        ExecuteMessagePackCodeGenerator ();
    }

    private static void ExecuteMasterMemoryCodeGenerator () {
        UnityEngine.Debug.Log ($"{nameof(ExecuteMasterMemoryCodeGenerator)} : start");

        var exProcess = new Process ();

        var rootPath = Application.dataPath;
        var filePath = rootPath + "/GeneratorTools/MasterMemory.Generator";
        var exeFileName = "";
#if UNITY_EDITOR_WIN
        exeFileName = "/win-x64/MasterMemory.Generator.exe";
#elif UNITY_EDITOR_OSX
        exeFileName = "/osx-x64/MasterMemory.Generator";
#elif UNITY_EDITOR_LINUX
        exeFileName = "/linux-x64/MasterMemory.Generator";
#else
        return;
#endif

        var psi = new ProcessStartInfo () {
            CreateNoWindow = true,
            WindowStyle = ProcessWindowStyle.Hidden,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            FileName = filePath + exeFileName,
            Arguments = $@"-i ""{rootPath}/Scripts/Master/MPokemon"" -o ""{rootPath}/Scripts/Generated/MasterMemory"" -n ""MasterData""",
        };

        var p = Process.Start (psi);

        p.EnableRaisingEvents = true;
        p.Exited += (object sender, System.EventArgs e) => {
            var data = p.StandardOutput.ReadToEnd ();
            UnityEngine.Debug.Log ($"{data}");
            UnityEngine.Debug.Log ($"{nameof(ExecuteMasterMemoryCodeGenerator)} : end");
            p.Dispose ();
            p = null;
        };
    }

    private static void ExecuteMessagePackCodeGenerator () {
        UnityEngine.Debug.Log ($"{nameof(ExecuteMessagePackCodeGenerator)} : start");

        var exProcess = new Process ();

        var rootPath = Application.dataPath;
        var filePath = rootPath + "/GeneratorTools/mpc";
        var exeFileName = "";
#if UNITY_EDITOR_WIN
        exeFileName = "/win/mpc.exe";
#elif UNITY_EDITOR_OSX
        exeFileName = "/osx/mpc";
#elif UNITY_EDITOR_LINUX
        exeFileName = "/linux/mpc";
#else
        return;
#endif
        
        var psi = new ProcessStartInfo () {
            CreateNoWindow = true,
            WindowStyle = ProcessWindowStyle.Hidden,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            FileName = filePath + exeFileName,
            Arguments = $@"-i ""{rootPath}/Scripts/Master/MPokemon"" -o ""{rootPath}/Scripts/Generated/MessagePack""",
        };

        var p = Process.Start (psi);

        p.EnableRaisingEvents = true;
        p.Exited += (object sender, System.EventArgs e) => {
            var data = p.StandardOutput.ReadToEnd ();
            UnityEngine.Debug.Log ($"{data}");
            UnityEngine.Debug.Log ($"{nameof(ExecuteMessagePackCodeGenerator)} : end");
            p.Dispose ();
            p = null;
        };
    }
}
