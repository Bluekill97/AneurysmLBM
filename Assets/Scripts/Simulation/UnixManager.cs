﻿using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Reflection;
using UnityEngine;
using Utilities.Terminal;
using HemeSimulation.Tasks.LinuxSetup;

namespace HemeSimulation {
    public class UnixManager {
        public bool verbose = true;

        private TerminalManager TerminalMgr;

        /// <summary>
        /// TaskDictionary holds all tasks with corresponding IDs. Useful since successor tasks are given by ID.
        /// </summary>
        private Dictionary<int, ITerminalTask> TaskDic = new Dictionary<int, ITerminalTask>();

        /// <summary>
        /// Stack of tasks, intended to use for logging in case of an error
        /// </summary>
        private List<ITerminalTask> TaskStack = new List<ITerminalTask>();

        /// <summary>
        /// Task that is currently being executed, usable for monitoring the progress to the user (mostly the simulation itself)
        /// </summary>
        private ITerminalTask ttask;

        private int LinuxCheckStartTaskID = 100;
        private int HemeSetupStartTaskID = 200;
        private int HemeBuildStartTaskID = 300;
        private int SimulationStartTaskID = 400;

        private bool RequirementCheckSuccessfull = false;
        private bool HemeSetupSuccessfull = false;
        private bool HemeBuildSuccess = false;
        private bool SimulationSuccess = false;

        public UnixManager() {
            TerminalMgr = new TerminalManager();
            PopulateTaskList();
        }

        /// <summary>
        /// Starts task queue to set up Linux (installation of Linux itself not included) by installing all necessary packages
        /// Should only be done once for setup (and when there are updates maybe)
        /// </summary>
        /// <returns> True if setup was successfull </returns>
        public async Task<bool> UnixRequirementCheckAsync() {
            // TODO: maybe check for internet connection, e.g. ping google.com

            RequirementCheckSuccessfull = await ExecuteCommandQueue(LinuxCheckStartTaskID);

            // TODO: Debug and replace this hotfix
            // somehow the version checks only work the 2nd time
            RequirementCheckSuccessfull = await ExecuteCommandQueue(LinuxCheckStartTaskID);

            return RequirementCheckSuccessfull;
        }

        /// <summary>
        /// Installs the Heme software (download from git) and builds its dependencies
        /// </summary>
        /// <returns> True if setup was successfull </returns>
        public async Task<bool> HemeSimSetupAsync() {
            if (!RequirementCheckSuccessfull)
                return false;

            HemeSetupSuccessfull = await ExecuteCommandQueue(HemeSetupStartTaskID);

            return HemeSetupSuccessfull;
        }

        /// <summary>
        /// Builds Heme sofware with entered settings and starts the simulation
        /// </summary>
        /// <returns> True if setup was successfull </returns>
        public async Task<bool> HemeSimStartAsync() {
            // TODO: check if Heme is already build with correct settings (then skip the heme build)
            // workaround: just delete and build new

            // Heme build
            HemeBuildSuccess = await ExecuteCommandQueue(HemeBuildStartTaskID);

            if (!HemeBuildSuccess)
                return false;


            // Start Simulations
            SimulationSuccess = await ExecuteCommandQueue(SimulationStartTaskID);

            return SimulationSuccess;
        }

        private async Task<bool> ExecuteCommandQueue(int startID) {
            TaskStack = new List<ITerminalTask>();

            int nextTaskID = startID;

            // work off all tasks, for a overview look at the ITerminalTask class (end of the file)
            while (TaskDic.ContainsKey(nextTaskID)) {
                TaskDic.TryGetValue(nextTaskID, out ttask);

                // IDs higher/ equal 900 indicate an error
                if (nextTaskID >= 900) {
                    Debug.LogError($"<color=red> Task ended with error {ttask.GetName()}</color>");
                    TaskStack.Add(ttask);
                    LogStackForError();
                    return false;
                }

                // TODO: instead of this if insert some interface to show progress to the user?
                if (verbose)
                    Debug.Log("<color=blue>Executing command " + ttask.GetID() + " " + ttask.GetName() + "</color>");

                // Execute and wait for task
                bool exitedSuccessful = await TerminalMgr.RunAsync(ttask);

                if (verbose)
                    Debug.Log($"<color=teal>Task {ttask.GetName()} was " + (exitedSuccessful ? "successful" : "unsuccessful")
                         + ", continuing with task " + (TaskDic.ContainsKey(ttask.GetNextID()) ? TaskDic[ttask.GetNextID()].GetName() : $"<no new task with ID {ttask.GetNextID()}></color>"));

                TaskStack.Add(ttask);

                if (!exitedSuccessful) {
                    // TODO: inform user
                    Debug.LogError($"<color=red> Task {ttask.GetName()} ended with an error</color>");
                    LogStackForError();
                    return false;
                }

                nextTaskID = ttask.GetNextID();
            }

            ttask = null;
            return true;
        }


        public ProgressStatus GetCurrentProgress() {
            return null;
        }

        public bool CurrentTaskDone() {
            return false;
        }

        public bool CurrentTaskFinshedSuccessful() {
            return true;
        }

        private void PopulateTaskList() {

            // get all ITerminalTasks to the dictionary
            var ITTinstances = from t in Assembly.GetExecutingAssembly().GetTypes()
                            where t.GetInterfaces().Contains(typeof(ITerminalTask))
                                     && t.GetConstructor(Type.EmptyTypes) != null
                            select Activator.CreateInstance(t) as ITerminalTask;

            // add all ITerminalTasks to the dictionary
            foreach (var instance in ITTinstances)
                TaskDic.Add(instance.GetID(), instance);
        }

        private void LogStackForError() {
            Utilities.ErrorLog.LogTaskError(TaskStack);
        }

        /// <summary>
        /// Dataholder to show progress to user 
        /// </summary>
        public class ProgressStatus {
            public string CurrentTaskName { get; private set; }
            public int CurrentTaskNumber { get; private set; }
            public int TotalTaskNumber { get; private set; }
            public float CurrentProgress { get; private set; }

            public ProgressStatus(string taskName, int taskNumber, int totTaskNumber, float progress) {
                CurrentTaskName = taskName;
                CurrentTaskNumber = taskNumber;
                TotalTaskNumber = totTaskNumber;
                CurrentProgress = progress;
            }
        }
    }
}