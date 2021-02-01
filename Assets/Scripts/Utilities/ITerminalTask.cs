using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities.Terminal {
    public interface ITerminalTask {

        /// <summary>
        /// Returns the unique ID of a task.
        /// </summary>
        /// <returns> Tasks Unique ID </returns>
        int GetID();

        /// <summary>
        /// Returns the ID of the task that follows this one. Should only be called after current task is finished. "-1" if no task follows.
        /// 
        /// The IDs are given as follow:
        /// 1.. -> Linux check
        /// 2.. -> Heme setup
        /// 3.. -> Heme build
        /// 4.. -> Heme simulation
        /// 
        /// The list with all IDs + classes is at the end of this file.
        /// </summary>
        /// <returns> Next tasks ID </returns>
        int GetNextID();

        /// <summary>
        /// Returns the name of the task. In case the command isn't easily redable/ for displaying purposes.
        /// </summary>
        /// <returns> Name describing the task </returns>
        string GetName();

        /// <summary>
        /// Contains the string that gets executed in the terminal. Doesn't have to include "/C".
        /// </summary>
        /// <returns> Command to be executed </returns>
        string GetTerminalCommand();

        /// <summary>
        /// Task progress in range 0..1, if not possible to determine it's -1.
        /// </summary>
        /// <returns> Progress in range 0..1, -1 if unknown </returns>
        float GetProgress();

        /// <summary>
        /// To add output after the task is finished. Should be used to calculate progress and whether command finished successful. 
        /// </summary>
        /// <param name="newOutput"></param>
        void AddOutput(string newOutput);

        /// <summary>
        /// To get the output lines of the console.
        /// </summary>
        /// <returns> Output lines of the console </returns>
        string GetOutput();

        /// <summary>
        /// To tell the task the process is done, should only be used by the TerminalManager.
        /// </summary>
        void ProcessDone();
    }
}


/*
 *List of all Tasks + ID 
 *
 *---------- Linux setup ----------
 * ((many usually installed with linux-build-essentials, check just in case))
 * 100: LinuxAptUpdateUpgrade //so that we only have
 * 101: Git installed check
 * 102: Git install
 * 103: GCC installed check
 * 104: GCC install
 * 105: MPI install check
 * 106: MPI install
 * 107: GNU Make check
 * 108: Gnu Make install
 * 109: Cmake installed check
 * 110: Cmake install, cmake-curses-gui (ccmake) install
 * 111: Python check
 * 112: Python downgrade
 * 113: Python install
 * 
 *---------- Heme dependency setup ----------
 * 200: Git Clone Heme
 * 201: Git Clone HemeXtract
 * 202: Cmake folder setup (create folder) -> Dependency build folder setup //rename
 * 203: Dependency build Cmake configuration // rename
 * 204: Make build Heme dependencies
 * 205: Make HemeXtract
 * 
 * TODO/ optional:
 * 210: Setup test Heme                 (not implemented)
 * 211: Build test Heme                 (not implemented)
 * 212: Run test Heme simulation        (not implemented)
 * 213: Delete test Heme build          (not implemented)
 * 
 *---------- Heme build (workaround) ----------
 * 300: Delete build folder
 * 301: Build folder setup (create build create folder)
 * 302: Build Cmake configuration
 * 303: Make build Heme
 * 
 *---------- Heme simulation ----------
 * 400: Simulation folder setup         
 * 401: Copy 3D model                   (workaround only)
 * 402: Convert 3D model                (workaround only)
 * 403: Generate input.xml              (workaround only)
 * 404: Simulation
 * 405: Convert data HemeXtract         (not implemented)
 * 406: Convert data Paraview           (not implemented)
 * 407: Convert data Unity              (not implemented)
 * 
 *---------- Error handling ----------
 * 900: LinuxSetupError
 * 901: HemeDependencySetupError
 * 902: HemeBuildError
 * 903: HemeSimulationError
 * 
 * TODO: evaluate output to included errors
 * (output evaluation for several tasks missing -> 
 * not correctly pointed towards error)
 */