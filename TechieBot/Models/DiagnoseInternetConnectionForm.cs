﻿using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechieBot.Models
{
    [Serializable]
    public class DiagnoseInternetConnectionForm
    {
        [Prompt("I think it's safe to assume you're having problems connecting on another device?")]
        public bool? CurrentDevice { get; set; }

        [Prompt("Have you tried turning it off and on?")]
        public bool? RestartedDevice { get; set; }

        [Prompt("Have you tried turning the router off and on recently?")]
        public bool RestartedRouter { get; set; }

        public static IForm<DiagnoseInternetConnectionForm> BuildForm()
        {
            OnCompletionAsyncDelegate<DiagnoseInternetConnectionForm> diagnoseInternetProblem = async (context, state) =>
            {
                //TODO Change strings to make sense
                var message = "Searching for Problems";
                if (!string.IsNullOrEmpty(state.CurrentDevice.ToString()))
                {
                    message += $" in {state.RestartedDevice.ToString()}...";
                }
                else if (!string.IsNullOrEmpty(state.RestartedRouter.ToString()))
                {
                    message += $" near {state.RestartedRouter.ToString()} ...";
                }

                await context.PostAsync(message);
            };

            return new FormBuilder<DiagnoseInternetConnectionForm>()
                .AddRemainingFields()
                .OnCompletion(diagnoseInternetProblem)
                .Build();
        }
    }
}