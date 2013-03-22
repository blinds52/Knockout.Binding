﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace KnckoutBindingGenerater
{
    internal static class ObservableArraysGenerator
    {
        internal static string GetCollectionsAsObservables(IBindableToJs viewModel)
        {
            var observableCollections = viewModel.GetType().ObservableCollectionProperties();

            var observableArrays = observableCollections.Select(info => CreateObservableArrayWithListener(info.Name, viewModel.Name, info.GetValue(viewModel, null))).ToList();

            return String.Join(Environment.NewLine, observableArrays);
        }

        private static string CreateObservableArrayWithListener(string name, string instanceName, object values)
        {
            const string c_ObservableTemplate = @"
                this.{0} = ko.observableArray({2});

                this.{0}.subscribe(function(newValue) {{    
                    var actualClrCollection = {1}.get_{0}().DisableNotifications();

                    actualClrCollection.Clear();

                    for (var i = 0; i < newValue.length; i++) {{
                        actualClrCollection.Add(newValue[i]);
                    }}
                }});
            ";

            var valuesAsJson = JsonUtils.GetJsonForEnumerable((IEnumerable<object>)values);

            return String.Format(c_ObservableTemplate, name, instanceName, valuesAsJson);
        }
    }
}