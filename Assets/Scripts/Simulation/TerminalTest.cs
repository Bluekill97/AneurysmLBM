using System.Collections;
using System.Diagnostics;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Terminal;
using HemeSimulation.Tasks;


public class TerminalTest : MonoBehaviour {

    /*
    Process process;

    public void Test1() {
        string strCmdText;
        //strCmdText = "/K ping google.com";
        strCmdText = "/K " + new GetCurrentPath().GetTerminalCommand();
        Process.Start("CMD.exe", strCmdText);
    }

    public void Test2() {
        process = new System.Diagnostics.Process();
        process.StartInfo.FileName = "cmd.exe";
        process.StartInfo.Arguments = "/K " + new TestTask().GetTerminalCommand();
        process.Start();
    }

    public void Test3() {
        process = new System.Diagnostics.Process();
        process.StartInfo.FileName = "cmd.exe";
        process.StartInfo.Arguments = "/K " + new GetCurrentPath().GetTerminalCommand();

        //redirect standard outout -> required setting set UseShellExecute false
        process.StartInfo.UseShellExecute = false;

        //redirect all inputs from the window to us
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardInput = true;
        process.StartInfo.RedirectStandardError = true;
        process.OutputDataReceived += ProcessDataReceived;
        process.ErrorDataReceived += ProcessErrorReceived;
        process.Exited += CommandExit;

        //since everything is redirected we dont need the window anymore
        process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

        process.Start();

        //impotant to receive redirected input
        process.BeginOutputReadLine();
        process.BeginErrorReadLine();


        //alternate way to read output
        //while (!process.StandardOutput.EndOfStream) {
        //Console.WriteLine(proc.StandardOutput.ReadLine());
        //UnityEngine.Debug.Log( "<color=blue>" + process.StandardOutput.ReadLine() + "</color>");
        //} // -> cant use Redirect..Output and this at once
    }



    private void ProcessDataReceived(object sender, DataReceivedEventArgs e) {
        // null data means we've received everything
        if (e.Data == null) {
            UnityEngine.Debug.Log("<color=yellow> Data output finished</color>");
            process.CancelOutputRead();
            process.CancelErrorRead();
            return;
        }

        //UnityEngine.Debug.Log("<color=red>Process data received:</color>");
        UnityEngine.Debug.Log(e.Data);

        // apparently after last output:
        if (e.Data == "") {
            UnityEngine.Debug.Log("<color=teal>Terminal output End</color>");
            if (!corunning) {
                UnityEngine.Debug.Log("STARIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIING");
                StartCoroutine(COCheck());
            }
            NextTask();
        }
    }

    private void ProcessErrorReceived(object sender, DataReceivedEventArgs e) {
        // null data means we've received everything
        if (e.Data == null) {
            UnityEngine.Debug.Log("<color=yellow> Error output finished</color>");
            process.CancelOutputRead();
            process.CancelErrorRead();

            IsProcessRunning();
            NextTask();

            //return;
        }
        else {
            UnityEngine.Debug.Log("<color=red>Process received error:</color>");
            UnityEngine.Debug.Log(e.Data);

        }
    }

    private void CommandExit(object sender, System.EventArgs e) {
        UnityEngine.Debug.Log("<color=green> Command exited</color>");
    }

    private void IsProcessRunning() {
        var proc = Process.GetProcessById(process.Id);

        if (proc != null)
            UnityEngine.Debug.Log("Process Running");
        else
            UnityEngine.Debug.Log("Process not running");
    }


    int cc = 0;

    private void NextTask() {

        if (cc < 1) {
            cc++;
            process.StandardInput.WriteLine(new WINListFiles().GetTerminalCommand());
        }
        else {
            UnityEngine.Debug.Log("Starting to end the process");

            process.CancelErrorRead();
            process.CancelOutputRead();
            process.OutputDataReceived -= ProcessDataReceived;
            process.ErrorDataReceived -= ProcessErrorReceived;
            process.Exited -= CommandExit;


            process.Kill();

            StartCoroutine(EndProcess());
        }

    }

    private IEnumerator EndProcess() {

        UnityEngine.Debug.Log("Running?");
        IsProcessRunning();
        yield return new WaitForEndOfFrame();

        UnityEngine.Debug.Log("Killing");
        process.Kill();
        yield return new WaitForEndOfFrame();

        UnityEngine.Debug.Log("Still Running?");
        IsProcessRunning();
    }

    private void PrintAllProcesses() {
        Process[] processlist = Process.GetProcesses();

        UnityEngine.Debug.Log("Processes:");
        foreach (Process theprocess in processlist) {
            UnityEngine.Debug.Log("Process: " + theprocess.ProcessName + " ID: " + theprocess.Id);
        }
    }

    bool corunning = false;
    private IEnumerator COCheck() {
        corunning = true;
        while (true) {
            UnityEngine.Debug.Log("Ping");
            yield return new WaitForSeconds(1f);
        }
    }

    */
}
