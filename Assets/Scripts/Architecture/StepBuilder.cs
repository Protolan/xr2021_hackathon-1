﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using ScriptableSystem.GameEvent;
using UnityEditor;
using UnityEngine;
using Voice;

namespace Architecture
{
    public class StepBuilder
    {
#if UNITY_EDITOR
        public List<Step> CreateStepsFromFile(string filePath, List<Step> steps)
        {
            var lines = CreateLines(filePath);
            if (lines == null) return steps;


            string[] separatingStrings = { "<>", "<", ">" };
            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line)) continue;
                string[] variables = line.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);
                var step = ScriptableObject.CreateInstance<Step>();
                AssetDatabase.CreateAsset(step, $"Assets/Main/Steps/{variables[0]}.asset");
                step.Features.Add(variables[2] == "1" ? StepFeature.ModularMenu : StepFeature.DialogMenu);
                step.DialogMenuData._text = variables[1];
                step.ModularMenuData._mainText = variables[1];
                steps.Add(step);
            }

            AssetDatabase.SaveAssets();
            return steps;
        }

        public void SetButtonActionFromFile(string filePath, List<Step> steps)
        {
            var lines = CreateLines(filePath);
            if (lines == null) return;

            string[] separatingStrings = { "<>", "<", ">" };
            var stepDictionary = steps.ToDictionary(x => x.name);

            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line)) continue;
                string[] variables = line.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);
                stepDictionary[variables[0]].Features.Add(StepFeature.DeviceAction);
                stepDictionary[variables[0]].DeviceButtonActionData._buttonID = variables[1];
                stepDictionary[variables[0]].DeviceButtonActionData._footnoteText = variables[2];
            }
        }

        public void SetStepTransitions(List<Step> steps, GameEvent condition)
        {
            for (int i = 1; i < steps.Count; i++)
                steps[i - 1].Transitions.Add(new Transition(steps[i], condition));
        }
        

        public void SetMainMenuExitSteps(string filePath, List<Step> steps, Step mainMenuStep, GameEvent condition)
        {
            var lines = CreateLines(filePath);
            if (lines == null) return;

            string[] separatingStrings = { "<>", "<", ">" };
            var stepDictionary = steps.ToDictionary(x => x.name);

            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line)) continue;
                string[] variables = line.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);
                stepDictionary[variables[0]].Transitions.Clear();
                stepDictionary[variables[0]].Transitions.Add(new Transition(mainMenuStep, condition));
            }
        }

        public void SetCancelSteps(string filePath, List<Step> steps)
        {
            var lines = CreateLines(filePath);
            if (lines == null) return;

            string[] separatingStrings = { "<>", "<", ">" };
            var stepDictionary = steps.ToDictionary(x => x.name);

            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line)) continue;
                string[] variables = line.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);
                stepDictionary[variables[0]].ModularMenuData._features = new[] {ModularMenuFeature.Cancel};
            }
        }

        public void SetVoiceActingFromFiles(List<Step> steps, string folderPath)
        {
            foreach (var step in steps)
            {
                if (!step.ContainsFeature(StepFeature.VoiceActing))
                    step.Features.Add(StepFeature.VoiceActing);
                step.ActingData._clip =
                    (AudioClip)AssetDatabase.LoadAssetAtPath($"{folderPath}/{step.name}.wav", typeof(AudioClip));
            }
        }

        public void SetVoiceListenersFromFile(List<Step> steps, string filePath, VoiceIntent intent, AudioClip defaultClip)
        {
            var lines = CreateLines(filePath);
            if (lines == null) return;

            string[] separatingStrings = { "<>", "<", ">" };
            var stepDictionary = steps.ToDictionary(x => x.name);

            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line)) continue;
                string[] variables = line.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);
                Debug.Log(variables[0]);
                if (!stepDictionary[variables[0]].ContainsFeature(StepFeature.VoiceListener))
                    stepDictionary[variables[0]].Features.Add(StepFeature.VoiceListener);
                stepDictionary[variables[0]].ListenerData._intents = new[] { intent };
                stepDictionary[variables[0]].ListenerData._notUnderStandClip = defaultClip;
            }
        }

        public void CreateStepsJSON(List<Step> steps, string filePath)
        {
            var stepsSerializable = new StepSerializable[steps.Count];
            for (var i = 0; i < stepsSerializable.Length; i++) 
                stepsSerializable[i] = new StepSerializable(steps[i]);
            var stepsJson = JsonUtility.ToJson(new StepsSerializable(stepsSerializable));
            File.WriteAllText(filePath, stepsJson);
        }

        private static string[] CreateLines(string filePath)
        {
            return File.Exists(filePath) ? File.ReadAllLines(filePath) : null;
        }
#endif
    }

}